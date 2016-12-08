using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestConsole.Models
{
    public class User
    {
        public event Action<User> positionChanged;
        public event Action<bool> isOnPrisonChanged;

        public string Name { get; private set; }

        private bool isInPrison;

        public bool IsInPrison
        {
            get { return isInPrison; }
            set
            {
                isInPrison = value;
                isOnPrisonChanged?.Invoke(value);
            }
        }


        private int position;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                positionChanged?.Invoke(this);
            }
        }

        public User(string _name)
        {
            Name = _name;
            position = 0;
            IsInPrison = false;
        }
    }
}
