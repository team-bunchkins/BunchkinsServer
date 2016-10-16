using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door.Monsters;
using static Bunchkins.Domain.Core.Input;

namespace Bunchkins.Domain.Core.GameStates
{
    class DrawState : GameState
    {
        public DrawState(Game game) : base(game)
        {
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
                Game.SetState(new EndState(Game));
            }
        }

    }
}
