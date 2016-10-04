using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Door;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Players;
using Bunchkins.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bunchkins.Domain.Core
{
    public class Game
    {
        public int GameId { get; set; }
        public IGameState State { get; private set; }
        public IEnumerator<Player> mPlayerIterator;
        public List<Player> Players { get; set; }

        private Random mRandomGenerator = new Random();
        public Player ActivePlayer
        {
            get
            {
                return Players.SingleOrDefault(p => p.IsActive);
            }
        }

        public void NextPlayer()
        {
            // TODO: Move mPlayerIterator instantiation to game initialization
            if (mPlayerIterator == null)
            {
                mPlayerIterator = CreatePlayerIterator().GetEnumerator();
            }

            ActivePlayer.IsActive = false;
            mPlayerIterator.MoveNext();
            mPlayerIterator.Current.IsActive = true;
        }

        public void Start()
        {
            State = new StartState();
        }

        IEnumerable<Player> CreatePlayerIterator()
        {
            while (true)
            {
                foreach (Player player in Players)
                {
                    yield return player;
                }
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
            using (var db = new BunchkinsDataContext())
            {
                return db.Cards.OfType<DoorCard>().GetRandomElement(c => c.CardId);
            }
        }

        public TreasureCard DrawTreasureCard()
        {
            using (var db = new BunchkinsDataContext())
            {
                return db.Cards.OfType<TreasureCard>().GetRandomElement(c => c.CardId);
            }
        }
    }
}
