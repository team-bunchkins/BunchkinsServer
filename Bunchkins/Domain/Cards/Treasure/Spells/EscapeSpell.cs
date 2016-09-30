using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards.Door.Monsters;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class EscapeSpell : TreasureSpell, ICombatSpell
    {
        public void Cast(Player player, Monster[] monsters)
        {
            throw new NotImplementedException();
            //Make monsters in combat go away
        }
    }
}
