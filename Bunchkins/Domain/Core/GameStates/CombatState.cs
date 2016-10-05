using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door.Monsters;
using static Bunchkins.Domain.Core.Input;

namespace Bunchkins.Domain.Core.GameStates
{
    public class CombatState : GameState
    {
        public List<MonsterCard> Monsters { get; private set; }
        public int MonsterCombatBonus { get; private set; }
        public int PlayerCombatBonus { get; private set; }
        public int PlayersPassed { get; set; }
        public int PileOfTreasures { get; private set; }

        public CombatState(Game game, MonsterCard monster) : base(game)
        {
            Monsters = new List<MonsterCard>();
            Monsters.Add(monster);
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == RUN)
            {
                Game.RandomGenerator.Next(1, 7);
                // Bad stuff
            }
            else if (input == PASS)
            {
                // TODO: MAYBE it should be a list of passed players instead?
                PlayersPassed++;
                
                if ((PlayersPassed == Game.Players.Count()) && (player == Game.ActivePlayer)) {
                    int treasures = Monsters.Sum(m => m.TreasureGain);
                    Game.SetState(new TreasureLootState(Game, treasures));
                }
            }
        }

        public override void PlayCard(Player player, Player target, Card card)
        {
            if (card is IAnytimeSpell)
            {
                ((IAnytimeSpell)card).Cast(target);
                player.Hand.Remove(card);
            }
            else if (card is ICombatSpell)
            {
                ((ICombatSpell)card).Cast(this);
                player.Hand.Remove(card);
            }
        }

        public void AddMonsterCombatBonus(int bonus)
        {
            MonsterCombatBonus += bonus;
        }

        public void AddPlayerCombatBonus(int bonus)
        {
            PlayerCombatBonus += bonus;
        }

        public void AddMonster()
        {
            MonsterCard monster = Game.DrawMonsterCard();
            Monsters.Add(monster);
        }

        public void RemoveMonster(MonsterCard monster, bool isLootable)
        {
            if (isLootable)
            {
                PileOfTreasures += monster.TreasureGain;
            }

            Monsters.Remove(monster);
            
            if (Monsters.Count == 0 && isLootable)
            {
                Game.SetState(new TreasureLootState(Game, PileOfTreasures));
            }
            else if (Monsters.Count == 0)
            {
                Game.SetState(new TreasureLootState(Game, 0));
            }
            
        }

        public bool CanPlayerWin()
        {
            int playerPower = Game.ActivePlayer.CombatPower + PlayerCombatBonus;
            int monsterPower = Monsters.Sum(m => m.Level) + MonsterCombatBonus;
            if (playerPower > monsterPower)
            {
                return true;
            }
            return false;
        }
    }
}
