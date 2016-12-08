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

        public event Func<User, bool> BuyRepo;

        //Объекты сессии и движка
        public GameEngine Engine { get; set; }
        public Session Session { get; set; }

        public Repository()
        {
            //Сингтоны движка и сессии
            Engine = GameEngine.getInstance();
            Engine.Buy += (user) =>
            {
                return BuyRepo?.Invoke(user); //Надеюсь, меня не исключат за это говно;
            };
            Session = Session.getInstance();
        }
        public void NewMove(User user)
        {
            Engine.NewMove(user);
        }


    }
}
