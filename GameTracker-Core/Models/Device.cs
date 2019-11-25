using System;
using System.Collections.Generic;
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

        public Device(string token) : this()
        {
            this._token = token;
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

        public IList<GameDirectory> GetGameDirectories()
        {
            return _gameDirectories.AsReadOnly();
        }
        public bool removeGameDirectory(GameDirectory gameDirectory)
        {
            return _gameDirectories.Remove(gameDirectory);
        }
    }
}
