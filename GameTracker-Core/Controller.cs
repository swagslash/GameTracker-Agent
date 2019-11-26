using GameTracker_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameTracker_Core
{
    public class Controller
    {
        private static readonly string appDataPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "GameTrackerAgent");
        private static readonly string fileName = "device.bin";
        private Device _device { get; set; }

        public Controller()
        {
            _device = new Device();  
            Directory.CreateDirectory(appDataPath);
            if (Directory.Exists(Path.Combine(appDataPath, fileName)))
            {
                _device = Serializer.Load<Device>(Path.Combine(appDataPath, fileName));
            }
        }

        public void ScanComputer()
        {
            //Find new or removed Games
            var gameDirectories = _device.GetGameDirectories();
            List<Game> removedGames = new List<Game>();
            List<Game> newGames = new List<Game>();
            foreach(GameDirectory gameDirectory in gameDirectories)
            {
                removedGames.AddRange(DirectorySearchHelper.FindNotExistingGames(gameDirectory));
                newGames.AddRange(DirectorySearchHelper.FindNewGames(gameDirectory));
            }

            //Send them to Backend
            if (SendNewGames(newGames))
            {
                //If success add the new games and save it
                foreach(Game g in newGames)
                {
                    var Directorypath = g.DirectoryPath.Remove(g.DirectoryPath.LastIndexOf("\\"));
                    foreach (GameDirectory gd in _device.GetGameDirectories())
                    {
                        if (gd.Directory==Directorypath)
                        {
                            gd.addGame(g);
                        }
                    }
                }
            }
            if (SendRemovedGames(removedGames))
            {
                //If success remove the removed games and save it
                foreach (Game g in removedGames)
                {
                    var Directorypath = g.DirectoryPath.Remove(g.DirectoryPath.LastIndexOf("\\"));
                    foreach (GameDirectory gd in _device.GetGameDirectories())
                    {
                        if (gd.Directory==Directorypath)
                        {
                            gd.RemoveGame(g);
                        }
                    }
                }
            }



        }

        public bool SendNewGames(List<Game> games)
        {
            return false;
        }

        public bool SendRemovedGames(List<Game> games)
        {
            return false;
        }
    }
}
