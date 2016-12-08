using Logic.DataProcess.Models.Cells;
using Logic.DataProcess.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess
{
    //Основные методы игры
    public class GameEngine
    {
        #region Singleton
        private static GameEngine instance;

        public static GameEngine getInstance()
        {
            if (instance == null)
                instance = new GameEngine();
            return instance;
        }
        #endregion Singleton

        #region Random
        static Random random;
        /// <summary>
        /// Создаем объект типа Random
        /// </summary>
        static void SeedRandom()
        {
            random = new Random();
        }
        #endregion

        public event Func<User, bool?> Buy;

        public List<Cell> Cells { get; private set; }

        public GameEngine()
        {
            SeedRandom();
            //Инициализация полей
            Cells = CellsSeeder.SeedCells();
        }

        /// <summary>
        /// Кидать кубики
        /// </summary>
        /// <returns></returns>
        public int[] DiceRoll()
        {
            return new int[]
            {
                random.Next(1,7),
                random.Next(1,7)
            };
        }

        /// <summary>
        /// Ход игрока
        /// </summary>
        /// <param name="user"></param>
        public void NewMove(User user)
        {
            int[] dices;

            int prisonCounter = 0;

            //Для дублей
            do
            {
                //Проверка на 3 дубля
                prisonCounter++;
                if (prisonCounter == 3)
                {
                    user.IsInPrison = true;
                    return;
                }
                Console.WriteLine("{0} бросает кубики", user.Name);
                dices = DiceRoll();
                Console.WriteLine("Первый кубик - {0}, второй - {1}", dices[0], dices[1]);
                user.Position += dices[0] + dices[1];
                //Заглушка для полей, которых еще нет
                if(user.Position > 10)
                {
                    Console.WriteLine("Больше 10");
                    return;
                }

                var Cell = Cells.Find(c => c.ID == user.Position);               

                Console.WriteLine("Вы находитесь на {0}", Cell.Name);


                Console.WriteLine(Cell.GetType().Name);

                if(Cell is Property)
                {
                    var Location = Cell as Property;
                    if(Location.Owner == null)
                    {
                        Console.WriteLine(Buy?.Invoke(user));
                    }
                }


            }
            while (dices[0] == dices[1]);
        }
    }
}
