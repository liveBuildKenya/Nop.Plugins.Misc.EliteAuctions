using Microsoft.Extensions.Logging;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Overrrides;
using Nop.Services.Events;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Consumers
{
    public class PrepareAuctionModelCosumer : IConsumer<ModelPreparedEvent<BaseNopModel>>,
        IConsumer<ProductOverviewModelPreparedEvent>
    {
        private readonly IAuctionService _auctionService;
        private readonly ILogger<PrepareAuctionModelCosumer> logger;

        public PrepareAuctionModelCosumer(IAuctionService auctionService, ILogger<PrepareAuctionModelCosumer> logger)
        {
            _auctionService = auctionService;
            this.logger = logger;
        }
        public async Task HandleEventAsync(ModelPreparedEvent<BaseNopModel> eventMessage)
        {
            switch (eventMessage.Model)
            {
                case ProductDetailsModel detailsModel:
                    await HandleProductDetails(detailsModel);
                    break;
            }

            await Task.CompletedTask;
        }

        public async Task HandleEventAsync(ProductOverviewModelPreparedEvent eventMessage)
        {
            await HandleProductOverview(eventMessage.Model);

            await Task.CompletedTask;
        }

        private async Task HandleProductDetails(ProductDetailsModel model)
        {
            if (await IsAuctionProduct(model.Id))
                return;

            // Disable buy button
            model.AddToCart.DisableBuyButton = true;
            model.AddToCart.DisableWishlistButton = true;

        }

        private async Task HandleProductOverview(ProductOverviewModel model)
        {
            if (await IsAuctionProduct(model.Id))
                return;

            model.ProductPrice.DisableBuyButton = true;
        }

        private async Task<bool> IsAuctionProduct(int productId)
        {
            var auction = await _auctionService.GetAuctionByProductId(productId);

            return (auction == null);
        }
    }
}
