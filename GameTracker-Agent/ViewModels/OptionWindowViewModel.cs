using GameTracker_Core;
using System.Windows.Input;

namespace GameTracker_Agent.ViewModels
{
    class OptionWindowViewModel:BaseVM
    {
        private ICommand _saveCommand;

        public string Token
        {
            get { return controller.GetToken(); }
            set { controller.SetToken(value); RaisePropertyChanged("Token"); }
        }

        public OptionWindowViewModel()
        {
            SaveCommand = new RelayCommand(SaveOptions);
            //controller.SetToken("b9eebd97-6244-488a-88a8-1592de03cad7"); //local UserToken for Testing purpose
        }
        public string SaveContent
        {
            get
            {
                return Properties.Resources.SAVE;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        public void SaveOptions(object window)
        {
            controller.SaveDevice();
        }

    }
}
