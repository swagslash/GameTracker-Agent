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
using GameTracker_Agent.Views;

namespace GameTracker_Agent
{
    class MainWindowViewModel : BaseVM
    {
        private static readonly string GAMETRACKERPATH = Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.ApplicationData), "GameTrackerAgent");

        private ICommand _addDirectoryCommand;
        private ICommand _exitProgramCommand;
        private ICommand _optionCommand;

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
            AddDirectoryCommand = new RelayCommand(AddDirectory);
            ExitProgramCommand = new RelayCommand(ExitProgram);
            OptionCommand = new RelayCommand(OpenOptions);
            //controller.addGameDirectory(@"D:\Blizzard");
            //controller.addGameDirectory(@"D:\Origin");
            Controller.addGameDirectory(@"");
            GameDirectories = fillGameDirectories(Controller.GetGameDirectories());
            if (GameDirectories.Count > 0)
            {
                SelectedDirectory = GameDirectories.First();
            }
            //SelectedDirectory = GameDirectories[0];
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
                Controller.addGameDirectory(dlg.FileName);
                var gameDirectory = Controller.GetGameDirectory(dlg.FileName);
                var gameDirectoryDto = new GameDirectoryDto(gameDirectory.Directory, gameDirectory.GetGames());
                GameDirectories.Add(gameDirectoryDto);
                selectedDirectory = GameDirectories.Last();
                Console.WriteLine(Serializer.SerializeJson<Device>(Controller._device));
                Controller.SaveDevice();
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
            Controller.ScanComputer();
            Controller.SendGames();
            
            var newGameDirectories = fillGameDirectories(Controller.GetGameDirectories());

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                var directory = selectedDirectory.Directory;
                GameDirectories.Clear();
                AddChanges(newGameDirectories);
                //SelectedDirectory = GameDirectories[0];
                SelectedDirectory = GameDirectories.Where(x => x.Directory == directory).FirstOrDefault();
            });
            Controller.SaveDevice();
            //MessageBox.Show("test");
        }

        private void AddChanges(ObservableCollection<GameDirectoryDto> changes)
        {
            foreach (GameDirectoryDto gameDirectoryDto in changes)
            {
                //ObservableCollection has no AddRange
                GameDirectories.Add(gameDirectoryDto);
            }
        }

        public void ExitProgram(object obj)
        {
            timer.Stop(); 
            Environment.Exit(Environment.ExitCode);
            Controller.SaveDevice();
            Application.Current.Shutdown();
        }

        public void OpenOptions(object obj)
        {
            //toDo
            OptionWindow optionWindow = new OptionWindow();
            optionWindow.Show();
            Console.WriteLine("openOptions");
        }

    }
}
