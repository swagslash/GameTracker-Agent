using GameTracker_Core;
using GameTracker_Core.Models;
using System;
using System.Linq;

namespace GameTrackerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller();
            c.addGameDirectory(@"D:\Blizzard");
            c.ScanComputer();
        }
    }
}
