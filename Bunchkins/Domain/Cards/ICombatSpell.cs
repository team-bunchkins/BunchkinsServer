using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Cards
{
    public interface ICombatSpell
    {
        // TODO: HOW TO TARGET MONSTER?!?!?!?!?!?!?
        void Cast(CombatState combatState);
    }
}
