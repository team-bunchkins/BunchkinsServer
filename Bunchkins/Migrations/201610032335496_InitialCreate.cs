namespace Bunchkins.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        PictureUrl = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Level = c.Int(),
                        LevelGain = c.Int(),
                        TreasureGain = c.Int(),
                        MinPursuitLevel = c.Int(),
                        LevelsDecreased = c.Int(),
                        CombatPower = c.Int(),
                        Gold = c.Int(),
                        Slot = c.String(),
                        Bonus = c.Int(),
                        CombatPower1 = c.Int(),
                        CombatPower2 = c.Int(),
                        Level1 = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}
