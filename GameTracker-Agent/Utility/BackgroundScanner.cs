using System;
using System.ComponentModel;
using System.Windows;

namespace GameTracker_Agent
{
    internal class BackgroundScanner
    {
        private BackgroundWorker _Worker;
        private Action method;

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
            _Worker.ProgressChanged += new ProgressChangedEventHandler
                    (Worker_ProgressChanged);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (Worker_RunWorkerCompleted);

        }

        public void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        public void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(method != null)
            {
                method.Invoke();
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        public void StartWorker(object sender, EventArgs e)
        {
            if (!_Worker.IsBusy)
            {
                _Worker.RunWorkerAsync();
            }
        }
    }
}
