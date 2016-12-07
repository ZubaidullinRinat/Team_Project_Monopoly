using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess
{
    //Основной класс для общения с viewModel'ями
    public class Repository
    {
        #region Singleton
        private static Repository instance;

        public static Repository getInstance()
        {
            if (instance == null)
                instance = new Repository();
            return instance;
        }
        #endregion

        //Объекты сессии и движка
        public GameEngine Engine { get; set; }
        public Session Session { get; set; }

        public Repository()
        {
            //Сингтоны движка и сессии
            Engine = GameEngine.getInstance();
            Session = Session.getInstance();
        }
    }
}
