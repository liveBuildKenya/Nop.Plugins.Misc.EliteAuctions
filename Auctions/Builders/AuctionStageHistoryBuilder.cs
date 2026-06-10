using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Builders;

/// <summary>
/// Represents a builder for constructing the history of auction stages. 
/// This builder is responsible for creating a chronological record of the various stages an auction goes through, such as creation, bidding, and completion. 
/// It allows for the aggregation of relevant data and events associated with each stage, providing a comprehensive overview of the auction's lifecycle.
/// </summary>
public class AuctionStageHistoryBuilder : NopEntityBuilder<AuctionStageHistory>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(AuctionStageHistory.AuctionId)).AsInt32().NotNullable().ForeignKey<Auction>()
            .WithColumn(nameof(AuctionStageHistory.AuctionStageId)).AsInt32().NotNullable().ForeignKey<AuctionStage>()
            .WithColumn(nameof(AuctionStageHistory.CreatedOnUtc)).AsDateTimeOffset().NotNullable();
    }
}
