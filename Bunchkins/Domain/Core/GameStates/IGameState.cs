using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Core.GameStates
{
    public interface IGameState
    {
        // TODO: Decide if need to pass reference to game so implementation can check if player = activePlayer
        IGameState HandleInput(Player player, Input input);
        void PlayCard(Player player, ITarget target, Card card);
        void Initialize(Game game);
    }
}
