﻿using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
    class LoseHandCardsCurseCard : CurseCard, IAnytimeSpell
    {
        public List<Card> Hand { get; set; }
        public void Cast(Player player)
        {
            player.Hand.Clear();
        }
    }
}
