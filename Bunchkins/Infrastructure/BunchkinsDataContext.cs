using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Treasure.Equipment;
using Bunchkins.Domain.Core;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Infrastructure
{
    public class BunchkinsDataContext : DbContext
    {
        public BunchkinsDataContext() : base("BunchkinsDatabase")
        {

        }

        public IDbSet<Card> Cards { get; set; }
        public IDbSet<Game> Games { get; set; }
        public IDbSet<Player> Players { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                        .HasMany(p => p.Hand)
                        .WithMany(c => c.PlayersHands);

            modelBuilder.Entity<Player>()
                        .HasMany(p => p.EquippedCards)
                        .WithRequired(ec => ec.Player)
                        .HasForeignKey(ec => ec.PlayerId);

            modelBuilder.Entity<EquipmentCard>()
                        .HasMany(ec => ec.PlayersEquippingThisCard)
                        .WithRequired(ec => ec.EquipmentCard)
                        .HasForeignKey(ec => ec.EquipmentCardId);

            modelBuilder.Entity<EquippedCard>()
                        .HasKey(ec => new { ec.EquipmentCardId, ec.PlayerId });
        }
    }
}
