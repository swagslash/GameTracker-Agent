using GameTracker_Agent.ViewModels;
using GameTracker_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker_Agent.ViewModels
{
    class OptionWindowViewModel:BaseVM
    {

        public string Token
        {
            get { return Controller.GetToken(); }
            set { Controller.SetToken(value); }
        }

        public OptionWindowViewModel()
        {
            Controller.SetToken("b9eebd97-6244-488a-88a8-1592de03cad7");
        }
    }
}
