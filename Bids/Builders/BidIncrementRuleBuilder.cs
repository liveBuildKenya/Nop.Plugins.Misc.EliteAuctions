using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;
using System.Data;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Builders
{
    /// <summary>
    /// Represents a builder for constructing bid increment rule entities.
    /// </summary>
    public class BidIncrementRuleBuilder : NopEntityBuilder<BidIncrementRule>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(BidIncrementRule.BidIncrementType)).AsString(50).NotNullable()
                .WithColumn(nameof(BidIncrementRule.LowerBandBidAmount)).AsDecimal(18, 2).NotNullable()
                .WithColumn(nameof(BidIncrementRule.UpperBandBidAmount)).AsDecimal(18, 2).Nullable()
                .WithColumn(nameof(BidIncrementRule.IncrementAmount)).AsDecimal(18, 2).NotNullable()
                .WithColumn(nameof(BidIncrementRule.Priority)).AsInt32().NotNullable()
                .WithColumn(nameof(BidIncrementRule.AuctionId)).AsInt32().Nullable().ForeignKey<Auction>(onDelete: Rule.SetNull);
        }
    }

}
