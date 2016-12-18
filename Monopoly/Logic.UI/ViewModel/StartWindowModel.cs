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
    public class StartWindowModel : ViewModelBase
    {
        List<int> _source = new List<int> { 2, 3, 4 };
        public List<int> Source
        {
            get { return _source; }
        }
        public string Active3 { get; set; } = "False";
        public string Active4 { get; set; } = "False";
        public string Name1 { get; set; } = "Donald Trump";
        public string Name2 { get; set; } = "Bill Gates";
        public string Name3 { get; set; } = "...";
        public string Name4 { get; set; } = "...";
        string _theSelectedItem = null;
        public string TheSelectedItem
        {
            get { return _theSelectedItem; }
            set {
                if (value.ToString() == "2")
                {
                    Active3 = "False";
                    Active4 = "False";
                }
                if (value.ToString() == "3")
                {
                    Active3 = "True";
                    Active4 = "False";
                }
                if (value.ToString() == "4")
                {
                    Active3 = "True";
                    Active4 = "True";
                }
                _theSelectedItem = value; } // NotifyPropertyChanged
        }


        public StartWindowModel()
        {
            LaunchMain = new RelayCommand(() =>
            {
                try
                {

                    if (Name1 == String.Empty || Name2 == String.Empty || Name3 == String.Empty || Name4 == String.Empty)
                    {
                        throw new ArgumentException("Name can't be empty!");
                    }
                    Messenger.Default.Send(new NotificationMessage("StartMain"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

        }
        public RelayCommand LaunchMain { get; set; }
        public RelayCommand OpenTextBoxes { get; private set; }

    }
}