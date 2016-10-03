using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards
{
    public abstract class Card
    {
        public int CardId { get; set; }
        public string PictureUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Player> PlayersHands { get; set; }
    }
}
