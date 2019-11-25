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

        [JsonProperty("Name")]
        private string _name;

        [JsonProperty("Directory")]
        private string _directoryPath;

        [JsonProperty("ImageDirectory")]
        private string _imagePath;


        public Game()
        {
            
        }

        public Game(string directoryPath)
        {
            this._directoryPath = directoryPath;
            _name = Path.GetDirectoryName(directoryPath);
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

        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!this.GetType().Equals(obj.GetType())) return false;
            Game g = obj as Game;
            return g.Name.Equals(this.Name) && g.DirectoryPath.Equals(this.DirectoryPath);
        }


    }
}
