using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker_Core.Models.Dto
{
    class GameDto
    {
        [JsonProperty("Name")]
        private string _name;

        [JsonProperty("Directory")]
        private string _directoryPath;

        public GameDto()
        {

        }

        public GameDto(string name, string directoryPath)
        {
            _name = name;
            _directoryPath = directoryPath;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
            set
            {
                _directoryPath = value;
            }
        }
    }
}
