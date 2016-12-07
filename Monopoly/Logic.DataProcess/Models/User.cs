using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestConsole.Models
{
    public class User
    {
        public string Name { get; set; }

        public User(string _name)
        {
            Name = _name;
        }
    }
}
