namespace Bunchkins.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlavorTextAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "FlavorText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "FlavorText");
        }
    }
}
