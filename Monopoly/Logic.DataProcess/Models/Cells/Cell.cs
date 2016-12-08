using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cells
{
    public class Cell
    {
        public string Name { get; private set; }
        public int ID { get; private set; }

        public Cell(string _name, int _ID)
        {
            Name = _name;
            ID = _ID;
        }
    }
}
