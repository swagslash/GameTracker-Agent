using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public GameDirectory(string path): this()
        {
            _directory = path;
            _games = DirectorySearchHelper.FindNewGames(this);
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

        public void RemoveGames(List<Game> games)
        {
            foreach(Game g in games)
            {
                _games.Remove(g);
            }
        }

        public void AddGames(List<Game> games)
        {
            foreach(Game g in games)
            {
                _games.Add(g);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!this.GetType().Equals(obj.GetType())) return false;
            GameDirectory d = obj as GameDirectory;
            return d.Directory == this.Directory && d.GetGames().SequenceEqual(this.GetGames());
        }

        public override int GetHashCode()
        {
            var hashCode = 2132176778;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_directory);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Game>>.Default.GetHashCode(_games);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Directory);
            return hashCode;
        }
    }
}
