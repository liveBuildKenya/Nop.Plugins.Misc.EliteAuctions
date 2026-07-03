using Microsoft.Extensions.Logging;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Bids.Services;
using Nop.Plugin.Misc.EliteAuctions.MarketPlace;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Misc.EliteAuctions.Reminders
{
    /// <summary>
    /// Process ended auctions
    /// </summary>
    public class ProcessEndedAuctions : IScheduleTask
    {
        #region Fields

        private readonly ILogger<ProcessEndedAuctions> _logger;
        private readonly IAuctionService _auctionService;
        private readonly IProductService _productService;
        private readonly IBidService _bidService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMarketPlaceFactory _marketPlaceFactory;
        private readonly IShoppingCartService _shoppingCartService;

        #endregion

        #region Ctor

        public ProcessEndedAuctions(ILogger<ProcessEndedAuctions> logger,
            IAuctionService auctionService,
            IAuctionStageService auctionStageService,
            IAuctionStageHistoryService auctionStageHistoryService,
            IProductService productService,
            IBidService bidService,
            IMarketPlaceFactory marketPlaceFactory,
            IShoppingCartService shoppingCartService,
            ILocalizationService localizationService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper)

        {
            _logger = logger;
            _auctionService = auctionService;
            _productService = productService;
            _bidService = bidService;
            _marketPlaceFactory = marketPlaceFactory;
            _shoppingCartService = shoppingCartService;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Methods

        public async Task ExecuteAsync()
        {
            var auctionsList = await _auctionService.GetAuctionsOnBidding();

            foreach (var auction in auctionsList)
            {
                var product = await _productService.GetProductByIdAsync(auction.ProductId);

                if (product != null && auction.WinningCustomerId == null)
                {
                    var now = DateTime.Now;
                    var endDate = auction.EndDateTimeUtc;

                    if (now >= endDate)
                    {
                        var winningBid = await _bidService.GetHighestBid(auction.Id);
                        if (winningBid != null && auction.CurrentBidPrice == winningBid.Amount)
                        {
                            var customer = await _customerService.GetCustomerByIdAsync(winningBid.CustomerId);

                            await _shoppingCartService.AddToCartAsync(customer, product, ShoppingCartType.ShoppingCart, auction.StoreId, null, winningBid.Amount, null, null, product.StockQuantity);

                            auction.WinningCustomerId = winningBid.CustomerId;
                            await _auctionService.UpdateAuction(auction);

                            await _marketPlaceFactory.MoveAuctionToNextStage(auction.Id);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
