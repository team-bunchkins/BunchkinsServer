using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Door;
using Bunchkins.Domain.Cards.Door.Monsters;
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
        public Random RandomGenerator = new Random();
        public Player ActivePlayer
        {
            get
            {
                return Players.SingleOrDefault(p => p.IsActive);
            }
        }

        public void Start()
        {
            Players.First().IsActive = true;
            mPlayerIterator = CreatePlayerIterator().GetEnumerator();

            State = new StartState(this);
        }

        public void NextPlayer()
        {
            ActivePlayer.IsActive = false;
            mPlayerIterator.MoveNext();
            mPlayerIterator.Current.IsActive = true;
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
                bool isInHand = false;
                DoorCard card;

                do
                {
                    card = db.Cards.OfType<DoorCard>().GetRandomElement(c => c.CardId);
                    isInHand = false;

                    // check whether card already exists in players' hand/equips
                    if (Players.Exists(p => p.Hand.Contains(card)))
                    {
                        isInHand = true;
                    }
                } while (isInHand);

                return card;
            }
        }

        public TreasureCard DrawTreasureCard()
        {
            using (var db = new BunchkinsDataContext())
            {
                bool isInHand = false;
                TreasureCard card;

                do
                {
                    card = db.Cards.OfType<TreasureCard>().GetRandomElement(c => c.CardId);
                    isInHand = false;

                    // check whether card already exists in players' hand/equips
                    if (Players.Exists(p => p.Hand.Contains(card)) || Players.Exists(p => p.EquippedCards.Contains(card)))
                    {
                        isInHand = true;
                    }
                } while (isInHand);

                return card;
            }
        }
        public MonsterCard DrawMonsterCard()
        {
            using (var db = new BunchkinsDataContext())
            {
                return db.Cards.OfType<MonsterCard>().GetRandomElement(c => c.CardId);
            }
        }

    }
}
