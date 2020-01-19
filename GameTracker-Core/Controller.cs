using GameTracker_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameTracker_Core
{
    public class Controller
    {
        private static readonly string appDataPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "GameTrackerAgent");
        private static readonly string fileName = "device.bin";
        private Device _device;

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
            foreach(GameDirectory gameDirectory in gameDirectories)
            {
                var newGames = DirectorySearchHelper.FindNewGames(gameDirectory);
                if(newGames.Count > 0)
                {
                    gameDirectory.AddGames(newGames);
                }

                var removedGames = DirectorySearchHelper.FindNotExistingGames(gameDirectory);
                if (removedGames.Count > 0)
                {
                    gameDirectory.RemoveGames(removedGames);
                }
            }

            //Send them to Backend
            SendGames();
        }

        private void SendGames()
        {
            IList<Game> games = _device.GetAllGames();
            var json = Serializer.SerializeJson<IList<Game>>(games);
            //send
        }

        public void addGameDirectory(string path)
        {
            _device.addGameDirectory(new GameDirectory(path));
        }

        public void removeGameDirectory(GameDirectory gameDirectory)
        {
            _device.removeGameDirectory(gameDirectory);
        }

        public IList<GameDirectory> GetGameDirectories()
        {
            return _device.GetGameDirectories();
        }

        public string GetToken()
        {
            return _device.Token;
        }

        public void SetToken(string token)
        {
            _device.Token = token;
        }
    }
}
