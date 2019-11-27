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
    class DirectoryHelperTest
    {

        private static readonly string appDataPath = Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.ApplicationData), "GameTrackerAgent", "DirectoryHelperTest");
        [Test]
        public void DirectoryExecuteSearchSuccessful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);

            // when
            var res = DirectorySearchHelper.DirectoryExecuteSearch(appDataPathExtended);

            // then
            Assert.IsTrue(res);
        }

        [Test]
        public void DirectoryExecuteSearchUnsuccessful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);

            // when
            var res = DirectorySearchHelper.DirectoryExecuteSearch(Path.Combine(appDataPathExtended, "Test3"));

            // then
            Assert.IsFalse(res);
        }

        [Test]
        public void FindNewGamesSuccessful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var gameDirectory = new GameDirectory(appDataPathExtended);
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test3", "Test31"));
            using FileStream fs1 = File.Create(Path.Combine(appDataPathExtended, "Test3", "Test31", "test3.exe"));

            // when
            var newGames = DirectorySearchHelper.FindNewGames(gameDirectory);

            // then
            CollectionAssert.IsNotEmpty(newGames);
            CollectionAssert.Contains(newGames, new Game(Path.Combine(appDataPathExtended, "Test3")));
            Assert.AreEqual(newGames.Count, 1);
        }
    

        [Test]
        public void FindNewGamesWithoutNewGames()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var gameDirectory = new GameDirectory(appDataPathExtended);

            // when
            var newGames = DirectorySearchHelper.FindNewGames(gameDirectory);

            // then
            CollectionAssert.IsEmpty(newGames);
        }

        [Test]
        public void GetAllGamesFromPath()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);

            // when
            var games = DirectorySearchHelper.GetAllGamesFromPath(appDataPathExtended);

            // then
            CollectionAssert.IsNotEmpty(games); 
            Assert.AreEqual(games.Count, 2);
            CollectionAssert.Contains(games, new Game(Path.Combine(appDataPathExtended, "Test1")));
        }

        [Test]
        public void FindNotExistingGamesSuccessful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);

            // when
            var gameDirectory = new GameDirectory(appDataPathExtended);
            Directory.Delete(Path.Combine(appDataPathExtended, "Test1"), true);
            var games = DirectorySearchHelper.FindNotExistingGames(gameDirectory);

            // then
            Assert.AreEqual(1, games.Count);
            Assert.AreEqual(2, gameDirectory.GetGames().Count);
            Assert.AreEqual(new Game(Path.Combine(appDataPathExtended, "Test1")), games[0]);
        }

        [Test]
        public void FindNotExistingGamesWithNoMissingGame()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);

            // when
            var gameDirectory = new GameDirectory(appDataPathExtended);
            var games = DirectorySearchHelper.FindNotExistingGames(gameDirectory);

            // then
            Assert.AreEqual(0, games.Count);
            Assert.AreEqual(2, gameDirectory.GetGames().Count);
        }

        [OneTimeSetUp]
        public void ClassInit()
        {
            if (Directory.Exists(appDataPath))
            {
                Directory.Delete(appDataPath, true);
            }
            Directory.CreateDirectory(appDataPath);
        }

        private string SetUp(string testName)
        {
            var appDataPathExtended = Path.Combine(appDataPath, testName);
            Directory.CreateDirectory(appDataPathExtended);
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test1"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test2"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test3"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test1", "Test11"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test1", "Test12"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test2", "Test21"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test2", "Test22"));
            using (File.Create(Path.Combine(appDataPathExtended, "Test1", "Test11", "test1.exe"))) { }
            using (File.Create(Path.Combine(appDataPathExtended, "Test1", "Test11", "test1.txt"))) { }
            using (File.Create(Path.Combine(appDataPathExtended, "Test1", "Test12", "test1.exe"))) { }
            using (File.Create(Path.Combine(appDataPathExtended, "Test2", "Test21", "test2.exe"))) { }
            using (File.Create(Path.Combine(appDataPathExtended, "Test2", "Test22", "test2.txt"))) { }

            return appDataPathExtended;
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            if (Directory.Exists(appDataPath))
            {
                Directory.Delete(appDataPath, true);
            }
        }
    }
}
