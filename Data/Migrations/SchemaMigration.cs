using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Data.Migrations;

[NopMigration("2026-06-09 00:00:00", "Misc.EliteAuctions schema", MigrationProcessType.Installation)]
public class SchemaMigration : Migration
{
    public override void Up()
    {
        this.CreateTableIfNotExists<Auction>();
        this.CreateTableIfNotExists<AuctionStage>();
        this.CreateTableIfNotExists<AuctionStageHistory>();
        this.CreateTableIfNotExists<Bid>();
    }

    public override void Down()
    {
        this.DeleteTableIfExists<Auction>();
        this.DeleteTableIfExists<AuctionStage>();
        this.DeleteTableIfExists<AuctionStageHistory>();
        this.DeleteTableIfExists<Bid>();
    }
}
