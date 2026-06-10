using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Builders;

/// <summary>
/// Represents a builder for constructing bid entities.
/// </summary>
public class BidBuilder : NopEntityBuilder<Bid>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Bid.AuctionId)).AsInt32().NotNullable().ForeignKey<Auction>()
            .WithColumn(nameof(Bid.Amount)).AsDecimal().NotNullable()
            .WithColumn(nameof(Bid.MaximumBidAmount)).AsDecimal().Nullable()
            .WithColumn(nameof(Bid.CreatedOnUtc)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Bid.CustomerId)).AsInt32().Nullable().ForeignKey<Customer>(onDelete: Rule.SetNull);
    }
}
