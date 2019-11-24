using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GameTracker_Core.Models
{
    [Serializable]
    public class GameDirectory
    {

        private string directory { get; set; }

        [JsonProperty("Games")]
        private List<Game> games;

        public GameDirectory()
        {
            games = new List<Game>();
        }

        public void addGame(Game game)
        {
            games.Add(game);
        }

        public List<Game> GetGames()
        {
            return games;
        }

        public string Directory
        {
            get
            {
                return directory;
            }
            set
            {
                directory = value;
            }
        }

        public string convertGameDirectoryToJSONString()
        {
            //var tags = new { tags = this.games };
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void convertJSONStringToGameDirectory(string jsonstring)
        {
            var obj =(GameDirectory) JsonConvert.DeserializeObject(jsonstring);
            directory = obj.directory;
            games = obj.games;
        }

        public void removeGame(Game g)
        {
            games.Remove(g);
        }
    }
}
