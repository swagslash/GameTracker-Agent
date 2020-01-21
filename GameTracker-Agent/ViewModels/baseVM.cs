using GameTracker_Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameTracker_Agent.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        protected static Controller controller = new Controller();

        public BaseVM()
        {
        }
        protected void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void saveDevice()
        {
            controller.SaveDevice();
        }
    }
}
