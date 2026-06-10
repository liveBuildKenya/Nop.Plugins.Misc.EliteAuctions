using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

public class AuctionStageService : IAuctionStageService
{
    private IRepository<AuctionStage> _auctionStageRepository;

    public AuctionStageService(IRepository<AuctionStage> auctionStageRepository)
    {
        _auctionStageRepository = auctionStageRepository;
    }

    public async Task<AuctionStage> GetBeginingAuctionStage()
    {
        return await _auctionStageRepository.Table
            .OrderByDescending(x => x.DisplayOrder)
            .FirstOrDefaultAsync();
    }

    public async Task InsertAuctionStage(AuctionStage auctionStage)
    {
        ArgumentNullException.ThrowIfNull(auctionStage);

        await _auctionStageRepository.InsertAsync(auctionStage);
    }
}
