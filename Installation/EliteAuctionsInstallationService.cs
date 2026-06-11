using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Services.Localization;

namespace Nop.Plugin.Misc.EliteAuctions.Installation
{
    public class EliteAuctionsInstallationService : IEliteAuctionsInstallationService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IAuctionStageService _auctionStageService;

        #endregion

        #region Constructor

        public EliteAuctionsInstallationService(ILocalizationService localizationService,
        IAuctionStageService auctionStageService)
        {
            _localizationService = localizationService;
            _auctionStageService = auctionStageService;
        }

        #endregion

        #region Methods

        private async Task<AuctionStage> EnsureAuctionStageAsync(SystemAuctionStageEnum stageEnum, int displayOrder)
        {
            var systemName = stageEnum.ToString();
            var stage = await _auctionStageService.GetAuctionStageBySystemName(systemName);

            if (stage == null)
            {
                stage = new AuctionStage
                {
                    DisplayOrder = displayOrder,
                    Name = systemName
                };
                await _auctionStageService.InsertAuctionStage(stage);
            }

            return stage;
        }

        public async Task InstallSystemAuctionStages()
        {
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Open, 1);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Bidding, 2);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Settlement, 3);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Closed, 4);
        }

        public async Task InstallLocaleResources()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.EliteAuctions.ActivateAuction"] = "Activate Auction",
                ["Plugins.Misc.EliteAuctions.AuctionOptions"] = "Auction Options",
                ["Plugins.Misc.EliteAuctions.PlaceBid"] = "Make a Bid",
                ["Plugins.Misc.EliteAuctions.ApplyAuctionSettings"] = "Apply Auction Settings",
                ["Plugins.Misc.EliteAuctions.EndsIn"] = "End in"

            });
        }

        #endregion
    }
}
