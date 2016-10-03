namespace Bunchkins.Migrations
{
    using Domain.Cards.Door.Monsters;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bunchkins.Infrastructure.BunchkinsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bunchkins.Infrastructure.BunchkinsDataContext context)
        {
            if(context.Cards.Count() == 0)
            {
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "A big scary dragon", Level = 10, Name = "Armor Remove Monster", TreasureGain = 2 });
                context.Cards.Add(new DeathMonsterCard { Description = "A big scary death monster card", Level = 12, Name = "Death monster", TreasureGain = 4 });

                context.SaveChanges();
            }
        }
    }
}
