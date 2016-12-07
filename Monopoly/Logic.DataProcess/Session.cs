using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess
{
    //Вся информация о игр
    public class Session
    {
        //Игра одна - сингтон
        #region Singleton
        private static Session instance;

        public static Session getInstance()
        {
            if (instance == null)
                instance = new Session();
            return instance;
        }
        #endregion Singleton
        //Игроки
        public List<User> Users { get; set; }
    }
}
