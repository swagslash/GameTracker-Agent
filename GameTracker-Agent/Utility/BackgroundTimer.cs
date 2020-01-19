using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GameTracker_Agent
{
    internal class BackgroundTimer
    {
        private readonly DispatcherTimer _Timer;
        private static readonly int _dispatchTime = 3600000; //1h

        public BackgroundTimer()
        {
            _Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(_dispatchTime)
            };
        }
        public BackgroundTimer(EventHandler e)
        {
            _Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(_dispatchTime)
            };

            _Timer.Tick += e;
        }

        public void Start()
        {
            if (!_Timer.IsEnabled)
            {
                _Timer.Start();
            }
        }

        public void Stop()
        {
            if (_Timer.IsEnabled)
            {
                _Timer.Stop();
            }
        }

        public void SetTimeInterval(int time)
        {
            _Timer.Interval = TimeSpan.FromMilliseconds(time);
        }

        public int GetTimeInterval()
        {
            return (int) _Timer.Interval.TotalMilliseconds;
        }

        public void AddEvent(EventHandler e)
        {
            _Timer.Tick += e;
        }

        public void RemoveEvent(EventHandler e)
        {
            _Timer.Tick -= e;
        }
    }
}
