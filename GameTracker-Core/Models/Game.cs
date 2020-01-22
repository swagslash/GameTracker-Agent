using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameTracker_Core.Models
{

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Game
    {
        [JsonProperty("gameName")]
        private string _name;

        [JsonProperty("gamePath")]
        private string _directoryPath;
        

        public Game()
        {
            
        }

        public Game(string directoryPath)
        {
            this._directoryPath = directoryPath;
            _name = Path.GetFileName(directoryPath);
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
            set
            {
                _directoryPath = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!this.GetType().Equals(obj.GetType())) return false;
            Game g = obj as Game;
            return g.Name == this.Name && g.DirectoryPath == this.DirectoryPath;
        }

        public override int GetHashCode()
        {
            var hashCode = 1652676069;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_directoryPath);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DirectoryPath);
            return hashCode;
        }
    }
}
