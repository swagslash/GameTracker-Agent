using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker_Agent.Models
{
    class GameDto
    {

        public string Name { get; set; }
        public string DirectoryPath {get; set; }

        public GameDto(string directoryPath)
        {
            this.DirectoryPath = directoryPath;
            Name = Path.GetFileName(directoryPath);
        }

        public GameDto(string directoryPath,string name)
        {
            this.DirectoryPath = directoryPath;
            Name = name;
        }

    }
}
