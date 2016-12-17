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
        public int Mortage { get; private set; }
        public User Owner { get; set; }

        public int Cost { get; private set; }
        public Railway(string _name, int _ID, int Cost, int Mortage) : base(_name, _ID)
        {
            this.Mortage = Mortage;
            this.Cost = Cost;
        }
    }
}
