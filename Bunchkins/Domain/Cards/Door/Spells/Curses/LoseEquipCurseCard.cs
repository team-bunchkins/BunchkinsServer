﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Door.Spells.Curses
{
    class LoseEquipCurseCard : CurseCard, IAnytimeSpell
    {
        public List<string> Slots { get; set; }

        public void Cast(Player player)
        {
            player.RemoveEquip(Slots);
        }
    }
}
