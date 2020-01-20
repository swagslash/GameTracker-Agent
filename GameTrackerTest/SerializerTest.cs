using GameTracker_Core;
using GameTracker_Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameTrackerTest
{
    [TestFixture]
    class SerializerTest
    {
        private static readonly string appDataPath = Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.ApplicationData), "GameTrackerAgent", "SerializerTest");

        #region Save and Load
        [Test]
        public void saveAndLoadFromDevice()
        {
            // given
            var device = new Device();
            var path = Path.Combine(appDataPath, "device.bin");

            // when
            Serializer.Save(path, device);
            var device2 = Serializer.Load<Device>(path);

            // then
            Assert.AreEqual(device, device2);
        }

        [Test]
        public void saveAndLoadFromGame()
        {
            // given
            var game = new Game();
            var path = Path.Combine(appDataPath, "game.bin");

            // when
            Serializer.Save(path, game);
            var game2 = Serializer.Load<Game>(path);
            Console.WriteLine(game2.DirectoryPath);

            // then
            Assert.AreEqual(game, game2);
        }

        [Test]
        public void saveAndLoadFromGameDirectory()
        {
            // given
            var gameDirectory = new GameDirectory();
            var path = Path.Combine(appDataPath, "gameDirectory.bin");

            // when
            Serializer.Save(path, gameDirectory);
            var gameDirectory2 = Serializer.Load<GameDirectory>(path);

            // then
            Assert.AreEqual(gameDirectory, gameDirectory2);
        }

        [Test]
        public void saveOnNotExistingPath()
        {
            // given 
            var game = new Game();
            var path = Path.Combine(appDataPath, "NotExistingPath");
            // when
            var erg = Serializer.Save(path, game);

            // then
            Assert.IsTrue(erg);
        }

        [Test]
        public void loadFromNotExistingPath()
        {
            // given
            var path = Path.Combine(appDataPath, "NotExistingPath");
            // when
            var erg = Serializer.Load<Game>(path);

            // then
            Assert.AreEqual(erg, new Game());
        }

        #endregion
        #region Json
        [Test]
        public void SerializeAndDeserializeJsonFromGameDirectory()
        {
            // given 
            var gameDirectory = new GameDirectory();

            // when
            var json = Serializer.SerializeJson(gameDirectory);
            var gameDirectory2 = Serializer.DeserializeJson<GameDirectory>(json);

            // then
            Assert.AreEqual(gameDirectory, gameDirectory2);
        }

        [Test]
        public void SerializeAndDeserializeJsonFromGame()
        {
            // given 
            var game = new Game();

            // when
            var json = Serializer.SerializeJson(game);
            var game2 = Serializer.DeserializeJson<Game>(json);

            // then
            Assert.AreEqual(game, game2);
        }

        [Test]
        public void SerializeAndDeserializeJsonFromDevice()
        {
            // given 
            var device = new Device();

            // when
            var json = Serializer.SerializeJson(device);
            var device2 = Serializer.DeserializeJson<Device>(json);

            // then
            Assert.AreEqual(device, device2);
        }

        #endregion
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(appDataPath);
        }
    }
}
