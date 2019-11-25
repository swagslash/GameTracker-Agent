﻿using GameTracker_Core.Models;
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
        private Device device { get; set; }

        public Controller()
        {
            device = new Device();
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            device = Serializer.Load<Device>(Path.Combine(appDataPath,fileName));
        }

        public void scanComputer()
        {
            //Find new or removed Games
            List<GameDirectory> gameDirectories = device.GetGameDirectories();
            List<Game> removedGames = new List<Game>();
            List<Game> newGames = new List<Game>();
            foreach(GameDirectory gameDirectory in gameDirectories)
            {
                removedGames.AddRange(DirectorySearchHelper.FindNotExistingGames(gameDirectory));
                newGames.AddRange(DirectorySearchHelper.FindNewGames(gameDirectory));
            }

            //Send them to Backend
            if (sendNewGames(newGames))
            {
                //If success add the new games and save it
                foreach(Game g in newGames)
                {
                    string Directorypath = g.DirectoryPath.Remove(g.DirectoryPath.LastIndexOf("\\"));
                    foreach (GameDirectory gd in device.GetGameDirectories())
                    {
                        if (gd.Directory.Equals(Directorypath))
                        {
                            gd.addGame(g);
                        }
                    }
                }
            }
            if (sendRemovedGames(removedGames))
            {
                //If success remove the removed games and save it
                foreach (Game g in removedGames)
                {
                    string Directorypath = g.DirectoryPath.Remove(g.DirectoryPath.LastIndexOf("\\"));
                    foreach (GameDirectory gd in device.GetGameDirectories())
                    {
                        if (gd.Directory.Equals(Directorypath))
                        {
                            gd.removeGame(g);
                        }
                    }
                }
            }



        }

        public bool sendNewGames(List<Game> games)
        {
            return false;
        }

        public bool sendRemovedGames(List<Game> games)
        {
            return false;
        }
    }
}
