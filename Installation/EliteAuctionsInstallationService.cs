using Nop.Core.Domain.ScheduleTasks;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Reminders;
using Nop.Services.Localization;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Misc.EliteAuctions.Installation
{
    public class EliteAuctionsInstallationService : IEliteAuctionsInstallationService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IAuctionStageService _auctionStageService;
        private readonly IScheduleTaskService _scheduleTaskService;

        #endregion

        #region Constructor

        public EliteAuctionsInstallationService(ILocalizationService localizationService,
        IAuctionStageService auctionStageService,
        IScheduleTaskService scheduleTaskService)

        {
            _localizationService = localizationService;
            _auctionStageService = auctionStageService;
            _scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Utilities

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

        #endregion

        #region Methods

        /// <summary>
        /// Installs the system auction stages
        /// </summary>
        public async Task InstallSystemAuctionStages()
        {
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Open, 1);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Bidding, 2);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Settlement, 3);
            await EnsureAuctionStageAsync(SystemAuctionStageEnum.Closed, 4);
        }

        /// <summary>
        /// Installs the locale resources for the plugin
        /// </summary>
        public async Task InstallLocaleResources()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.EliteAuctions.ActivateAuction"] = "Activate Auction",
                ["Plugins.Misc.EliteAuctions.AuctionOptions"] = "Auction Options",
                ["Plugins.Misc.EliteAuctions.PlaceBid"] = "Make a Bid",
                ["Plugins.Misc.EliteAuctions.ApplyAuctionSettings"] = "Apply Auction Settings",
                ["Plugins.Misc.EliteAuctions.EndsIn"] = "Ends in",
                ["Plugins.Misc.EliteAuctions.StartingBidPrice"] = "Starting Bid Price",
                ["Plugins.Misc.EliteAuctions.ReserveBidPrice"] = "Reserve Bid Price",
                ["Plugins.Misc.EliteAuctions.StartDateTimeUtc"] = "Start Date and Time",
                ["Plugins.Misc.EliteAuctions.EndDateTimeUtc"] = "End Date and Time",
                ["Plugins.Misc.EliteAuctions.ExtensionTriggerSeconds"] = "Extension Trigger Seconds"

            });
        }

        /// <summary>
        /// Installs the scheduled tasks for the plugin
        /// </summary>
        public async Task InstallScheduledTasks()
        {
            var scheduledTask = await _scheduleTaskService.GetTaskByTypeAsync(SystemScheduledTask.ProcessEndedAuctionsType);

            if (scheduledTask == null)
            {
                scheduledTask = new ScheduleTask
                {
                    Enabled = true,
                    Name = SystemScheduledTask.ProcessEndedAuctionsName,
                    Seconds = 5,
                    StopOnError = true,
                    Type = SystemScheduledTask.ProcessEndedAuctionsType
                };

                await _scheduleTaskService.InsertTaskAsync(scheduledTask);
            }
        }

        #endregion
    }
}
