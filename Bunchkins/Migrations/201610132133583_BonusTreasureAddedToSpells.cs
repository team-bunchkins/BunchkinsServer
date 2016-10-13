namespace Bunchkins.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BonusTreasureAddedToSpells : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "TreasureChange", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "TreasureChange");
        }
    }
}
