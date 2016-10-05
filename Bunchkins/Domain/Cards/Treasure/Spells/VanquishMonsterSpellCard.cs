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
        public void Cast(CombatState combat)
        {
            // TODO: How do para-meter for RemoveMonster?
            //combat.RemoveMonster();
        }
    }
}
