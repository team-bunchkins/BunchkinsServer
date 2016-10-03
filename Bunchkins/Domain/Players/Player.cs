using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Treasure.Equipment;
using Bunchkins.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunchkins.Domain.Players
{
    public class Player : ITarget
    {
        public int PlayerId { get; set; }

        public int? GameId { get; set; }

        public int Level { get; set; }

        public virtual ICollection<Card> Hand { get; set; }

        public virtual ICollection<EquippedCard> EquippedCards { get; set; }

        public virtual Game Game { get; set; }

        public bool IsActive { get; set; }

        public int GetCombatPower()
        {
            return EquippedCards.Sum(c => c.EquipmentCard.Bonus) + Level;
        }

        public void Die()
        {
            Hand.Clear();

            //TODO: Write logic to clear players equipped cards when they die.
            //foreach (var card in Equipped)
            //{
            //    if(card.GetType() != typeof(ClassCard))
            //}
        }

        public void DecreaseLevel(int levels)
        {
            if ((Level - levels) < 1)
            {
                Level = 1;
            }
            else
            {
                Level = Level - levels;
            }
        }

        public void IncreaseLevel(int levels)
        {
            Level += levels; 
        }

        public void LoseArmor(List<string> slots)
        {
            foreach(var slot in slots)
            {
                foreach(var card in EquippedCards.Select(ec => ec.EquipmentCard))
                {
                    if(card.Slot == slot)
                    {
                        // TODO: Lose armor
                    }
                }
            }
        }

    }
}
