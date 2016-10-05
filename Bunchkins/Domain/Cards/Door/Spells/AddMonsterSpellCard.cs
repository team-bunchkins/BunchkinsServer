using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Players;
using Bunchkins.Domain.Core.GameStates;

namespace Bunchkins.Domain.Cards.Door.Spells
{
    class AddMonsterSpellCard : DoorSpell, ICombatSpell
    {
        public void Cast(CombatState combat)
        {
            combat.AddMonster();
        }
    }
}
