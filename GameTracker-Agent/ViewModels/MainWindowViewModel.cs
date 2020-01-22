using GameTracker_Agent.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GameTracker_Core.Models;
using GameTracker_Core;
using System.IO;
using GameTracker_Agent.ViewModels;
using GameTracker_Agent.Views;

namespace GameTracker_Agent
{
    class MainWindowViewModel:BaseVM
    {
        private ICommand _addDirectoryCommand;
        private ICommand _exitProgramCommand;
        private ICommand _optionCommand;
        private ICommand _sendCommand;


        public ObservableCollection<GameDirectoryDto> GameDirectories { get; set; }

        private GameDirectoryDto selectedDirectory;

        private readonly BackgroundScanner scanner;
        //private readonly BackgroundTimer timer;


        public GameDirectoryDto SelectedDirectory
        {
            get { return selectedDirectory; }
            set { selectedDirectory = value; RaisePropertyChanged("SelectedDirectory"); }
        }


        public string AddDirectoryContent
        {
            get
            {
                return Properties.Resources.ADD_DIRECTORY;
            }
        }

        public string SendContent
        {
            get
            {
                return Properties.Resources.SEND;
            }
        }


        public string OptionContent
        {
            get
            {
                return Properties.Resources.OPTION;
            }
        }

        public ICommand AddDirectoryCommand
        {
            get
            {
                return _addDirectoryCommand;
            }
            set
            {
                _addDirectoryCommand = value;
            }
        }
        public ICommand SendCommand
        {
            get
            {
                return _sendCommand;
            }
            set
            {
                _sendCommand = value;
            }
        }


        public ICommand OptionCommand
        {
            get
            {
                return _optionCommand;
            }
            set
            {
                _optionCommand = value;
            }
        }

        public ICommand ExitProgramCommand
        {
            get
            {
                return _exitProgramCommand;
            }
            set
            {
                _exitProgramCommand = value;
            }
        }

        public MainWindowViewModel() : base()
        {
            GameDirectories = new ObservableCollection<GameDirectoryDto>();
            AddDirectoryCommand = new RelayCommand(AddDirectory);
            ExitProgramCommand = new RelayCommand(ExitProgram);
            OptionCommand = new RelayCommand(OpenOptions);
            SendCommand = new RelayCommand(SendGames);
            FillGameDirectories(controller.GetGameDirectories());

            if (GameDirectories.Count > 0)
            {
                SelectedDirectory = GameDirectories.First(); //just pick one
            }
            scanner = new BackgroundScanner(ScanComputer);
            //timer = new BackgroundTimer(scanner.StartWorker);
            //timer.Start();
        }

        private void SendGames(object obj)
        {
            scanner.StartWorker();
        }

        public void AddDirectory(object obj)
        {
            using (var dlg = new CommonOpenFileDialog()
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
            })
            {
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    controller.AddGameDirectory(dlg.FileName);
                    var gameDirectory = controller.GetGameDirectory(dlg.FileName);
                    var gameDirectoryDto = new GameDirectoryDto(gameDirectory.Directory, gameDirectory.GetGames());
                    GameDirectories.Add(gameDirectoryDto);
                    
                    SelectedDirectory = GameDirectories.Last(); //for showing the new inserted one
                    controller.SaveDevice();
                }
            }
        }

        private void FillGameDirectories(IList<GameDirectory> gameDirectories)
        {
            foreach (GameDirectory directory in gameDirectories)
            {
                GameDirectories.Add(new GameDirectoryDto(directory.Directory, directory.GetGames()));
            }
        }

        private void ScanComputer()
        {
            controller.ScanComputer();
            controller.SendGames();
            

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                var directory = selectedDirectory.Directory;
                GameDirectories.Clear();
                FillGameDirectories(controller.GetGameDirectories());
                SelectedDirectory = GameDirectories.Last();
            });
            controller.SaveDevice();
        }

        public void ExitProgram(object obj)
        {
            //timer.Stop(); 
            Environment.Exit(Environment.ExitCode);
            controller.SaveDevice();
            Application.Current.Shutdown();
        }

        public void OpenOptions(object obj)
        {
            OptionWindow optionWindow = new OptionWindow();
            optionWindow.ShowDialog();
            Console.WriteLine("openOptions");
        }

    }
}
