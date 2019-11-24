using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameTracker_Core.Models
{

    [Serializable]
    public class Game
    {
        private string name { get; set; }
        private string directoryPath { get; set; }
        private string imagePath { get; set; }

        public Game()
        {
            
        }

        public Game(string directoryPath)
        {
            this.directoryPath = directoryPath;
            name = directoryPath.Substring(directoryPath.LastIndexOf("\\") + 1);
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string DirectoryPath
        {
            get
            {
                return directoryPath;
            }
            set
            {
                directoryPath = value;
            }
        }

        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
            }
        }



    }
}
