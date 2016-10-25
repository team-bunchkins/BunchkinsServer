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
        public int NumCards { get; private set; }
        public string CombatResults { get; private set; }

        public EndState(Game game, int numCards) : base(game)
        {
            NumCards = numCards;
        }

        // constructor called when entering EndState from combat
        public EndState(Game game, int numCards, string combatResults) : this(game, numCards)
        {
            CombatResults = combatResults;
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
