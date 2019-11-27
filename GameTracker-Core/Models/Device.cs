using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTracker_Core.Models
{
    [Serializable]
    public class Device
    {
        private string _token;
        private List<GameDirectory> _gameDirectories;

        public Device()
        {
            _gameDirectories = new List<GameDirectory>();
        }

        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

        public void addGameDirectory(GameDirectory gameDirectory)
        {
            _gameDirectories.Add(gameDirectory);
        }

        public List<GameDirectory> GetGameDirectories()
        {
            return _gameDirectories;
        }
        public bool removeGameDirectory(GameDirectory gameDirectory)
        {
            return _gameDirectories.Remove(gameDirectory);
        }

        public IList<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();
            foreach(GameDirectory gameDirectory in _gameDirectories)
            {
                games.AddRange(gameDirectory.GetGames());
            }
            return games.AsReadOnly();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!this.GetType().Equals(obj.GetType())) return false;
            Device d = obj as Device;
            return d.Token == this.Token && d.GetGameDirectories().SequenceEqual(this.GetGameDirectories());
        }

        public override int GetHashCode()
        {
            var hashCode = 2019729052;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_token);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<GameDirectory>>.Default.GetHashCode(_gameDirectories);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Token);
            return hashCode;
        }
    }
}
