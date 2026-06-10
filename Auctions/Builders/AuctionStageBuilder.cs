using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Builders;

/// <summary>
/// Represents a builder for constructing auction stage entities
/// </summary>
public class AuctionStageBuilder : NopEntityBuilder<AuctionStage>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(AuctionStage.Name)).AsString(400).NotNullable()
            .WithColumn(nameof(AuctionStage.DisplayOrder)).AsInt32().NotNullable();

    }
}
