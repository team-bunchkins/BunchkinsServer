using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
    class LoseLevelCurseCard : CurseCard, IAnytimeSpell
    {
        public void Cast(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
