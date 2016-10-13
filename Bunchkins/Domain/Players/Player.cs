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

        public string ConnectionId { get; set; }
        
        public string Name { get; set; }

        public int Level { get; private set; }

        public List<Card> Hand { get; private set; }

        public List<EquipmentCard> EquippedCards { get; private set; }

        public bool IsActive { get; set; }

        public int CombatPower
        {  
            get
            {
                return EquippedCards.Sum(c => c.Bonus) + Level;
            }
        }

        public Player()
        {
            Hand = new List<Card>();
            EquippedCards = new List<EquipmentCard>();
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

        public void RemoveEquip(string slot)
        {
                EquippedCards.RemoveAll(e => e.Slot == slot); 
        }

        public void RemoveAllEquips()
        {
                EquippedCards.Clear();
        }

        public void RemoveHandCards(int numCards)
        {
            var rand = new Random();

            //TODO : While loop to check hand count is bigger than removal
            for (int i = 0; i < numCards; i++)
            {
                var index = (int)(rand.NextDouble() * Hand.Count());
                Hand.RemoveAt(index);
                // Hand.GetRandomElement(c => c.CardId);
            }
            
        }

    }
}
