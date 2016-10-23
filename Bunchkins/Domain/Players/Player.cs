using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Treasure.Equipment;
using Bunchkins.Domain.Core;
using Bunchkins.Hubs;
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

        public bool Reconnecting { get; set; }

        public Player()
        {
            Hand = new List<Card>();
            EquippedCards = new List<EquipmentCard>();
            Level = 1;
        }

        public void Die()
        {
            Hand.Clear();

            // TODO: Do not remove race/class cards
            EquippedCards.Clear();
            BunchkinsHub.UpdatePlayer(this);
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
            BunchkinsHub.UpdateLevel(this);
        }

        public void IncreaseLevel(int levels)
        {
            if ((Level + levels) <= 9)
            {
                Level += levels;
            }
            else
            {
                Level = 9;
            }
            BunchkinsHub.UpdateLevel(this);
        }

        public void EquipItem(EquipmentCard equipment)
        {
            if (equipment.Slot == "2Hands")
            {
                // Check if player equips two-handed weapon,
                // player should discard all one-handed weapons
                EquippedCards.RemoveAll(e => e.Slot == "1Hand");
                EquippedCards.RemoveAll(e => e.Slot == equipment.Slot);
            }
            else if (equipment.Slot == "1Hand")
            {
                // Check if player equips one-handed weapon, 
                // player should discard two handed weapon
                EquippedCards.RemoveAll(e => e.Slot == "2Hands");

                // If player has more than two one-handed weapon,
                // replace the weaker one
                if (EquippedCards.Count(e => e.Slot == "1Hand") > 1)
                {
                    EquippedCards.Remove(EquippedCards.Where(e => e.Slot == "1Hand").OrderBy(e => e.Bonus).First());
                }
            }
            else
            {
                // Remove all cards in the current slot
                EquippedCards.RemoveAll(e => e.Slot == equipment.Slot);
            }

            // Add equipment to player
            EquippedCards.Add(equipment);
            BunchkinsHub.UpdatePlayer(this);
        }

        public void RemoveEquip(string slot)
        {
            EquippedCards.RemoveAll(e => e.Slot == slot);
            BunchkinsHub.UpdatePlayer(this);
        }

        public void RemoveAllEquips()
        {
            EquippedCards.Clear();
            BunchkinsHub.UpdatePlayer(this);
        }

        public void AddHandCard(Card card)
        {
            Hand.Add(card);
            BunchkinsHub.UpdateHand(this);
        }

        public void Discard(Card card)
        {
            if (Hand.Contains(card))
            {
                Hand.Remove(card);
            }
            else if (EquippedCards.Contains(card))
            {
                EquippedCards.Remove((EquipmentCard) card);
            }
            BunchkinsHub.UpdatePlayer(this);   
        }

        public void RemoveRandomHandCards(int numCards)
        {
            var rand = new Random();

            for (int i = 0; i < numCards && Hand.Count() > 0; i++)
            {
                var index = (int)(rand.NextDouble() * Hand.Count());
                Hand.RemoveAt(index);
                // Hand.GetRandomElement(c => c.CardId);
            }

            BunchkinsHub.UpdatePlayer(this);
        }

    }
}
