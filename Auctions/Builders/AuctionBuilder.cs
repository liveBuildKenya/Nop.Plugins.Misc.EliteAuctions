using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
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
            .WithColumn(nameof(Auction.CreatedOnUtc)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Auction.WinningCustomerId)).AsInt32().Nullable().ForeignKey<Customer>(onDelete: Rule.SetNull);

    }
}
