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
    class EndState : GameState
    {
        public EndState(Game game) : base(game)
        {
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == PROCEED)
            {
                Game.SetState(new StartState(Game));
            }
        }

    }
}
