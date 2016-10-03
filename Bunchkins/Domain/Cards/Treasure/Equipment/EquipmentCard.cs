using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Treasure.Equipment
{
    public class EquipmentCard : TreasureCard
    {
        public string Slot { get; set; }
        public int Bonus { get; set; }
        public List<string> RestrictedTo { get; set; }

        public virtual ICollection<EquippedCard> PlayersEquippingThisCard { get; set; }
    }
}
