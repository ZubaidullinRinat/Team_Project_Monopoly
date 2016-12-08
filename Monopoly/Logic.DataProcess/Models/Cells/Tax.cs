using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cells
{
    public class Tax : Cell
    {
        public int Amount { get; private set; }

        public Tax(string _name, int _ID, int _amount) : base(_name, _ID)
        {
            Amount = _amount;
        }
    }
}
