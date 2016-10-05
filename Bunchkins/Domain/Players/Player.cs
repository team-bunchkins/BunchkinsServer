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

        public int Level { get; private set; }

        public List<Card> Hand { get; set; }

        public List<EquipmentCard> EquippedCards { get; set; }

        public bool IsActive { get; set; }

        public int CombatPower
        {  
            get
            {
                return EquippedCards.Sum(c => c.Bonus) + Level;
            }
        }

        public void Die()
        {
            Hand.Clear();

            // TODO: Do not remove race/class cards
            EquippedCards.Clear();
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

        public void EquipItem(EquipmentCard equipment)
        {
            EquippedCards.RemoveAll(e => e.Slot == equipment.Slot);
            EquippedCards.Add(equipment);
        }

        public void RemoveEquip(List<string> slots)
        {
            foreach(var slot in slots)
            {
                EquippedCards.RemoveAll(e => e.Slot == slot);
            }
        }

    }
}
