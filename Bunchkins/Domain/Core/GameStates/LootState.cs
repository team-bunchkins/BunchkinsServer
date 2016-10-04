using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Core.GameStates
{
    public abstract class LootState : GameState
    {
        public LootState(Game game) : base(game)
        {
        }

        public override void HandleInput(Player player, Input input)
        {
            throw new NotImplementedException();
        }
    }
}
