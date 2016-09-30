using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class LevelUpSpell : TreasureSpell, IAnytimeSpell
    {
        public int Level { get; set; }
        public void Cast(Player player)
        {
            player.IncreaseLevel(Level);
        }
    }
}
