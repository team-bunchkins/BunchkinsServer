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
    public class StartState : GameState
    {
        public StartState(Game game) : base(game)
        {
            game.NextPlayer();
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == PROCEED)
            {
                Game.SetState(new DrawState(Game));
            }
        }

    }
}
