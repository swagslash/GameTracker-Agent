using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GameTracker_Core.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class GameDirectory
    {
        [JsonProperty("Directory")]
        private string _directory;

        [JsonProperty("Games")]
        private List<Game> _games;

        public GameDirectory()
        {
            _games = new List<Game>();
        }

        public void addGame(Game game)
        {
            _games.Add(game);
        }

        public IList<Game> GetGames()
        {
            return _games.AsReadOnly();
        }

        public string Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = value;
            }
        }

        public void RemoveGame(Game g)
        {
            _games.Remove(g);
        }
    }
}
