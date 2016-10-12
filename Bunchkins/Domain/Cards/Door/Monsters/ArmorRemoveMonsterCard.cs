using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Door.Monsters
{
    class ArmorRemoveMonsterCard : MonsterCard
    {
        public string Slot { get; set; }

        public override void BadStuff(Player player)
        {
            player.RemoveEquip(Slot);
        }
    }
}
