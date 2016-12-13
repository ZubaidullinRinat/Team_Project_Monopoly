using Logic.DataProcess.Models.Cards;
using Logic.DataProcess.Models.Cells;
using Logic.DataProcess.Models.Seeder;
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
        public event Func<User, bool?> BuybackFromPrison;

        public List<Cell> Cells { get; private set; }
        public List<Card> Chances { get;  private set; }
        
        public GameEngine()
        {
            SeedRandom();
            //Инициализация полей
            Cells = CellsSeeder.SeedCells();
            Chances = ChanceSeeder.SeedCards();
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

        public int GetRandomCardId()
        {
            int cardId = random.Next(1, Chances.Count);        
            return cardId;
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
                Console.WriteLine("{0} бросает кубики", user.Name);
                dices = DiceRoll();
                Console.WriteLine("Первый кубик - {0}, второй - {1}", dices[0], dices[1]);
                user.Position += dices[0] + dices[1];
                
                //Заглушка для полей, которых еще нет
                if (user.Position > 10)
                {
                    Console.WriteLine("Больше 10");
                    return;
                }

                var Cell = Cells.Find(c => c.ID == user.Position);


                Console.WriteLine("Вы находитесь на {0}", Cell.Name);


                Console.WriteLine(Cell.GetType().Name);

                //Попадание на клетку тюрьмы
                prisonCounter++;
                if (Cell is Prison || prisonCounter == 3)
                {
                    if (!user.IsInPrison)
                    {
                        user.IsInPrison = true;
                        user.IdleCount = 3;
                    }
                    else
                    {
                        if (BuybackFromPrison?.Invoke(user) == true)
                        {
                            if (user.Money < 50000)
                            {
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= 50000;
                            user.IdleCount = 0;
                        }
                        else
                        {
                            if (user.IdleCount > 0)
                            {
                                user.IdleCount--;
                            }
                        }
                        if (user.IdleCount == 0)
                            user.IsInPrison = false;
                    }
                }
                //Попадание на клетку шанса\общественной казны
                if (Cell is CardPick)
                {
                    var CardPickCard = Cell as CardPick;
                    
                    switch (CardPickCard.Type)
                    {
                        case Models.Cells.Type.CommunityChest:
                            var chestCard = Chances.Find(c => c.Id == GetRandomCardId());
                            Console.WriteLine($"{user.Name} попал на клетку общественной казны '{chestCard.Name}'");                            
                            return;
                        case Models.Cells.Type.Chance:
                            var chanceCard = Chances.Find(c => c.Id == GetRandomCardId());
                            //Console.WriteLine($"{user.Name} попал на клетку шанс '{chanceCard.Name}'");
                            if (chanceCard is Motion)
                            {
                                var motion = chanceCard as Motion;
                                Console.WriteLine($"{motion.Name}");
                                user.Position = motion.Position;
                                var newPosition = Cells.Find(c => c.ID == user.Position);
                                if (Cells.Find(c => c.ID == user.Position) != null)
                                   
                                Console.WriteLine($"{user.Name} перемещается на клетку {newPosition.Name}");
                            }
                            if(chanceCard is Transaction)
                            {
                                var transaction = chanceCard as Transaction;
                                Console.WriteLine($"{transaction.Name}");
                                user.Money += transaction.Cost;
                                Console.WriteLine($"{user.Name} новое количество денег - {user.Money}");
                            }
                            
                            return;
                            
                        default:
                            break;
                    }
                }
                //Попадания на клетку недвижимости 
                if (Cell is Property)
                {
                    var Location = Cell as Property;
                    if (Location.Owner == null)
                    {
                        if (Buy?.Invoke(user) == true)
                        {
                            if (user.Money < Location.Price)
                            {
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= Location.Price;
                            Location.Owner = user;
                        }
                        //Логика торгов
                    }
                    else
                    {
                        if (!Location.InMonopoly)
                        {
                            user.Money -= Location.PropertyOnly;
                            Location.Owner.Money += Location.PropertyOnly;
                        }
                        else
                        {
                            //логика в монополии
                        }
                    }
                }


            }
            while (dices[0] == dices[1] && !user.IsInPrison);
        }
    }
}
