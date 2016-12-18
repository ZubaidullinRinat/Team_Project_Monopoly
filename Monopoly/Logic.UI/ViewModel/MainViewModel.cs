using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.DataProcess;
using Logic.DataProcess.Models.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UI_TestConsole.Models;
using System.Runtime.CompilerServices;

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
    public class MainViewModel : ViewModelBase, System.ComponentModel.INotifyPropertyChanged
    {
        [DllImport("User32")]
        public static extern int MessageBox(int Hwnd, string text, string caption, int type);

        Repository r;
        public List<Property> CurrentUser { get; set; }
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
            result.moneyChanged += MoneyChangedHandler;
            return result;
        }


        void MakeOpacity()
        {
            Player1 = 1;
            Player2 = 1;
            if (Player3 != 0)
                Player3 = 1;
            if (Player4 != 0)
                Player4 = 1;
            if (Test != 0)
                Player1 = 0.3;
            if (Test != 1)
                Player2 = 0.3;
            if (Test != 2 && Player3 != 0)
                Player3 = 0.3;
            if (Test != 3 && Player4 != 0)
                Player4 = 0.3;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TestCommand = new RelayCommand(() =>
            {
                if (Test == r.Session.Users.Count)
                {
                    Test = 0;
                }
                MakeOpacity();
                CurrentUser = null;
                r.NewMove(r.Session.Users[Test]);
                CurrentUser = ListUsers[Test];
                Test++;
                
            });

            //Новый репозиторий
            ListUsers = new ObservableCollection<List<Property>>();
            r = Repository.getInstance();
            SeedSubscriptions();
            //r.BuyRepo += BuyHandler;
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
                ListUsers.Add(new List<Property>());
            }

            RightSectionSeeder();
        } 
        void SeedSubscriptions()
        {
            r.BuyRepo += BuyHandler;
            r.BuybackFromPrisonRepo += BuyBackFromPrison;
            r.GetUsersPropertiesRepo += GetUserProperties;
            r.JailReleaseRepo += JailRelease;
            r.SetPrisonRepo += SetPrison;
            r.DiceRepo += Dice;
            r.CurrentCellRepo += CurrentCell;
            r.GetCardPickRepo += GetCardpick;
            r.NoEnoughMoneyRepo += NoEnoughtMoney;
            r.EndGame += (user) =>
            {
                MessageBox(0, $"Победил - {user.Name}", "Конец игры", 1);
            };

        }
        bool BuyHandler(User user)
        {
            var test = MessageBox(0, "Будуте покупать?", user.Name, 4);
            if (test.ToString() == "6")
            {
                   return true;            
            }
            return false;
        }
        bool BuyBackFromPrison(User user)
        {
            var test = MessageBox(0, "Хотите выкупиться за 50? Если вы откажетесь, будут кинты кубики, и, в сучае дубля, вас освободят", user.Name, 4);
            if (test.ToString() == "6")
            {
                return true;
            }
            return false;
        }
        void GetUserProperties(User user)//логика по отображению
        {
            ListUsers[Test] = user.Properties;
        }
        void JailRelease(User user)
        {
            MessageBox(0, "Вас освободили", user.Name, 4);
        }
        void SetPrison(User user)
        {
            MessageBox(0, "Вы попали в тюрьму", user.Name, 4);
        }
        void Dice(User user, int dice1, int dice2)
        {
            MessageBox(0, $"Вы бросили кубики. На первом - {dice1}, на втором - {dice2}", user.Name, 1);
        }
        void CurrentCell(User user, Cell cell)//логика отображения клетки
        { }
        void NoEnoughtMoney(User user)
        {
            MessageBox(0, $"У вас не хватает денег {user.Money}", user.Name, 1);
        }
        void MoneyChangedHandler(User user)//логика транзакции
        {
            GetType().GetProperty("Player" + (Test+1) + "_Money")
                               .SetValue(this, user.Money, null);
        }
        void GetCardpick(User user, CardPick cp)
        {
            MessageBox(0, cp.Name, user.Name, 1);
        }
        void postionHadler(User user)
        {
           PositionAnimation(user.PreviousPosition, user.Position);

        }
        public string Position { get; set; }
        public double Player1 { get; set; } = 1;
        public double Player2 { get; set; } = 0.3;
        public double Player3 { get; set; } = 0.3;
        public double Player4 { get; set; } = 0.3;
        public string Player1_Name { get; set; }
        public string Player2_Name { get; set; }
        public string Player3_Name { get; set; }
        public string Player4_Name { get; set; }
        public int Player1_Money { get; set; } 
        public int Player2_Money { get; set; }
        public int Player3_Money { get; set; }
        public int Player4_Money { get; set; }
        private ObservableCollection<List<Property>> listUsers;

        public ObservableCollection<List<Property>> ListUsers
        {
            get { return listUsers; }
            set
            {
                listUsers = value;
                RaisePropertyChanged("ListUsers");
            }
        }
        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
        }

        public RelayCommand TestCommand { get; set; }

        void RightSectionSeeder()
        {
            for (int i = 0; i < r.Session.Users.Count; i++)
            {
                GetType().GetProperty("Player" + (i+1)+"_Name")
                                .SetValue(this, r.Session.Users[i].Name, null);
                GetType().GetProperty("Player" + (i + 1) + "_Money")
                                .SetValue(this, r.Session.Users[i].Money, null);
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
                var next = current + i;
                
                if (next == 40)
                {
                    SeedPositions(Test);
                   
                    current = 0;
                    Iterations -= i;
                    i = 0;
                    next = current + i;
                }
                if(next > 40)
                {
                    next -= 40;
                }
                var str = (string)GetType().GetProperty("Position_" + Test)
                    .GetValue(this, null);
                string[] positions = str.Split(',');
                int x = Int32.Parse(positions[0]);
                
                int y = Int32.Parse(positions[1]);
                if((next)%10 == 0)
                {
                    DirectionChanger(out Direction, next);
                }
                int pixels = 53;
                switch (Direction)
                {
                    case Movement.Down:
                        if((current+i) == 9)
                        {
                            pixels += 20;
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
                            if ((next) == 10)
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
                        if (next == 20)
                        {
                            pixels = 75;
                        }
                        if (next == 29)
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
                        if(next == 39)
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
                        if (next == 40)
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