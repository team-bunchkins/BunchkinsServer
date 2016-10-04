using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Core.GameStates
{
    public abstract class GameState
    {
        protected Game Game { get; private set; }

        public GameState(Game game)
        {
            Game = game;
        }

        public abstract void HandleInput(Player player, Input input);
        public virtual void PlayCard(Player player, ITarget target, Card card)
        {
        }
    }
}
