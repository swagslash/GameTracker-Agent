using System;
using GameTracker_Core;
using GameTracker_Core.Models;

namespace GameTrackerTest
{
    class Program
    {
        static void Main()
        {
            GameDirectory gameDirectory = new GameDirectory();
            gameDirectory.Directory = "Test";

            var game = new Game();
            game.Name = "World of Warcraft";
            game.DirectoryPath = "C:ka";
            gameDirectory.addGame(game);
            var json = Serializer.SerializeJson(gameDirectory);
            Console.WriteLine(json);
            var obj = Serializer.DeserializeJson<GameDirectory>(json);
            Console.WriteLine(obj.ToString());

            var game2 = new Game();
            game2.Name = "LoL";
            game2.DirectoryPath = "D:ka";
            gameDirectory.addGame(game2);

            Device d = new Device("token");
            d.addGameDirectory(gameDirectory);
            System.IO.Directory.CreateDirectory("C:\\Users\\Pete\\AppData\\Roaming\\GameTrackerAgent");
            Serializer.Save("C:\\Users\\Pete\\AppData\\Roaming\\GameTrackerAgent\\device.bin", d);
            Device d2 = Serializer.Load<Device>("C:\\Users\\Pete\\AppData\\Roaming\\GameTrackerAgent\\device.bin");

            //Console.WriteLine(d2.GetGameDirectories()[0].convertGameDirectoryToJSONString());

            Console.WriteLine(DirectorySearchHelper.DirectoryExecuteSearch(@"D:\Blizzard"));
            Console.WriteLine(DirectorySearchHelper.DirectoryExecuteSearch(@"D:\UNI\Logik"));
            
        }
    }
}
