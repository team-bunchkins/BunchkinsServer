using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class EscapeAllSpellCard : TreasureSpellCard, ICombatSpell
    {
        // Removes all monsters from combat, monsters' treasure is not lootable
        public void Cast(CombatState combat)
        {
            //Make monsters in combat go away
            MonsterCard[] monstersToRemove = combat.Monsters.ToArray();

            foreach (MonsterCard monster in monstersToRemove)
            {
                combat.RemoveMonster(monster, false);
            }
        }
    }
}
