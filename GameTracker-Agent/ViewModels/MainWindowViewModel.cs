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
using GameTracker_Core.Models;
using GameTracker_Core;
using System.IO;
using GameTracker_Agent.ViewModels;
using System.Windows.Data;

namespace GameTracker_Agent
{
    class MainWindowViewModel : BaseVM
    {
        private static readonly string GAMETRACKERPATH = Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.ApplicationData), "GameTrackerAgent");

        private ICommand _addDirectoryCommand;
        private ICommand _exitProgramCommand;

        private Controller controller;

        public ObservableCollection<GameDirectoryDto> GameDirectories { get; set; }

        private GameDirectoryDto selectedDirectory;

        private BackgroundScanner scanner;
        private BackgroundTimer timer;

        public GameDirectoryDto SelectedDirectory
        {
            get { return selectedDirectory; }
            set { selectedDirectory = value; RaisePropertyChanged(); }
        }


        public string AddDirectoryContent
        {
            get
            {
                return Properties.Resources.ADD_DIRECTORY;
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


        public MainWindowViewModel()
        {
            AddDirectoryCommand = new RelayCommand(AddDirectory);
            ExitProgramCommand = new RelayCommand(ExitProgram);
            controller = new Controller();
            controller.addGameDirectory(@"D:\Blizzard");
            controller.addGameDirectory(@"D:\Origin");
            controller.addGameDirectory(@"D:\GameTest");
            GameDirectories = fillGameDirectories(controller.GetGameDirectories());
            SelectedDirectory = GameDirectories[0];
            //_gameDirectoryLock = new object();
            //BindingOperations.EnableCollectionSynchronization(GameDirectories, _gameDirectoryLock);
            //MessageBox.Show(GameDirectories[0].Games.Count + " ");
            scanner = new BackgroundScanner(scanComputer);
            timer = new BackgroundTimer(scanner.StartWorker);
            timer.Start();
        }

        public void AddDirectory(object obj)
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
                controller.addGameDirectory(dlg.FileName);
                var gameDirectory = controller.GetGameDirectory(dlg.FileName);
                GameDirectories.Add(new GameDirectoryDto(gameDirectory.Directory, gameDirectory.GetGames()));
                //RaisePropertyChanged();
                //MessageBox.Show(gameDirectory.Directory);
            }
        }

        private ObservableCollection<GameDirectoryDto> fillGameDirectories(IList<GameDirectory> gameDirectories)
        {
            var directories = new ObservableCollection<GameDirectoryDto>();
            foreach (GameDirectory directory in gameDirectories)
            {
                var gameDirectoryDto = new GameDirectoryDto(directory.Directory, directory.GetGames());
                directories.Add(gameDirectoryDto);
            }
            return directories;
        }

        private void scanComputer()
        {
            controller.ScanComputer();
            
            var newGameDirectories = fillGameDirectories(controller.GetGameDirectories());

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                var directory = selectedDirectory.Directory;
                GameDirectories.Clear();
                AddChanges(newGameDirectories);
                //SelectedDirectory = GameDirectories[0];
                SelectedDirectory = GameDirectories.Where(x => x.Directory == directory).FirstOrDefault();
            });
            //MessageBox.Show("test");
        }

        private void AddChanges(ObservableCollection<GameDirectoryDto> changes)
        {
            foreach (GameDirectoryDto gameDirectoryDto in changes)
            {
                GameDirectories.Add(gameDirectoryDto);
            }
        }

        public void ExitProgram(object obj)
        {
            timer.Stop();
            Application.Current.Shutdown();
        }

    }
}
