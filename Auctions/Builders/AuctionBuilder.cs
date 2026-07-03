using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using System.Data;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Builders;

/// <summary>
/// Represents a builder for constructing auction entities
/// </summary>
public class AuctionBuilder : NopEntityBuilder<Auction>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Auction.ProductId)).AsInt32().ForeignKey<Product>()
            .WithColumn(nameof(Auction.StoreId)).AsInt32().Nullable().ForeignKey<Store>(onDelete: Rule.SetNull)
            .WithColumn(nameof(Auction.WinningCustomerId)).AsInt32().Nullable().ForeignKey<Customer>(onDelete: Rule.SetNull)
            .WithColumn(nameof(Auction.StartingBidPrice)).AsDecimal(18, 2).NotNullable()
            .WithColumn(nameof(Auction.CurrentBidPrice)).AsDecimal(18, 2).NotNullable()
            .WithColumn(nameof(Auction.ReserveBidPrice)).AsDecimal(18, 2).NotNullable()
            .WithColumn(nameof(Auction.StartDateTimeUtc)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Auction.EndDateTimeUtc)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Auction.ExtensionTriggerSeconds)).AsInt32().NotNullable()
            .WithColumn(nameof(Auction.DateTimeCreatedUtc)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Auction.DateTimeUpdatedUtc)).AsDateTimeOffset().NotNullable();

    }
}
