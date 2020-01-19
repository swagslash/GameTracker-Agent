using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameTracker_Agent
{
    internal class BackgroundScanner
    {
        private BackgroundWorker _Worker;

        public BackgroundScanner()
        {
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
            MessageBox.Show("Worker says hi!");
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
