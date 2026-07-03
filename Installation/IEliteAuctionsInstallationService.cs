namespace Nop.Plugin.Misc.EliteAuctions.Installation
{
    public interface IEliteAuctionsInstallationService
    {
        /// <summary>
        /// Installs system auction stages
        /// </summary>
        Task InstallSystemAuctionStages();

        /// <summary>
        /// Installs locale resources
        /// </summary>
        Task InstallLocaleResources();

        /// <summary>
        /// Installs scheduled tasks
        /// </summary>
        Task InstallScheduledTasks();
    }
}
