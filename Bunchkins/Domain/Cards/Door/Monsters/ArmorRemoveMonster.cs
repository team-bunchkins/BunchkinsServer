using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Door.Monsters
{
    class ArmorRemoveMonster : Monster
    {
        public List<string> Slots { get; set; }

        public override void BadStuff(Player player)
        {
            player.LoseArmor(Slots);
        }
    }
}
