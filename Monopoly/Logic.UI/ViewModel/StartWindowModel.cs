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
        public StartWindowModel()
        {
            LaunchMain = new RelayCommand(() => 
            {
                Messenger.Default.Send(new NotificationMessage("StartMain"));
            });
        }
        public RelayCommand LaunchMain { get; set; }
    }
}