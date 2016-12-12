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
        public event Action<User> isOnPrisonChanged;
        public event Action<User> moneyChanged;

        public string Name { get; private set; }

        private int money;

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                moneyChanged?.Invoke(this);
                
            }
        }

        private bool isInPrison;

        public bool IsInPrison
        {
            get { return isInPrison; }
            set
            {
                isInPrison = value;
                isOnPrisonChanged?.Invoke(this);
            }
        }

        private int _idleCount;

        public int IdleCount
        {
            get { return _idleCount; }
            set { _idleCount = value; }
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
            Money = 1500;
            Name = _name;
            position = 0;
            IsInPrison = false;
        }
    }
}
