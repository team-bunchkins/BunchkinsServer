using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
    public abstract class CurseCard : DoorSpell
    {
        public abstract void Cast(Player player);
    }
}
