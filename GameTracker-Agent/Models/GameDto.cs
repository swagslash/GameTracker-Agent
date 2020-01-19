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

        public override bool Equals(object obj)
        {
            return obj is GameDto dto &&
                   Name == dto.Name &&
                   DirectoryPath == dto.DirectoryPath;
        }

        public override int GetHashCode()
        {
            var hashCode = -199040267;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DirectoryPath);
            return hashCode;
        }
    }
}
