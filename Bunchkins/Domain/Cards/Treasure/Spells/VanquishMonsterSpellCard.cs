using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Core.GameStates;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class VanquishMonsterSpellCard : TreasureSpellCard, ICombatSpell
    {
        // Removes monster from combat, monster's treasure is lootable
        public void Cast(CombatState combat)
        {
            // TODO: Target specific monster
            combat.RemoveMonster(combat.Monsters.OrderByDescending(m => m.Level).First(), true);
        }
    }
}
