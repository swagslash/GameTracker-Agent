using System;
using System.ComponentModel;
using System.Windows;

namespace GameTracker_Agent
{
    internal class BackgroundScanner
    {
        private readonly BackgroundWorker _Worker;
        private readonly Action method;

        public BackgroundScanner(Action method)
        {
            this.method = method;
            _Worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
             
            };

            _Worker.DoWork += new DoWorkEventHandler
                    (Worker_DoWork);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (Worker_RunWorkerCompleted);

        }

        public void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = e.Error;
                // weiteres ggf. über InnerException
                Console.WriteLine("Completed with Error: '{0}'", error.Message);
            }
            else if (e.Result != null)
            {
                Console.WriteLine("Completed with Result: {0}", e.Result);
            }
        }
        
        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(method != null)
            {
                method.Invoke();
            }
            else
            {
                MessageBox.Show("There is no Work to do");
            }
        }

        public void StartWorker(object sender, EventArgs e)
        {
            if (!_Worker.IsBusy)
            {
                _Worker.RunWorkerAsync();
            }
        }

        public void StartWorker()
        {
            if (!_Worker.IsBusy)
            {
                _Worker.RunWorkerAsync();
            }
        }
    }
}
