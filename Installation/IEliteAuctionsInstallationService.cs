namespace Nop.Plugin.Misc.EliteAuctions.Installation
{
    public interface IEliteAuctionsInstallationService
    {
        Task InstallSystemAuctionStages();
        Task InstallLocaleResources();
    }
}
