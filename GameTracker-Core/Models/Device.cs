using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker_Core.Models
{
    [Serializable]
    public class Device
    {
        private string token { get; set; }
        private List<GameDirectory> gameDirectories;

        public Device()
        {
            gameDirectories = new List<GameDirectory>();
        }

        public Device(string token) : this()
        {
            this.token = token;
        }

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }

        public void addGameDirectory(GameDirectory gameDirectory)
        {
            gameDirectories.Add(gameDirectory);
        }

        public List<GameDirectory> GetGameDirectories()
        {
            return gameDirectories;
        }
        public bool removeGameDirectory(GameDirectory gameDirectory)
        {
            return gameDirectories.Remove(gameDirectory);
        }
    }
}
