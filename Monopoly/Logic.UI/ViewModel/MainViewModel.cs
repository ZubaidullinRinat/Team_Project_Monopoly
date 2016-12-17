using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                r.TestMove(r.Session.Users[0]);
            });
            //Новый репозиторий
            r = Repository.getInstance();
            //r.BuyRepo += BuyBackFromPrisonHandler;
            //r.BuyRepo += BuyHandler;
            //Этот кусок будет вызывать новую viewModel для определения количества пользователей
            r.Session.Users = new List<User>
            {
                UserSeeder("John"),
                UserSeeder("Andrew"),
                UserSeeder("Bob"),
                UserSeeder("Max")
            };
            BluePosition = "660,602,0,0";
            Player1_Name = r.Session.Users[0].Name.ToString();
            Player2_Name = r.Session.Users[1].Name.ToString();
            Player3_Name = r.Session.Users[2].Name.ToString();
            Player4_Name = r.Session.Users[3].Name.ToString();
        }
        void postionHadler(User user)
        {
            PositionAnimation();
            //Position = user.Name;
        }
        public string BluePosition { get; set; } 
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

        async void PositionAnimation()
        {
            while (Player2 > 0.1)
            {
                Player2 -= 0.05;
                Player3 -= 0.05;
                Player4 -= 0.05;
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
            var Positions = BluePosition.Split(',');
            int hor = Int32.Parse(Positions[0]);
            int vert = Int32.Parse(Positions[1]);
            while (hor > 570)
            {
                hor--;
                BluePosition = string.Format($"{hor.ToString()},{Positions[1]},0,0");
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
        }
        // объявить все лейблы и тектблоки из UI -------  FIX ME//
        // поля OWNER для Юзарей, кто купил эти улицы //
    }
}