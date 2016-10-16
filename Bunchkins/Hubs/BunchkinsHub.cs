using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Bunchkins.Domain.Core;
using Bunchkins.Domain.Players;
using Microsoft.AspNet.SignalR.Hubs;
using static Bunchkins.Domain.Core.Input;
using Bunchkins.Domain.Core.GameStates;
using Bunchkins.Domain.Cards;
using Bunchkins.Domain.Cards.Treasure.Equipment;
using Bunchkins.Infrastructure;

namespace Bunchkins.Hubs
{
    [HubName("bunchkinsHub")]
    public class BunchkinsHub : Hub
    {
        #region Connection management

        public override Task OnConnected()
        {
            // Reconnect player if already logged on?

            // TODO: Come back to this - does user retain any other identity info?
            if (!string.IsNullOrEmpty(Context.User.Identity.Name))
            {
                var player = GameManager.Instance.Players
                    .Where(x => x.Name == Context.User.Identity.Name)
                    .SingleOrDefault();

                if (player != null)
                {
                    player.ConnectionId = Context.ConnectionId;
                    Clients.Caller.updateSelf(player.Name);
                }
            }

            return base.OnConnected();
        }

        #endregion

        #region Incoming requests

        public void CreateGame(string playerName)
        {
            Player player;

            if (GameManager.Instance.Players.Any(x => x.Name == playerName))
            {
                Clients.Caller.displayError("Username already exists.");
                return;
            }
            else if (GameManager.Instance.Players.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                Clients.Caller.displayError("User is already in a game.");
                return;
            }
            else
            {
                player = new Player
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId,
                    // Use IsActive to indicate Host
                    IsActive = true
                };

                Game game = GameManager.Instance.CreateGame(player);

                Groups.Add(player.ConnectionId, game.GameId.ToString());
                Clients.Caller.callerJoined(game.GameId);
            }
        }

        public void JoinGame(string name, Guid gameId)
        {
            Player player;

            // Check if username already exists
            if (GameManager.Instance.Players.Any(x => x.Name == name))
            {
                Clients.Caller.displayError("Username already exists.");
                return;
            }
            else if (GameManager.Instance.Players.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                Clients.Caller.displayError("User is already in a game.");
                return;
            }
            else
            {
                player = new Player
                {
                    Name = name,
                    ConnectionId = Context.ConnectionId
                };
            }


            Game game = GetGame(gameId);

            if (game == null)
            {
                Clients.Caller.displayError("Could not find game.");
                return;
            }
            else if (game.State != null)
            {
                Clients.Caller.displayError("Game is already in progress!");
            }
            else
            {
                game.Players.Add(player);
                GameManager.Instance.Players.Add(player);

                Clients.Group(game.GameId.ToString()).playerJoined(new { Name = player.Name });
                Groups.Add(player.ConnectionId, game.GameId.ToString());
                Clients.Caller.callerJoined(gameId, game.Players.Where(p => p.Name != name).Select(p => new { Name = p.Name }));
            }
        }

        public void StartGame(Guid gameId)
        {
            Game game = GetGame(gameId);

            if (game.State == null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                game.Start();
            }

            Clients.Group(game.GameId.ToString()).gameStarted();
        }

        public void PlayCard(Guid gameId, string playerName, string targetName, int cardId)
        {
            Game game = GetGame(gameId);
            Player player = GetPlayer(playerName);
            Player target = GetPlayer(targetName);
            Card card = GetCard(cardId);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                if ((game.State is CombatState && !(card is EquipmentCard)) || (!(game.State is CombatState) && !(card is ICombatSpell))) {
                    game.State.PlayCard(player, target, card);
                }
                else
                {
                    Clients.Caller.displayError("This card cannot be played right now!");
                }
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        public void Discard(Guid gameId, string playerName, Card card)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                player.Discard(card);
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        public void Proceed(Guid gameId, string playerName)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.ActivePlayer.ConnectionId == Context.ConnectionId)
            {
                game.HandleInput(player, PROCEED);

                // Notify all clients in group of pass
                Clients.Group(game.GameId.ToString()).proceeded(player);
            }
            else if (game != null)
            {
                Clients.Caller.displayError("It is not your turn!");
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        public void Fight(Guid gameId, string playerName)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.ActivePlayer.ConnectionId == Context.ConnectionId && game.State is DrawState)
            {
                game.HandleInput(player, FIGHT);
            }
            else if (game != null)
            {
                Clients.Caller.displayError("It is not your turn!");
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        public void Run(Guid gameId, string playerName)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.ActivePlayer.ConnectionId == Context.ConnectionId && game.State is CombatState)
            {
                game.HandleInput(player, RUN);
            }
            else if (game != null)
            {
                Clients.Caller.displayError("It is not your turn!");
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        public void Pass(Guid gameId, string playerName)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId) && game.State is CombatState)
            {
                game.HandleInput(player, PASS);
            }
            else if (game != null)
            {
                Clients.Caller.displayError("You cannot do that now.");
            }
            else
            {
                Clients.Caller.displayError("Could not find game.");
            }
        }

        #endregion

        #region Outgoing requests

        internal static void UpdateState(Game game)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).stateChanged(game.State.GetType().Name);
        }

        internal static void UpdateActivePlayer(Game game)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).activePlayerChanged(game.ActivePlayer.Name);
        }

        internal static void UpdatePlayer(Game game, Player player)
        {
            // Update with hand info for updated player
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Client(player.ConnectionId).updatePlayer(
                new
                {
                    Name = player.Name,
                    Level = player.Level,
                    CombatPower = player.CombatPower,
                    Hand = player.Hand,
                    EquippedCards = player.EquippedCards
                });

            // Update with only hand size for other players
            List<string> OtherConnectionIds = game.Players.Select(p => p.ConnectionId).Where(c => c != player.ConnectionId).ToList();
            hubContext.Clients.Clients(OtherConnectionIds).updateOpponent(
                new
                {
                    Name = player.Name,
                    Level = player.Level,
                    CombatPower = player.CombatPower,
                    HandSize = player.Hand.Count(),
                    EquippedCards = player.EquippedCards
                });
        }

        internal static void UpdatePlayer(Player player)
        {
            Game game = GetGame(player);
            UpdatePlayer(game, player);
        }

        internal static void UpdateHand(Game game, Player player)
        {
            // Update with hand contents for updated player
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Client(player.ConnectionId).updateHand(player.Hand);

            // Update with only hand size for other players
            List<string> OtherConnectionIds = game.Players.Select(p => p.ConnectionId).Where(c => c != player.ConnectionId).ToList();
            hubContext.Clients.Clients(OtherConnectionIds).updateOpponentHand(player.Name, player.Hand.Count());
        }

        internal static void UpdateHand(Player player)
        {
            Game game = GetGame(player);
            UpdateHand(game, player);
        }

        internal static void UpdateLevel(Game game, Player player)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).updateLevel(player.Name, player.Level);
        }

        internal static void UpdateLevel(Player player)
        {
            Game game = GetGame(player);
            UpdateLevel(game, player);
        }

        internal static void UpdateCombatState(Game game, CombatState combat)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).updateCombatState(
                new {
                    Monsters = combat.Monsters,
                    MonsterCombatPower = combat.Monsters.Sum(m => m.Level),
                    PlayerCombatBonus = combat.PlayerCombatBonus,
                    MonsterCombatBonus = combat.MonsterCombatBonus,
                    PlayersPassed = combat.PlayersPassed.Select(p => p.Name)
                });
        }

        internal static void EndCombatState(Game game)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).endCombatState();
        }

        //internal static void UpdatePassedPlayers(Game game, List<Player> players)
        //{
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
        //    hubContext.Clients.Group(game.GameId.ToString()).updatePassedPlayers(players.Select(p => p.Name));
        //}

        #endregion

        #region Helper functions

        private Game GetGame(Guid gameId)
        {
            return GameManager.Instance.Games
                .Where(x => x.GameId == gameId)
                .SingleOrDefault();
        }

        private static Game GetGame(Player player)
        {
            return GameManager.Instance.Games
                .Where(g => g.Players.Contains(player))
                .SingleOrDefault();
        }

        private Player GetPlayer(string name)
        {
            return GameManager.Instance.Players
                .Where(x => x.Name == name)
                .SingleOrDefault();
        }

        private Card GetCard(int cardId)
        {
            using (var db = new BunchkinsDataContext())
            {
                return db.Cards.Where(c => c.CardId == cardId).SingleOrDefault();
            }
        }

        #endregion
    }
}