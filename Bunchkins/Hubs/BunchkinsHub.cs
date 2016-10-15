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

namespace Bunchkins.Hubs
{
    [HubName("bunchkinsHub")]
    public class BunchkinsHub : Hub
    {
        #region Connection management

        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
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

                    // _connections.Add(player.Name, player.ConnectionId);

                    Clients.Caller.updateSelf(player.Name);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.
            if (stopCalled)
            {
                var player = GameManager.Instance.Players
                    .Where(x => x.Name == Context.User.Identity.Name)
                    .SingleOrDefault();
                Game game = new Game();
                game.Players.Remove(player);
                // _connections.Remove(name, Context.ConnectionId);
                Console.WriteLine(String.Format("Client {0} explicitly closed connection.", Context.ConnectionId));

            }
            else
            {
                Console.WriteLine(String.Format("Client {0} timed out.", Context.ConnectionId));
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            // Add your own code here.
            // For example: in a chat application, you might have marked the
            // user as offline after a period of inactivity; in that case 
            // mark the user as online again.

            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

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

            // update hands
            foreach (Player player in game.Players)
            {
                Clients.Client(player.ConnectionId).updateHand(player.Hand);
            }
        }

        public void PlayCard(Guid gameId, string playerName, string targetName, Card card)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);
            var target = GetPlayer(targetName);

            if (game != null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                if (!(card is ICombatSpell) || game.State is CombatState) {
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

        protected void UpdateHand(Player player)
        {
            Clients.Client(player.ConnectionId).updateHand(player.Hand);
        }

        protected void UpdateEquips(Player player)
        {
            Clients.Client(player.ConnectionId).updateEquips(player.EquippedCards);
        }

        protected void UpdateState(Game game, Player player)
        {
            Clients.Client(player.ConnectionId).updateState(game.State.ToString());
        }

        #endregion

        #region Helper functions

        private Game GetGame(Guid gameId)
        {
            return GameManager.Instance.Games
                .Where(x => x.GameId == gameId)
                .SingleOrDefault();
        }

        private Player GetPlayer(string name)
        {
            return GameManager.Instance.Players
                .Where(x => x.Name == name)
                .SingleOrDefault();
        }

        #endregion
    }
}