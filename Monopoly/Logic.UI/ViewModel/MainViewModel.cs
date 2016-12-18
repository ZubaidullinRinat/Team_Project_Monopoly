using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.DataProcess;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UI_TestConsole.Models;

namespace Logic.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        Repository r;
        public string Position_0 { get; set; }
        public string Position_1 { get; set; } 
        public string Position_2 { get; set; }
        public string Position_3 { get; set; }
        public int Test { get; set; }
        public User UserSeeder(string _name)
        {
            var result = new User(_name);
            result.positionChanged += postionHadler;
            //result.isOnPrisonChanged += isInPriosnHandler;
            //result.moneyChanged += MoneyChangedHandler;
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TestCommand = new RelayCommand(()=> 
            {
                if(Test == r.Session.Users.Count)
                {
                    Test = 0;
                }
                r.TestMove(r.Session.Users[Test]);
                Test++;
            });
            //Новый репозиторий
            r = Repository.getInstance();
            //r.BuyRepo += BuyBackFromPrisonHandler;
            //r.BuyRepo += BuyHandler;
            //Этот кусок будет вызывать новую viewModel для определения количества пользователей
            r.Session.Users = new List<User>();
            foreach (var item in StartWindowModel.Users)
            {
                r.Session.Users.Add(UserSeeder(item));
            }
            for (int i = 0; i < r.Session.Users.Count; i++)
            {
                SeedPositions(i);
            }
            Test = 0;
            RightSectionSeeder();
        }
        void postionHadler(User user)
        {
           //GetType().GetProperty("Position_" + Current).SetValue(this, "100,100,0,0", null);
           PositionAnimation(user.PreviousPosition, user.Position);

        }
        public string Position { get; set; }
        public double Player1 { get; set; } = 1;
        public double Player2 { get; set; } = 1;
        public double Player3 { get; set; } = 1;
        public double Player4 { get; set; } = 1;
        public string Player1_Name { get; set; }
        public string Player2_Name { get; set; }
        public string Player3_Name { get; set; }
        public string Player4_Name { get; set; }

        public RelayCommand TestCommand { get; set; }

        void RightSectionSeeder()
        {
            for (int i = 0; i < r.Session.Users.Count; i++)
            {
                GetType().GetProperty("Player" + (i+1)+"_Name")
                                .SetValue(this, r.Session.Users[i].Name, null);
            }
            for (int i = 4; i > r.Session.Users.Count; i--)
            {
                GetType().GetProperty("Player" + (i))
                                .SetValue(this, 0, null);
            }
        }

        void PositionAnimation(int current, int newPosition)
        {
            Movement Direction = InitialDirection(current);    
            int Iterations = Math.Abs(newPosition - current);
            if(current > newPosition)
            {
                Iterations = 40 + newPosition - current;
            }
            for (int i = 0; i < Iterations; i++)
            {
                var str = (string)GetType().GetProperty("Position_" + Test)
                    .GetValue(this, null);
                string[] positions = str.Split(',');
                int x = Int32.Parse(positions[0]);
                
                int y = Int32.Parse(positions[1]);
                if((current + i)%10 == 0)
                {
                    DirectionChanger(out Direction, current + i);
                }
                int pixels = 53;
                switch (Direction)
                {
                    case Movement.Down:
                        if((current+i) == 9)
                        {
                            pixels += 20;
                        }
                        if(current+i == 0)
                        {
                            SeedPositions(Test);
                            str = (string)GetType().GetProperty("Position_" + Test)
                                    .GetValue(this, null);
                            positions = str.Split(',');
                            x = Int32.Parse(positions[0]);

                            y = Int32.Parse(positions[1]);
                        }
                        for (int l = 0; l < pixels; l++)
                        {
                            x--;
                            GetType().GetProperty("Position_" + Test)
                                .SetValue(this, string.Format($"{x.ToString()},{y.ToString()},0,0"), null);
                            //await Task.Delay(TimeSpan.FromMilliseconds(1));
                        }
                        break;
                    case Movement.Left:
                        pixels--;
                        while (y > Int32.Parse(positions[1]) - pixels)
                        {
                            if ((current + i) == 10)
                            {
                                pixels = 81;
                            }
                            y--;
                            GetType().GetProperty("Position_" + Test)
                                .SetValue(this, string.Format($"{x.ToString()},{y.ToString()},0,0"), null);
                            //await Task.Delay(TimeSpan.FromMilliseconds(5));
                        }
                        break;
                    case Movement.Up:
                        pixels --;
                        if (current + i == 20)
                        {
                            pixels = 75;
                        }
                        if (current + i == 29)
                        {
                            pixels = 80;
                        }
                        while (x < Int32.Parse(positions[0]) + pixels)
                        {                            
                            x++;
                            GetType().GetProperty("Position_" + Test)
                                .SetValue(this, string.Format($"{x.ToString()},{y.ToString()},0,0"), null);
                            //await Task.Delay(TimeSpan.FromMilliseconds(1));
                        }
                        break;
                    case Movement.Right:
                        if(current+i == 39)
                        {
                            pixels = 80;
                        }
                        pixels --;
                        while (y < Int32.Parse(positions[1]) + pixels)
                        {
                            y++;
                            GetType().GetProperty("Position_" + Test)
                                 .SetValue(this, string.Format($"{x.ToString()},{y.ToString()},0,0"), null);
                            //await Task.Delay(TimeSpan.FromMilliseconds(1));
                        }
                        if (current + i == 40)
                        {
                            SeedPositions(Test);
                            current = 0;
                        }                    
                        break;
                }
                
            }
            
        }
        void SeedPositions(int id)
        {
            switch (id)
            {
                case 0:
                    Position_0 = "620,610,0,0";
                    break;
                case 1:
                    Position_1 = "629,610,0,0";
                    break;
                case 2:
                    Position_2 = "620,618,0,0";
                    break;
                case 3:
                    Position_3 = "629,618,0,0";
                    break;
                default:
                    break;
            }
        }
        Movement InitialDirection (int position)
        {             
            if(position >= 10 && position < 20)
            {
                return Movement.Left;
            }
            else if (position >= 20 && position < 30)
            {
                return Movement.Up;
            }
            else if (position >= 30 && position <= 39)
            {
                return Movement.Right;
            }
            return Movement.Down;
        }
        void DirectionChanger(out Movement _direction, int position)
        {
            _direction = Movement.Down;
            switch (position)
            {
                case 10:
                    _direction = Movement.Left;
                    break;
                case 20:
                    _direction = Movement.Up;
                    break;
                case 30:
                    _direction = Movement.Right;
                    break;
                case 0:
                    _direction = Movement.Down;
                    break;
            }
        }
        // объявить все лейблы и тектблоки из UI -------  FIX ME//
        // поля OWNER для Юзарей, кто купил эти улицы //
    }
    public enum Movement
    {
        Down, 
        Left,
        Up,
        Right
    }
}