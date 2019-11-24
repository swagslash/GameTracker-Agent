using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GameTracker_Agent
{
    class BackgroundTimer
    {
        private readonly DispatcherTimer Timer;

        public BackgroundTimer()
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
        }
        public BackgroundTimer(EventHandler e)
        {
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            Timer.Tick += e;
        }

        public void Start()
        {
            if (!Timer.IsEnabled)
            {
                Timer.Start();
            }
        }

        public void Stop()
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
        }

        public void SetTimeInterval(int time)
        {
            Timer.Interval = TimeSpan.FromMilliseconds(time);
        }

        public int GetTimeInterval()
        {
            return (int) Timer.Interval.TotalMilliseconds;
        }

        public void AddEvent(EventHandler e)
        {
            Timer.Tick += e;
        }

        public void RemoveEvent(EventHandler e)
        {
            Timer.Tick -= e;
        }
    }
}
