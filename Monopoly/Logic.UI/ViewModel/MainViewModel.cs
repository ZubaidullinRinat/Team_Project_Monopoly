using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.DataProcess;
using System;
using System.Collections.Generic;
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
            BluePosition = "765,602,0,0";
        }
        void postionHadler(User user)
        {
            PositionAnimation();
            //Position = user.Name;
        }
        public string BluePosition { get; set; }
        public string Position { get; set; }

        public RelayCommand TestCommand { get; set; }

        void PositionAnimation()
        {
            var test = BluePosition.Split(',');
            int hor = Int32.Parse(test[0]);
            int vert = Int32.Parse(test[1]);
            MessageBox.Show(hor.ToString());
            while(hor != 674)
            {
                hor--;
                BluePosition = string.Format($"{hor.ToString()},{test[1]},{test[2]},{test[3]}");
            }
        }

        // объявить все лейблы и тектблоки из UI -------  FIX ME//
        // поля OWNER для Юзарей, кто купил эти улицы //
    }
}