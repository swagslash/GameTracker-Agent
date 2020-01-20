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
            get { return controller.GetToken(); }
            set { controller.SetToken(value); }
        }

        public OptionWindowViewModel()
        {
            controller.SetToken("b9eebd97-6244-488a-88a8-1592de03cad7");
        }
    }
}
