using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameTracker_Agent.Models
{
    class DirectoryPath : INotifyPropertyChanged
    {
        private String _directory;

        public event PropertyChangedEventHandler PropertyChanged;

        public String Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                this._directory = value;
                NotifyPropertyChanged("DirectoryPath");
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DirectoryPath()
        {
            this._directory = "";
        }
    }
}
