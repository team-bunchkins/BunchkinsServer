namespace Bunchkins.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "NumCards", c => c.Int());
            AddColumn("dbo.Cards", "Levels", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "Levels");
            DropColumn("dbo.Cards", "NumCards");
        }
    }
}
