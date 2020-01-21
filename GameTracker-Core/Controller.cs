using GameTracker_Core.Models;
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
        public Device Device { get; set; }

        public string Url { get; set; }

        public Controller(string url)
        {
            this.Url = url;
            Device = new Device();
            Directory.CreateDirectory(appDataPath);
            if (File.Exists(Path.Combine(appDataPath, fileName)))
            {
                Device = Serializer.Load<Device>(Path.Combine(appDataPath, fileName));
            }
        }

        public Controller(string filePath,string url)
        {
            this.Url = url;
            Device = new Device();
            if (File.Exists(filePath))
            {
                Device = Serializer.Load<Device>(filePath);
            }
        }

        public void ScanComputer()
        {
            //Find new or removed Games
            var gameDirectories = Device.GetGameDirectories();
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
        }

        public void SendGames()
        {
            if(!string.IsNullOrEmpty(Device.Token))
            {
                IList<Game> games = Device.GetAllGames();
                //var gameDtos = ConvertGameIListToGameDtoList(games);
                var json = Serializer.SerializeJson<IList<Game>>(games);
                Console.WriteLine(json);
                try
                {
                    var response = WebApiClient.PostGamesToServer(json, Device.Token, Url);
                    //send
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("success");
                    }
                    else
                    {
                        Console.WriteLine("Failure, could not send: " + json);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("SendGames Error: {0}", e);
                }
            }
        }

        public void AddGameDirectory(string path)
        {
            Device.AddGameDirectory(new GameDirectory(path));
        }

        public void RemoveGameDirectory(GameDirectory gameDirectory)
        {
            Device.RemoveGameDirectory(gameDirectory);
        }

        public IList<GameDirectory> GetGameDirectories()
        {
            return Device.GetGameDirectories();
        }

        public GameDirectory GetGameDirectory(string path)
        {
            return Device.GetGameDirectory(path);
        }

        public IList<Game> GetGamesFromDirectory(string path)
        {
            return Device.GetGamesFromDirectory(path);
        }

        public string GetToken()
        {
            return Device.Token;
        }

        public void SetToken(string token)
        {
            Device.Token = token;
        }

        public void SaveDevice()
        {
            Serializer.Save(Path.Combine(appDataPath, fileName), Device);
        }
    }
}
