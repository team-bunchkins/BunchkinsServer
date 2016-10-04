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

    }
}
