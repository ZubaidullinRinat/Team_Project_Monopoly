using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess.Models.Cells
{
    public class Railway : Cell
    {
        public bool IsMortaged { get; private set; }

        public User Owner { get; private set; }

        public Railway(string _name, int _ID) : base(_name, _ID)
        {
        }
    }
}
