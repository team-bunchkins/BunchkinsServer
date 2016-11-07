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
            // Context.QueryString[2] == {[username, value]}
            // {[ key, value ]}

            // check for username in QueryString
            // if exists then reassign player to new connection
            // send all the current info to that client 
            
            if (Context.QueryString.Any(x => x.Key == "username") && Context.QueryString.Where(x => x.Key == "username").First().Value != "")
            {
                var player = GetPlayer(Context.QueryString.Where(x => x.Key == "username").First().Value);

                if (player != null)
                {
                    player.ConnectionId = Context.ConnectionId;
                    var game = GetGame(player);

                    // TODO: Implement on client side to notify others
                    // Clients.Group(game.GameId.ToString()).userReconnected(player.Name);
                    Groups.Add(player.ConnectionId, game.GameId.ToString());

                    // Send game info if game is currently active
                    // NOTE: If game hadn't started, player is removed on disconnect
                    if (game.State != null)
                    {
                        // Send game info to client
                        var gameDTO = new
                        {
                            GameId = game.GameId,
                            ActivePlayer = game.ActivePlayer.Name
                        };

                        var playerDTO = new
                        {
                            Name = player.Name,
                            Level = player.Level,
                            CombatPower = player.CombatPower,
                            Hand = player.Hand,
                            EquippedCards = new
                            {
                                Headgear = player.EquippedCards.Where(e => e.Slot == "Headgear").SingleOrDefault(),
                                Armor = player.EquippedCards.Where(e => e.Slot == "Armor").SingleOrDefault(),
                                Footgear = player.EquippedCards.Where(e => e.Slot == "Footgear").SingleOrDefault(),
                                Weapons = player.EquippedCards.Where(e => e.Slot == "1Hand" || e.Slot == "2Hands")
                            }
                        };

                        var opponentsDTO = game.Players.Where(p => p.Name != player.Name).Select(o => new
                        {
                            Name = o.Name,
                            Level = o.Level,
                            CombatPower = o.CombatPower,
                            HandSize = o.Hand.Count(),
                            EquippedCards = o.EquippedCards
                        });

                        Clients.Caller.setGameInfo(gameDTO, playerDTO, opponentsDTO);
                        UpdateState(game, game.State);
                    }
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Check if disconnected client was in a game
            if (Context.QueryString.Any(x => x.Key == "username") && Context.QueryString.Where(x => x.Key == "username").First().Value != "")
            {
                var player = GetPlayer(Context.QueryString.Where(x => x.Key == "username").First().Value);

                if (player != null)
                {
                    player.ConnectionId = Context.ConnectionId;
                    var game = GetGame(player);

                    // Remove player from game if game hasn't started
                    if (game.State == null)
                    {
                        GameManager.Instance.RemovePlayer(game, player);
                    }
                    // TODO: Maybe put timeout, remove user from active game once time is up
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.

            /* string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }
            */

            return base.OnReconnected();
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

        public void LeaveGame(Guid gameId, String playerName)
        {
            Game game = GetGame(gameId);
            Player player = GetPlayer(playerName);

            // Check if game exists for player to be removed from
            if (game == null)
            {
                Clients.Caller.displayError("Could not find game.");
                return;
            }
            else
            {
                // Remove player from game and game manager
                Groups.Remove(player.ConnectionId, game.GameId.ToString());
                Clients.Group(game.GameId.ToString()).playerLeft(player.Name);
                GameManager.Instance.RemovePlayer(game, player);
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

            Card card = GetCard(cardId, player);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                if (card == null)
                {
                    Clients.Caller.displayError("Could not find card.");
                }
                else if ((game.State is CombatState && !(card is EquipmentCard)) || (!(game.State is CombatState) && !(card is ICombatSpell))) {
                    game.PlayCard(player, target, card);
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

        public void Discard(Guid gameId, string playerName, int cardId)
        {
            Game game = GetGame(gameId);
            Player player = GetPlayer(playerName);
            Card card = GetCard(cardId, player);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                if (card == null)
                {
                    Clients.Caller.displayError("Could not find card.");
                }
                else
                {
                    player.Discard(card);
                }

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

        internal static void UpdateState(Game game, GameState state)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();

            if (state is StartState)
            {
                var stateDTO = new { Name = state.GetType().Name };
                hubContext.Clients.Group(game.GameId.ToString()).updateState(stateDTO);
            }
            else if (state is DrawState)
            {
                var stateDTO = new
                {
                    Name = state.GetType().Name,
                    Card = ((DrawState)state).CardDrawn
                };
                hubContext.Clients.Group(game.GameId.ToString()).updateState(stateDTO);
            }
            else if (state is CombatState)
            {
                UpdateCombatState(game, (CombatState)state);
            }
            else if (state is EndState)
            {
                var stateDTO = new
                {
                    Name = state.GetType().Name,
                    NumCards = ((EndState)state).NumCards,
                    CombatResults = ((EndState)state).CombatResults
                };
                hubContext.Clients.Group(game.GameId.ToString()).updateState(stateDTO);
            }
        }

        internal static void UpdateCombatState(Game game, CombatState combat)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).updateState(
                new
                {
                    Name = combat.GetType().Name,
                    Monsters = combat.Monsters,
                    MonsterCombatPower = combat.Monsters.Sum(m => m.Level),
                    PlayerCombatBonus = combat.PlayerCombatBonus,
                    MonsterCombatBonus = combat.MonsterCombatBonus,
                    PlayersPassed = combat.PlayersPassed.Select(p => p.Name)
                });
        }

        internal static void UpdateActivePlayer(Game game)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).updateActivePlayer(game.ActivePlayer.Name);
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
                    EquippedCards = new
                    {
                        Headgear = player.EquippedCards.Where(e => e.Slot == "Headgear").SingleOrDefault(),
                        Armor = player.EquippedCards.Where(e => e.Slot == "Armor").SingleOrDefault(),
                        Footgear = player.EquippedCards.Where(e => e.Slot == "Footgear").SingleOrDefault(),
                        Weapons = player.EquippedCards.Where(e => e.Slot == "1Hand" || e.Slot == "2Hands")
                    }
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
            hubContext.Clients.Group(game.GameId.ToString()).updateLevel(player.Name, player.Level, player.CombatPower);
        }

        internal static void UpdateLevel(Player player)
        {
            Game game = GetGame(player);
            UpdateLevel(game, player);
        }

        internal static void CardPlayed(Game game, Player player, Player target, Card card)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).cardPlayed(player.Name, target.Name, card);
        }

        //internal static void EndCombatState(Game game)
        //{
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
        //    hubContext.Clients.Group(game.GameId.ToString()).endCombatState();
        //}

        internal static void Winzor(Game game, Player player)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<BunchkinsHub>();
            hubContext.Clients.Group(game.GameId.ToString()).winzor(player);

            foreach (Player p in game.Players)
            {
                hubContext.Groups.Remove(p.ConnectionId, game.GameId.ToString());
            }

            GameManager.Instance.RemoveGame(game);
        }

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

        private Card GetCard(int cardId, Player player)
        {
            // Check if card is in player's hand
            if (player.Hand.Any(c => c.CardId == cardId))
            {
                // Return card that is in player's hand
                return player.Hand.Where(c => c.CardId == cardId)
                    .SingleOrDefault();
            }
            else
            {
                // Return card that is equipped on player
                // Returns null if neither conditions are met through SingleOrDefault.
                return player.EquippedCards.Where(c => c.CardId == cardId)
                    .SingleOrDefault();

            }
        }

        #endregion
    }
}