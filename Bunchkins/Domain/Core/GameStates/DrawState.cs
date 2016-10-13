using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door;
using Bunchkins.Domain.Cards.Door.Monsters;
using Bunchkins.Domain.Cards.Door.Spells.Curses;
using static Bunchkins.Domain.Core.Input;

namespace Bunchkins.Domain.Core.GameStates
{
    class DrawState : GameState
    {
        public DrawState(Game game) : base(game)
        {
            DoorCard card = game.DrawDoorCard();
            if (card is MonsterCard)
            {
                Game.SetState(new CombatState(Game, (MonsterCard) card));
            }
            else if (card is CurseCard)
            {
                ((CurseCard)card).Cast(game.ActivePlayer);
            }
            else
            {
                //ADD TO HAND
                game.ActivePlayer.Hand.Add(card);
            }
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
                Game.SetState(new DoorLootState(Game));
            }
        }

    }
}
