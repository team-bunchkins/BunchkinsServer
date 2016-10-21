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
            Games.Add(game);
            game.Players.Add(player);
            Players.Add(player);
            return game;
        }

        public bool AddPlayerToGame(Player player, Game game)
        {
            game.Players.Add(player);
            Players.Add(player);
            return true;
        }

        public void RemoveGame(Game game)
        {
            //Remove players from list of players
            foreach (Player player in game.Players)
            {
                Players.Remove(player);
            }
            
            //Remove game from list of games
            Games.Remove(game);
        }
    }
}