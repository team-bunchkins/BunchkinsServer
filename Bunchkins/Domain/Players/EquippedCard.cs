using Bunchkins.Domain.Cards.Treasure.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Players
{
    public class EquippedCard
    {
        public int PlayerId { get; set; }
        public int EquipmentCardId { get; set; }

        public virtual Player Player { get; set; }
        public virtual EquipmentCard EquipmentCard { get; set; }
    }
}
