using GameTracker_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameTracker_Core
{
    public static class DirectorySearchHelper
    {
        public static bool DirectoryExecuteSearch(string dir)
        {
            return DirectoryExecuteSearchRecursive(dir, false);
        }

        private static bool DirectoryExecuteSearchRecursive(string dir, bool erg)
        {
            if (erg) return true;
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    if (Path.GetExtension(f).Equals(".exe"))
                    {
                        return true;
                    }
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    erg = DirectoryExecuteSearchRecursive(d, erg);
                    if (erg) break;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return erg;
        }

        public static List<Game> FindNewGames(GameDirectory gameDirectory)
        {
            List<Game> gameList = new List<Game>();
            foreach (Game g in GetAllGamesFromPath(gameDirectory.Directory))
            {
                if (!gameDirectory.GetGames().Contains(g))
                {
                    gameList.Add(g);
                }
            }
            return gameList;
        }

        public static List<Game> GetAllGamesFromPath(string path)
        {
            if (!Directory.Exists(path)) return new List<Game>();
            List<Game> gameList = new List<Game>();
            foreach(string d in Directory.GetDirectories(path))
            {
                if (DirectoryExecuteSearch(d))
                {
                    gameList.Add(new Game(d));
                }
            }
            return gameList;
        }

        public static List<Game> FindNotExistingGames(GameDirectory gameDirectory)
        {
            if (!Directory.Exists(gameDirectory.Directory)) return new List<Game>();
            List<Game> notExistingGameList = new List<Game>();
            foreach(Game g in gameDirectory.GetGames())
            {
                if (!Directory.Exists(g.DirectoryPath))
                {
                    notExistingGameList.Add(g);
                }
            }
            return notExistingGameList;
        }

    }
}
