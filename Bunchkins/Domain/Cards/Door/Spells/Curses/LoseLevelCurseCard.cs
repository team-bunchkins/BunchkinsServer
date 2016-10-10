using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
   public class LoseLevelCurseCard : CurseCard, IAnytimeSpell
    {
        public int Level { get; set; }
        public void Cast(Player player)
        {
           player.DecreaseLevel(Level);
        }
    }
}
