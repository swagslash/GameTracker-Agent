using GameTracker_Core.Models;
using GameTracker_Core.Models.Dto;
using GameTracker_Core.Network;
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
            var gameDtos = ConvertGameIListToGameDtoList(games);
            var json = Serializer.SerializeJson<List<GameDto>>(gameDtos);
            var response = WebApiClient.PostToServer(json);
            //send
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("success");
            }
        }

        private List<GameDto> ConvertGameIListToGameDtoList(IList<Game> games)
        {
            var gameDtos = new List<GameDto>();

            foreach (Game g in games)
            {
                gameDtos.Add(new GameDto(g.Name, g.DirectoryPath));
            }
            return gameDtos;
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

        public GameDirectory GetGameDirectory(string path)
        {
            return _device.GetGameDirectory(path);
        }

        public IList<Game> GetGamesFromDirectory(string path)
        {
            return _device.GetGamesFromDirectory(path);
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
