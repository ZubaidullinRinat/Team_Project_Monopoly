using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess.Models.Cells
{
    public class Prison : Cell
    {
        
        public Prison(string _name, int _ID) : base(_name, _ID)
        {
            
        }

        public void ExecutePrisoner(ref User user)
        {
            if (!user.IsInPrison)
            {
                user.IsInPrison = true;
                user.IdleCount = 3;
            } else
            {
                if (user.IdleCount > 0)
                {
                    user.IdleCount--;                 
                }

                if (user.IdleCount == 0)
                    user.IsInPrison = false;
            }
        }

        
        
    }
}
