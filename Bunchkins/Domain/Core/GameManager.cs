using Bunchkins.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bunchkins.Domain.Core
{
    public class GameManager
    {
        private static readonly GameManager _instance = new GameManager();

        private GameManager()
        {
            Players = new List<Player>();
            Games = new List<Game>();
        }

        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<Player> Players { get; set; }

        public List<Game> Games { get; set; }

        public Game CreateGame(Player player)
        {
            Game game = new Game();
            // Add new game to GameManager game list
            Games.Add(game);

            // Add new player to game
            game.Players.Add(player);

            // Add new player to GameManager player list
            Players.Add(player);
            return game;
        }

        public bool AddPlayerToGame(Player player, Game game)
        {
            game.Players.Add(player);
            Players.Add(player);
            return true;
        }

        public void RemovePlayer(Game game, Player player)
        {
            // Remove player from list in GameManager
            Players.Remove(player);

            // Remove player from game
            game.Players.Remove(player);

            // Check if player is the last one removed,
            // then game should be removed from GameManager
            if (game.Players.Count() < 1)
            {
                // "Nuke it! PEWWWW..." - Katie
                Games.Remove(game);
            }
        }
    }
}