using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
    public class LoseAllEquipCurseCard : CurseCard, IAnytimeSpell
    {

        public override void Cast(Player player)
        {
            player.RemoveAllEquips();
        }
    }
}