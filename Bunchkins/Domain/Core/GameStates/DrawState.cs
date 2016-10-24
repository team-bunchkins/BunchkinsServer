using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door.Monsters;
using static Bunchkins.Domain.Core.Input;
using Bunchkins.Domain.Cards;

namespace Bunchkins.Domain.Core.GameStates
{
    class DrawState : GameState
    {
        public Card CardDrawn { get; private set; }
        public DrawState(Game game, Card card) : base(game)
        {
            CardDrawn = card;
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == FIGHT)
            {
                MonsterCard card = Game.DrawMonsterCard();
                Game.SetState(new CombatState(Game, card));
            }
            else if (input == PROCEED)
            {
                Game.LootDoor();
                Game.SetState(new EndState(Game, 1));
            }
        }

    }
}
