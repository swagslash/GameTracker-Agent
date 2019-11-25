using GameTracker_Agent.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameTracker_Agent
{
    class MainWindowViewModel
    {

        private ICommand _addPathButtonCommand { get; set; }
        private ICommand _openDirectoryCommand { get; set; }
        private bool canExecute = true;

        public ObservableCollection<String> PathCollection { get; set; }
        public DirectoryPath DirectoryPath { get; set; }

        public string AddPathButtonContent
        {
            get
            {
                return Properties.Resources.ADD_PATH;
            }
        }


        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }

        public ICommand AddPathButtonCommand
        {
            get
            {
                return _addPathButtonCommand;
            }
            set
            {
                _addPathButtonCommand = value;
            }
        }

        public ICommand OpenDirectoryCommand
        {
            get
            {
                return _openDirectoryCommand;
            }
            set
            {
                _openDirectoryCommand = value;
            }
        }

        public MainWindowViewModel()
        {
            AddPathButtonCommand = new RelayCommand(AddPathToListbox);
            OpenDirectoryCommand = new RelayCommand(OpenDirectory);
            this.PathCollection = new ObservableCollection<String>();
            this.DirectoryPath = new DirectoryPath();
        }

        public void AddPathToListbox(object obj)
        {
            PathCollection.Add(DirectoryPath.Directory);
            BackgroundScanner backgroundScanner = new BackgroundScanner();
            BackgroundTimer backgroundTimer = new BackgroundTimer(backgroundScanner.StartWorker);
            //backgroundTimer.Start();
        }

        public void OpenDirectory(object obj)
        {
            var dlg = new CommonOpenFileDialog()
            {
                Title = "Choose Directory",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DirectoryPath.Directory = dlg.FileName;
            }
        }

    }
}
