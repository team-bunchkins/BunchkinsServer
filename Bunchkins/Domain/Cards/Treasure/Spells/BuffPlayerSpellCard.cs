using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using System;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class BuffPlayerSpellCard : TreasureSpellCard, ICombatSpell
    {
        public int CombatPower { get; set; }
        //TODO: Add target parameter to Cast method
        public void Cast(CombatState combat)
        {
            combat.AddPlayerCombatBonus(CombatPower);
        }
    }
}
