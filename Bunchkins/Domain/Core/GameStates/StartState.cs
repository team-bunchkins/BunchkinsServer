using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using static Bunchkins.Domain.Core.Input;

namespace Bunchkins.Domain.Core.GameStates
{
    class StartState : IGameState
    {
        public IGameState HandleInput(Player player, Input input)
        {
            if (input == PASS)
            {
                return new DrawState();
            }
            else
            {
                return null;
            }
        }

        public void Initialize(Game game)
        {
            throw new NotImplementedException();
        }

        public void PlayCard(Player player, ITarget target, Card card)
        {
            throw new NotImplementedException();
        }
    }
}
