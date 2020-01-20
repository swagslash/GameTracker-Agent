using GameTracker_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker_Agent.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        private Controller controller;
        public Controller Controller
        {
            get { return controller; }
            set { controller = value; RaisePropertyChanged("Controller"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseVM()
        {
            controller = new Controller();
        }
        protected void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
