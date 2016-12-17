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
        public List<Card> CommunityChest { get; private set; }
        public GameEngine()
        {
            SeedRandom();
            //Инициализация полей
            Cells = CellsSeeder.SeedCells();
            Chances = ChanceSeeder.SeedCards();
            CommunityChest = CommunityChestSeeder.SeedCards();
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
                //1,1
            };
        }

        private int getRandomCardId()
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
                Console.WriteLine("\n{0} бросает кубики", user.Name);
                dices = DiceRoll();
                Console.WriteLine("Первый кубик - {0}, второй - {1}", dices[0], dices[1]);              
                user.Position += dices[0] + dices[1];
                //Test
                //user.Position = 7;
                //Заглушка для полей, которых еще нет
                if (user.Position > 10)
                {
                    Console.WriteLine("Больше 10");
                    return;
                }

                var Cell = Cells.Find(c => c.ID == user.Position);


                Console.WriteLine("Вы находитесь на {0}", Cell.Name);
                

                Console.WriteLine(Cell.GetType().Name);

                //Клетку тюрьмы
                prisonCounter++;
                if (prisonCounter == 3)
                {                    
                    if (!user.IsInPrison && !user.JailReleasePermisson)
                    {
                        var prison = Cell as Prison;
                        user.Position = Cells.Find(c => c.ID == 10).ID;
                        user.IsInPrison = true;
                        user.IdleCount = 2;                        
                    }
                    else
                    {
                        if (user.IdleCount == 0 || user.JailReleasePermisson)
                        {
                            if (user.JailReleasePermisson)
                            {
                                Console.WriteLine("Вы воспользовались освобождением из тюрьмы");
                                user.JailReleasePermisson = false;
                                return;
                            }
                            user.IsInPrison = false;
                        }
                        else
                        {
                            if (BuybackFromPrison?.Invoke(user) == true)
                            {
                                if (user.Money < 500)
                                {
                                    Console.WriteLine("Нет денег");
                                    return;
                                }
                                user.Money -= 500;
                                user.IdleCount = 0;
                            }
                            else
                            {
                                if (user.IdleCount > 0)
                                {
                                    user.IdleCount--;
                                }
                            }
                        }
                                             
                    }
                    return;
                }
                //Попадание на клетку шанса\общественной казны
                if (Cell is CardPick)
                {
                    var CardPickCard = Cell as CardPick;
                    int r;
                    Card chanceCard;
                    switch (CardPickCard.Type)
                    {
                        case Models.Cells.Type.CommunityChest:
                            r = random.Next(1, CommunityChest.Count);
                            chanceCard = Chances.Find(c => c.Id == random.Next(1, Chances.Count));
                            //if(chanceCard != null)
                            //Console.WriteLine($"{user.Name} попал на клетку общественной казны '{chanceCard.Name}'");                            
                            break;
                        default:
                            r = random.Next(1, Chances.Count);
                            chanceCard = Chances.Find(c => c.Id == r);
                            //if (chanceCard != null)
                            //Console.WriteLine($"{user.Name} попал на клетку Шанс'{chanceCard.Name}'");
                            break;                        
                    }
                    if (chanceCard != null)
                        Console.WriteLine($"{user.Name} попал на клетку {CardPickCard.Type} '{chanceCard.Name}'");
                    if (chanceCard is Motion)
                    {
                        var motion = chanceCard as Motion;
                        Console.WriteLine($"{motion.Name}");
                        user.Position = motion.Position;
                        var newPosition = Cells.Find(c => c.ID == user.Position);
                        if (Cells.Find(c => c.ID == user.Position) != null)
                            Console.WriteLine($"{user.Name} перемещается на клетку {newPosition.Name}");
                        return;
                    }
                    if (chanceCard is Transaction)
                    {
                        var transaction = chanceCard as Transaction;
                        if (transaction != null)
                            Console.WriteLine($"{transaction.Name}");
                        Console.WriteLine($"{user.Name} старое количество денег - {user.Money}");
                        if(user.Money < transaction.Cost)
                        {
                            Console.WriteLine("У вас недостаточно денег!");
                            return;
                        }
                        user.Money += transaction.Cost;

                    }
                    if (chanceCard is PrisonCard)
                    {
                        var prison = Cell as Prison;
                        if (prison != null)
                            user.Position = Cells.Find(c => c == prison).ID;
                        user.IsInPrison = true;
                        user.IdleCount = 2;
                        return;
                    }
                    if (chanceCard is JailRelease)
                    {
                        user.JailReleasePermisson = true;
                        Console.WriteLine($"{chanceCard.Name}");
                    }
                    if (chanceCard is MoveCard)
                    {
                        var first_postion = user.Position;
                        var move = chanceCard as MoveCard;
                        user.Position = first_postion + move.MoveOn;
                        return;
                    }
                }

                //Клетка налога
                if(Cell is Tax)
                {
                    var Tax = Cell as Tax;
                    if(user.Money < Tax.Amount)
                    {
                        Console.WriteLine("У вас недостаточно денег");
                        return;
                    }
                    user.Money -= Tax.Amount;
                }

                //Клетка Railway
                if(Cell is Railway)
                {
                    var RailwayStation = Cell as Railway;
                    if (RailwayStation.Owner == null)
                    {
                        if (Buy?.Invoke(user) == true)
                        {
                            if (user.Money < RailwayStation.Cost)
                            {
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= RailwayStation.Cost;
                            RailwayStation.Owner = user;
                        }
                    }
                    else
                    {
                        List<Railway> ownerOfRailways;
                        ownerOfRailways = Cells.Select(a =>
                        {
                            if (a is Railway)
                                return a as Railway;
                            return null as Railway;
                        }).Where(a => a.Owner == RailwayStation.Owner).ToList();
                        int cost;
                        switch (ownerOfRailways.Count)
                        {
                            case 1:
                                cost = 25;
                                break;
                            case 2:
                                cost = 50;
                                break;
                            case 3:
                                cost = 100;
                                break;
                            case 4:
                                cost = 200;
                                break;
                            default:
                                cost = 0;
                                break;
                        }
                        if(user.Money < cost)
                        {
                            Console.WriteLine("Вам нехватает денег");
                            return;
                        }
                        user.Money -= cost;
                        RailwayStation.Owner.Money += cost;                        
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
        public void TestMove(User user)
        {
            user.Position = 9;
        }
    }
}
