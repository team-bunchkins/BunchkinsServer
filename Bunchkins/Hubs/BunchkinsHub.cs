using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Bunchkins.Domain.Core;
using Bunchkins.Domain.Players;
using Microsoft.AspNet.SignalR.Hubs;

namespace Bunchkins.Hubs
{
    [HubName("bunchkinsHub")]
    public class BunchkinsHub : Hub
    {
        #region Connection management

        public override Task OnConnected()
        {
            // Reconnect player if already logged on

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

        public void CreateGame(string playerName)
        {
            Player player;
            
            if (!GameManager.Instance.Players.Any(x => x.Name == playerName))
            {
                player = new Player
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId
                };

                Game game = GameManager.Instance.CreateGame(player);

                Groups.Add(player.ConnectionId, game.GameId.ToString());
                Clients.Caller.callerJoined(game.GameId);
            }
            else
            {
                Clients.Caller.displayError("Username already exists.");
                return;
            }
        }

        public void JoinGame(string name, Guid gameId)
        {
            Player player;

            // Check if username already exists
            if (!GameManager.Instance.Players.Any(x => x.Name == name))
            {
                player = new Player
                {
                    Name = name,
                    ConnectionId = Context.ConnectionId
                };
            } else
            {
                Clients.Caller.displayError("Username already exists.");
                return;
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

                Clients.Group(game.GameId.ToString()).playerJoined(player, Context.ConnectionId);
                Groups.Add(player.ConnectionId, game.GameId.ToString());
                Clients.Caller.callerJoined(gameId, game.Players.Where(p => p.Name != name).Select(p => new { name = p.Name }));
            }
        }

        public void StartGame(Guid gameId)
        {
            Game game = GetGame(gameId);

            if (game.State == null && game.Players.Any(p => p.ConnectionId == Context.ConnectionId))
            {
                game.Start();
            }
        }


        public void Pass(Guid gameId, string playerName)
        {
            var game = GetGame(gameId);
            var player = GetPlayer(playerName);

            if (game != null && game.ActivePlayer.ConnectionId == Context.ConnectionId)
            {
                game.HandleInput(player, Input.PASS);

                // Notify all clients in group of pass
                Clients.Group(game.GameId.ToString()).passed(player);
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
    }
}