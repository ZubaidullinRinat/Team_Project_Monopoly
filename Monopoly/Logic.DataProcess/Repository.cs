using Logic.DataProcess.Models.Cells;
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
        public event Func<User, bool> BuybackFromPrisonRepo;
        public event Action<User> GetUsersPropertiesRepo;

        public event Action<User> JailReleaseRepo; //Вы освобождены
        public event Action<User> SetPrisonRepo; //Вы сели в тюрьму

        public event Action<User, int, int> DiceRepo; //Вы бросили кубики 
        public event Action<User, Cell> CurrentCellRepo;

        public event Action<User, CardPick> GetCardPickRepo;

        public event Action<User> NoEnoughMoneyRepo;
        public event Action<User, int, int> TransactionRepo;
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
            Engine.BuybackFromPrison += (user) =>
            {
                return BuybackFromPrisonRepo?.Invoke(user);
            };
            Engine.GetUsersProperties += (user) =>
            {
                GetUsersPropertiesRepo?.Invoke(user);
            };
            Engine.JailRelease += (user) =>
            {
                JailReleaseRepo?.Invoke(user);
            };
            Engine.SetPrison += (user) =>
            {
                SetPrisonRepo?.Invoke(user);
            };
            Engine.Dice += (user, a, b) =>
            {
                DiceRepo?.Invoke(user, a, b);
            };
            Engine.CurrentCell += (user, position) =>
            {
                CurrentCellRepo?.Invoke(user, position);
            };
            Engine.GetCardPick += (user, card) =>
            {
                GetCardPickRepo?.Invoke(user, card);
            };
            Engine.NoEnoughMoney += (user) =>
            {
                NoEnoughMoneyRepo?.Invoke(user);
            };
            Engine.Transaction += (user, priviouse_m, current_m) =>
            {
                TransactionRepo?.Invoke(user, priviouse_m, current_m);
            };
            Session = Session.getInstance();
        }
        public void NewMove(User user)
        {
            Engine.NewMove(user);
        }
        public void TestMove(User user)
        {
            Engine.TestMove(user);
        }
    }
}
