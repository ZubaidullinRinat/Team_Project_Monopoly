using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        public StartViewModel()
        {
            LaunchMain = new RelayCommand(() =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowMainView"));
            });
        }

        public RelayCommand LaunchMain { get; private set; }
    }
}
