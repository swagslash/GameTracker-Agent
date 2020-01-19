using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTracker_Core.Models;

namespace GameTracker_Agent.Models
{
    class GameDirectoryDto
    {
        public string Directory { get; set; }
        public List<GameDto> Games { get; set; }

        public GameDirectoryDto()
        {
            Games = new List<GameDto>();
        }
        public GameDirectoryDto(string path, IList<Game> games) : this()
        {
            Directory = path;
            foreach (Game game in games)
            {
                Games.Add(new GameDto(game.DirectoryPath, game.Name));
            }
        }

    }
}
