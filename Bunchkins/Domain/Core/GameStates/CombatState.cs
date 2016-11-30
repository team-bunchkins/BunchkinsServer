using System.Collections.Generic;
using System.Linq;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Players;
using Bunchkins.Domain.Cards.Door.Monsters;
using static Bunchkins.Domain.Core.Input;
using Bunchkins.Hubs;
using System.Text.RegularExpressions;

namespace Bunchkins.Domain.Core.GameStates
{
    public class CombatState : GameState
    {
        public List<MonsterCard> Monsters { get; private set; }
        public int MonsterCombatBonus { get; private set; }
        public int PlayerCombatBonus { get; private set; }
        public List<Player> PlayersPassed { get; set; }
        public int PileOfTreasures { get; private set; }

        public CombatState(Game game, MonsterCard monster) : base(game)
        {
            Monsters = new List<MonsterCard>();
            Monsters.Add(monster);

            PlayersPassed = new List<Player>();
            BunchkinsHub.UpdateCombatState(Game, this);
        }

        public override void HandleInput(Player player, Input input)
        {
            if (input == RUN)
            {
                string results = "";
                foreach (MonsterCard monster in Monsters)
                {
                    int diceRoll = Game.RandomGenerator.Next(1, 7);
                    if (diceRoll < 5 && Game.ActivePlayer.Level >= monster.MinPursuitLevel)
                    {
                        // Bad Stuff!
                        monster.BadStuff(Game.ActivePlayer);
                        if (results != "") results += "\n";
                        results += "Could not escape " + monster.Name + "!";

                        Regex regex = new Regex("^.*Bad Stuff:");
                        string consequences = regex.Replace(monster.Description.ToString(), "");
                        if (consequences != null && consequences.Length > 0)
                        {
                            results += "\n" + consequences;
                        }
                    }
                    else
                    {
                        // Player escaped!
                        if (results != "") results += "\n";
                        results += "Escaped from " + monster.Name + "!";
                    }
                }
                Game.SetState(new EndState(Game, 0, results));
                // BunchkinsHub.EndCombatState(Game);
            }
            else if (input == PASS && !PlayersPassed.Any(p => p.Name == player.Name))
            {
                PlayersPassed.Add(player);
                BunchkinsHub.UpdateCombatState(Game, this);
            }
            else if ((input == PROCEED && PlayersPassed.Count() == Game.Players.Count() - 1) && CanPlayerWinCombat())
            {
                if ((player.Level) + (Monsters.Sum(m => m.LevelGain)) >= 10)
                {
                    BunchkinsHub.Winzor(Game, player);
                }
                else
                {
                    int treasures = Monsters.Sum(m => m.TreasureGain) + PileOfTreasures;

                    string results = "";
                    if (Monsters.Count() > 1)
                    {
                        results = "Defeated monsters!";
                    }
                    else
                    {
                        results = "Defeated monster!";
                    }

                    // BunchkinsHub.EndCombatState(Game);
                    player.IncreaseLevel(Monsters.Sum(m => m.LevelGain));
                    Game.LootTreasure(treasures);
                    Game.SetState(new EndState(Game, treasures, results));
                }
                // TODO: Error if player cannot defeat monster
            }
        }

        public override void PlayCard(Player player, Player target, Card card)
        {
            if (card is IAnytimeSpell)
            {
                ((IAnytimeSpell)card).Cast(target);
                player.Discard(card);
                BunchkinsHub.UpdateCombatState(Game, this);
            }
            else if (card is ICombatSpell)
            {
                ((ICombatSpell)card).Cast(this);
                player.Discard(card);
            }
        }

        public void AddMonsterCombatBonus(int bonus)
        {
            MonsterCombatBonus += bonus;
            BunchkinsHub.UpdateCombatState(Game, this);
        }

        public void AddPlayerCombatBonus(int bonus)
        {
            PlayerCombatBonus += bonus;
            BunchkinsHub.UpdateCombatState(Game, this);
        }

        public void AddMonster()
        {
            MonsterCard monster = Game.DrawMonsterCard();
            Monsters.Add(monster);

            BunchkinsHub.UpdateCombatState(Game, this);
        }

        public void AddMonsterTreasureBonus(int bonus)
        {
            PileOfTreasures += bonus;
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
                Game.LootTreasure(PileOfTreasures);
                // BunchkinsHub.EndCombatState(Game);
                Game.SetState(new EndState(Game, PileOfTreasures, "Escaped from combat with the loot!"));
            }
            else if (Monsters.Count == 0)
            {
                // BunchkinsHub.EndCombatState(Game);
                Game.SetState(new EndState(Game, 0, "Escaped from combat!"));
            }
            else
            {
                BunchkinsHub.UpdateCombatState(Game, this);
            }

        }

        public bool CanPlayerWinCombat()
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
