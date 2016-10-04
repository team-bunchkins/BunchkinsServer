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

namespace Bunchkins.Domain.Core.GameStates
{
    class DrawState : IGameState
    {
        public IGameState HandleInput(Player player, Input input)
        {
            throw new NotImplementedException();
        }

        public void Initialize(Game game)
        {
            DoorCard card = game.DrawDoorCard();
            if( card is MonsterCard )
            {
                //COMBAT DAT MONSTER
            }
            else if ( card is CurseCard )
            {
                //CURSE DAT PLAYA PLAYA
                //COMBAT DAT MONSTER???
            }
            else
            {
                //ADD TO HAND
                game.ActivePlayer.Hand.Add(card);

                //COMBAT DAT MONSTER???
            }
        }

        public void PlayCard(Player player, ITarget target, Card card)
        {
            throw new NotImplementedException();
        }
    }
}
