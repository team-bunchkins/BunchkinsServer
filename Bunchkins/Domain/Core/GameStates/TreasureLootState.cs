using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Core.GameStates
{
    class TreasureLootState : LootState
    {
        public TreasureLootState(Game game, int numTreasures) : base(game)
        {
            for (int i = 0; i < numTreasures; i++)
            {
                Game.ActivePlayer.AddHandCard(Game.DrawTreasureCard());
            }

            Game.SetState(new EndState(Game));
        }
    }
}
