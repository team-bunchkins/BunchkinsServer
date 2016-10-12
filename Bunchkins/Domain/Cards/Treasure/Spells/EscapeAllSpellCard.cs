using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using System;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class EscapeAllSpellCard : TreasureSpellCard, ICombatSpell
    {
        // Removes all monsters from combat, monsters' treasure is not lootable
        public void Cast(CombatState combat)
        {
            //Make monsters in combat go away
            foreach (MonsterCard monster in combat.Monsters)
            {
                combat.RemoveMonster(monster, false);
            }
        }
    }
}
