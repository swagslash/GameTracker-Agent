using GameTracker_Core;
using GameTracker_Core.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace GameTrackerTest
{
    [TestFixture]
    public class ControllerTest
    {
        private static readonly string appDataPath = Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.ApplicationData), "GameTrackerAgent", "ControllerTest");

        private Controller controller1;
        private Controller controller2;

        [Test]
        public void ScanComputerAfterOneAddGameRegister()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var path = Path.Combine(appDataPathExtended, "Test3");
            controller1.AddGameDirectory(path);
            controller2.AddGameDirectory(path);
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test3", "Test31"));
            Directory.CreateDirectory(Path.Combine(appDataPathExtended, "Test3", "Test32"));
            using (File.Create(Path.Combine(appDataPathExtended, "Test3", "Test31", "test3.exe")))  { }
            using (File.Create(Path.Combine(appDataPathExtended, "Test3", "Test32", "test3.txt")))  { }

            // when
            controller1.ScanComputer();
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 2);
            CollectionAssert.AreNotEqual(controller1.GetGameDirectories()[1].GetGames(), controller2.GetGameDirectories()[1].GetGames());
            Assert.AreEqual(controller1.GetGameDirectories()[1].GetGames().Count, 1);

        }

        [Test]
        public void ScanComputerWithNoChanges()
        {
            // given
            SetUp(TestContext.CurrentContext.Test.Name);

            // when
            controller1.ScanComputer();
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 1);
            CollectionAssert.AreEqual(controller1.GetGameDirectories()[0].GetGames(), controller2.GetGameDirectories()[0].GetGames());
        }

        [Test]
        public void ScanComputerAfterOneGameWasRemoved()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var path = Path.Combine(appDataPathExtended, "Test1");
            Directory.Delete(path, true);

            // when
            controller1.ScanComputer();

            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 1);
            Assert.AreEqual(controller2.GetGameDirectories().Count, 1);

        }

        [Test]
        public void AddGameDictionarySuccesful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            _ = controller1.GetGameDirectories();
            var path = Path.Combine(appDataPathExtended, "Test2");
            // when
            controller1.AddGameDirectory(path);
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count,2);
            Assert.AreEqual(controller2.GetToken(), controller1.GetToken());
            CollectionAssert.AreNotEqual(controller1.GetGameDirectories(), controller2.GetGameDirectories());
            CollectionAssert.IsSupersetOf(controller1.GetGameDirectories(), controller2.GetGameDirectories());
            Assert.AreEqual(controller1.GetGameDirectories()[1].GetGames().Count, 1);
            Assert.AreEqual(controller1.GetGameDirectories()[1].GetGames()[0].Name, "Test21");

        }

        [Test]
        public void AddNotExistingGameDictionary()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            _ = controller1.GetGameDirectories();
            var path = Path.Combine(appDataPathExtended, "Test4");
            // when
            controller1.AddGameDirectory(path);
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 2);
            Assert.AreEqual(controller2.GetToken(), controller1.GetToken());
            CollectionAssert.AreNotEqual(controller1.GetGameDirectories(), controller2.GetGameDirectories());
            CollectionAssert.IsSupersetOf(controller1.GetGameDirectories(), controller2.GetGameDirectories());
            Assert.AreEqual(controller1.GetGameDirectories()[1].GetGames().Count, 0);
        }

        [Test]
        public void RemoveGameDictionarySuccesful()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var path = Path.Combine(appDataPathExtended, "Test1");
            // when
            controller1.RemoveGameDirectory(new GameDirectory(path));
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 0);
        }

        [Test]
        public void RemoveNotExistingGameDictionary()
        {
            // given
            var appDataPathExtended = SetUp(TestContext.CurrentContext.Test.Name);
            var path = Path.Combine(appDataPathExtended, "Test4");
            // when
            controller1.RemoveGameDirectory(new GameDirectory(path));
            // then
            Assert.AreEqual(controller1.GetGameDirectories().Count, 1);
            CollectionAssert.AreEqual(controller1.GetGameDirectories(), controller2.GetGameDirectories());
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


            controller1 = new Controller(appDataPathExtended,"");
            controller2 = new Controller(appDataPathExtended,"");
            controller1.AddGameDirectory(Path.Combine(appDataPathExtended, "Test1"));
            controller2.AddGameDirectory(Path.Combine(appDataPathExtended, "Test1"));

            return appDataPathExtended;
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            if (Directory.Exists(appDataPath))
            {
                Directory.Delete(appDataPath,true);
            }
        }
    }
}
