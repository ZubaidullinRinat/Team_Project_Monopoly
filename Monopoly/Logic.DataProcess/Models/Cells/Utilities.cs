using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess.Models.Cells
{
    public class Utilities : Cell
    {
        public int Cost { get; private set; }
        public User Owner { get; set; }
        public Utilities(string _name, int _ID, int Cost) : base(_name, _ID)
        {
            this.Cost = Cost;
        }
    }
}
