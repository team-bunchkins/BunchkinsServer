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
        public GameState State { get; private set; }
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
            Players.First().IsActive = true;
            State = new StartState(this);
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
            State.HandleInput(player, input);
        }

        public void SetState(GameState state)
        {
            State = state;
        }

        public DoorCard DrawDoorCard()
        {
            using (var db = new BunchkinsDataContext())
            {
                return db.Cards.OfType<DoorCard>().GetRandomElement(c => c.CardId);
                // TODO: check whether card already exists in players' hand
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
