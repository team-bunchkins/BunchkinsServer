using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Players;
using static Bunchkins.Domain.Core.Input;
using Bunchkins.Domain.Cards.Door;
using Bunchkins.Domain.Cards.Door.Spells.Curses;
using Bunchkins.Domain.Cards.Door.Monsters;

namespace Bunchkins.Domain.Core.GameStates
{
    public class StartState : GameState
    {
        public StartState(Game game) : base(game)
        {
            game.NextPlayer();
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == PROCEED)
            {
                KnockOnDoor();
            }
        }

        public void KnockOnDoor()
        {
            DoorCard card = Game.DrawDoorCard();
            if (card is MonsterCard)
            {
                Game.SetState(new CombatState(Game, (MonsterCard)card));
            }
            else if (card is CurseCard)
            {
                ((CurseCard)card).Cast(Game.ActivePlayer);
                Game.SetState(new DrawState(Game));
            }
            else
            {
                //ADD TO HAND
                Game.ActivePlayer.AddHandCard(card);
                Game.SetState(new DrawState(Game));
            }
            
        }

    }
}
