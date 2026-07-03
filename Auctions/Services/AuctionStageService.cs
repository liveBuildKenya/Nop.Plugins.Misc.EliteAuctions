using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents an auction stage service
/// </summary>
public class AuctionStageService : IAuctionStageService
{
    #region Fields

    private IRepository<AuctionStage> _auctionStageRepository;

    #endregion

    #region Constructor

    public AuctionStageService(IRepository<AuctionStage> auctionStageRepository)
    {
        _auctionStageRepository = auctionStageRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets an auction stage by auctionId
    /// </summary>
    /// <param name="auctionStageId">Auction stage identifer</param>
    /// <returns>Auction Stage</returns>
    public async Task<AuctionStage> GetAuctionStageById(int auctionStageId)
    {
        return await _auctionStageRepository.GetByIdAsync(auctionStageId);
    }

    /// <summary>
    /// Gets an auction stage by system name
    /// </summary>
    /// <param name="systemName">System name</param>
    /// <returns>Auction stage</returns>
    public async Task<AuctionStage> GetAuctionStageBySystemName(string systemName)
    {
        return await _auctionStageRepository.Table
            .Where(x => x.Name.ToLower() == systemName.ToLower())
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Gets the initial auction stage
    /// </summary>
    /// <returns>Auction stage</returns>
    public async Task<AuctionStage> GetInitialAuctionStage()
    {
        return await _auctionStageRepository.Table
            .OrderBy(x => x.DisplayOrder)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Inserts an auction stage
    /// </summary>
    /// <param name="auctionStage">Auction stage</param>
    public async Task InsertAuctionStage(AuctionStage auctionStage)
    {
        ArgumentNullException.ThrowIfNull(auctionStage);

        await _auctionStageRepository.InsertAsync(auctionStage);
    }

    #endregion
}
