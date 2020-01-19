using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTracker_Core.Models;

namespace GameTracker_Agent.Models
{
    class GameDirectoryDto : INotifyPropertyChanged
    {
        public string Directory { get; set; }
        public ObservableCollection<GameDto> Games { get; set; }

        public GameDirectoryDto()
        {
            Games = new ObservableCollection<GameDto>();
        }
        public GameDirectoryDto(string path, IList<Game> games) : this()
        {
            Directory = path;
            foreach (Game game in games)
            {
                Games.Add(new GameDto(game.DirectoryPath, game.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddChanges(ObservableCollection<GameDto> games)
        {
            foreach (GameDto game in games)
            {
                Games.Add(game);
                RaisePropertyChanged("Games");
            }

        }
        private void RaisePropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
