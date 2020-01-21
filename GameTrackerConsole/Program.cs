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
            c.ScanComputer();
            c.SetToken("b9eebd97-6244-488a-88a8-1592de03cad7");
            //Console.WriteLine(Serializer.SerializeJson<Device>(c._device));
            c.SendGames();
        }
    }
}
