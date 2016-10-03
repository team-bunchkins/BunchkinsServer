using Bunchkins.Domain.Cards.Door;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bunchkins.Domain.Core
{
    public class Game
    {
        public int GameId { get; set; }
        public IGameState State { get; private set; }
        public virtual ICollection<Player> Players { get; set; }
        public Player ActivePlayer
        {
            get
            {
                return Players.SingleOrDefault(p => p.IsActive);
            }
        }

        public void HandleInput(Player player, Input input)
        {
            IGameState newState = State.HandleInput(player, input);
            if (newState != null)
            {
                InitializeState(newState);
            }
        }

        public void InitializeState(IGameState state)
        {
            // TODO: destroy current State?
            State = state;
            State.Initialize(this);
        }

        public DoorCard DrawDoorCard()
        {
            throw new NotImplementedException("Todo: implement DrawDoorCard");
        }
    }
}
