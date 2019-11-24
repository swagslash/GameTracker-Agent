using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameTracker_Agent
{
    class BackgroundScanner
    {
        private BackgroundWorker Worker;

        public BackgroundScanner()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += new DoWorkEventHandler
                    (Worker_DoWork);
            Worker.ProgressChanged += new ProgressChangedEventHandler
                    (Worker_ProgressChanged);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (Worker_RunWorkerCompleted);
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;

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
            if (!Worker.IsBusy)
            {
                Worker.RunWorkerAsync();
            }
        }
    }
}
