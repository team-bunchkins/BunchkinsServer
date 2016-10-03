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
                        Gold = c.Int(),
                        Slot = c.String(),
                        Bonus = c.Int(),
                        Level = c.Int(),
                        LevelGain = c.Int(),
                        TreasureGain = c.Int(),
                        MinPursuitLevel = c.Int(),
                        LevelsDecreased = c.Int(),
                        CombatPower = c.Int(),
                        CombatPower1 = c.Int(),
                        CombatPower2 = c.Int(),
                        Level1 = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        GameId = c.Int(),
                        Level = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.EquippedCards",
                c => new
                    {
                        EquipmentCardId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EquipmentCardId, t.PlayerId })
                .ForeignKey("dbo.Cards", t => t.EquipmentCardId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.EquipmentCardId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Player_PlayerId = c.Int(nullable: false),
                        Card_CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_PlayerId, t.Card_CardId })
                .ForeignKey("dbo.Players", t => t.Player_PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_CardId, cascadeDelete: true)
                .Index(t => t.Player_PlayerId)
                .Index(t => t.Card_CardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerCards", "Card_CardId", "dbo.Cards");
            DropForeignKey("dbo.PlayerCards", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "GameId", "dbo.Games");
            DropForeignKey("dbo.EquippedCards", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.EquippedCards", "EquipmentCardId", "dbo.Cards");
            DropIndex("dbo.PlayerCards", new[] { "Card_CardId" });
            DropIndex("dbo.PlayerCards", new[] { "Player_PlayerId" });
            DropIndex("dbo.EquippedCards", new[] { "PlayerId" });
            DropIndex("dbo.EquippedCards", new[] { "EquipmentCardId" });
            DropIndex("dbo.Players", new[] { "GameId" });
            DropTable("dbo.PlayerCards");
            DropTable("dbo.Games");
            DropTable("dbo.EquippedCards");
            DropTable("dbo.Players");
            DropTable("dbo.Cards");
        }
    }
}
