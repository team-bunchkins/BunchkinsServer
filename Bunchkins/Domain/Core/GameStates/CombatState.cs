using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door.Monsters;

namespace Bunchkins.Domain.Core.GameStates
{
    class CombatState : GameState
    {
        private List<MonsterCard> _monsters;
        public CombatState(Game game, MonsterCard monster) : base(game)
        {
            _monsters = new List<MonsterCard>();
            _monsters.Add(monster);
        }

        public override void HandleInput(Player player, Input input)
        {
            throw new NotImplementedException();
        }

        public override void PlayCard(Player player, ITarget target, Card card)
        {
            throw new NotImplementedException();
        }
    }
}
