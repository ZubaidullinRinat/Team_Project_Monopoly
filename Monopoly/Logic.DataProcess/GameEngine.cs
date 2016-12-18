using Logic.DataProcess.Models;
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
        public event Action<User> GetUsersProperties;
        //Add
        public event Action<User> JailRelease; //Вы освобождены
        public event Action<User> SetPrison; //Вы сели в тюрьму

        public event Action<User, int, int> Dice; //Вы бросили кубики 
        public event Action<User, Cell> CurrentCell;

        public event Action<User, CardPick> GetCardPick;

        public event Action<User> NoEnoughMoney;
        public event Action<User, int, int> Transaction;
                

        public List<Cell> Cells { get; private set; }
        public List<Card> Chances { get; private set; }
        public List<Card> CommunityChest { get; private set; }
        public List<Monopoly> Monopolies { get; private set; }
        public GameEngine()
        {
            SeedRandom();
            //Инициализация полей
            Cells = CellsSeeder.SeedCells();
            Chances = ChanceSeeder.SeedCards();
            CommunityChest = CommunityChestSeeder.SeedCards();
            Monopolies = MonopolySeeder.SeederMonopoly();
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
                //3,3
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
                if (user.IsInPrison)
                {
                    if (user.IdleCount == 0 || user.JailReleasePermisson)
                    {
                        JailRelease?.Invoke(user);
                        if (user.JailReleasePermisson)
                        {
                            Console.WriteLine("Вы воспользовались освобождением из тюрьмы");
                            user.JailReleasePermisson = false;
                            return;
                        }
                        else
                            Console.WriteLine("Вы особождены по истечению срока");
                        user.IsInPrison = false;
                        
                        return;
                    }
                    if (BuybackFromPrison?.Invoke(user) == true)
                    {
                        if (user.Money < 50)
                        {
                            NoEnoughMoney?.Invoke(user);
                            Console.WriteLine("Нет денег");
                            return;
                        }
                        else
                            Console.WriteLine("Вы освобождены");
                        user.Money -= 50;
                        user.IdleCount = 0;
                        user.IsInPrison = false;
                        JailRelease?.Invoke(user);
                        return;
                    }                    
                    else
                    {
                        if (user.IdleCount > 0)
                        {
                            user.IdleCount--;
                        }
                        return;
                    }
                }
                Console.WriteLine($"\n{user.Name} старая позиция {user.Position}");
                Console.WriteLine("{0} бросает кубики", user.Name);
                dices = DiceRoll();
                Console.WriteLine("Первый кубик - {0}, второй - {1}", dices[0], dices[1]);
                int move_count = dices[0] + dices[1];
                Dice?.Invoke(user, dices[0], dices[1]);
                int curr_position;

                user.Position += move_count;
                curr_position = user.Position;
                if (curr_position > 39)
                    user.Position = curr_position - 39;
                //Зацикливание 


                //Test
                //user.Position = 5;
                //Заглушка для полей, которых еще нет
                //if (user.Position > 10)
                //{
                //    Console.WriteLine("Больше 10");
                //    return;
                //}

                var Cell = Cells.Find(c => c.ID == user.Position);


                Console.WriteLine("Вы находитесь на {0}", Cell.Name);
                CurrentCell?.Invoke(user, Cell);

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
                        SetPrison?.Invoke(user);
                        Console.WriteLine("Вы в тюрьме");
                    }                    
                    return;
                }
                //Попадание на клетку шанса\общественной казны
                if (Cell is CardPick)
                {
                    var CardPickCard = Cell as CardPick;
                    GetCardPick?.Invoke(user, CardPickCard);
                    int r;
                    Card chanceCard;
                    switch (CardPickCard.Type)
                    {
                        case Models.Cells.Type.CommunityChest:
                            r = random.Next(1, CommunityChest.Count);
                            chanceCard = CommunityChest.Find(c => c.Id == r);
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
                        CurrentCell?.Invoke(user, newPosition);
                        if (Cells.Find(c => c.ID == user.Position) != null)
                            Console.WriteLine($"{user.Name} перемещается на клетку {newPosition.Name}");
                        return;
                    }
                    if (chanceCard is Transaction)
                    {
                        var transaction = chanceCard as Transaction;
                        if (transaction != null)
                            Console.WriteLine($"{transaction.Name}");
                        int previousAmount = user.Money;
                        Console.WriteLine($"{user.Name} старое количество денег - {previousAmount}");
                        if (user.Money < transaction.Cost)
                        {
                            NoEnoughMoney?.Invoke(user);
                            Console.WriteLine("У вас недостаточно денег!");
                            return;
                        }
                        
                        user.Money += transaction.Cost;
                        Transaction?.Invoke(user, previousAmount, user.Money);

                    }
                    if (chanceCard is PrisonCard)
                    {
                        var prison = Cell as Prison;
                        if (prison != null)
                            user.Position = Cells.Find(c => c == prison).ID;
                        user.IsInPrison = true;
                        user.IdleCount = 2;
                        SetPrison?.Invoke(user);
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
                        var newPosition = Cells.Find(c => c.ID == user.Position);
                        CurrentCell?.Invoke(user, newPosition);
                        if (Cells.Find(c => c.ID == user.Position) != null)
                            Console.WriteLine($"{user.Name} перемещается на клетку {newPosition.Name}");
                        return;
                    }
                }
                //Клетка газопровод
                if (Cell is Utilities)
                {
                    var utility = Cell as Utilities;
                    if (utility.Owner == null)
                    {
                        if (Buy?.Invoke(user) == true)
                        {
                            if (user.Money < utility.Cost)
                            {
                                NoEnoughMoney?.Invoke(user);
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= utility.Cost;
                            utility.Owner = user;
                        }
                    }
                    else
                    {
                        if (utility.Owner == user)
                        {
                            return;
                        }
                        List<Utilities> utilitiesOfOwner;
                        int cost;
                        utilitiesOfOwner = Cells.Where(a => a is Utilities).Select(a =>
                        {
                            if (a is Utilities)
                                return a as Utilities;
                            return null as Utilities;
                        }).Where(a => a.Owner == utility.Owner).ToList();

                        switch (utilitiesOfOwner.Count)
                        {
                            case 1:
                                cost = 4 * move_count;
                                break;
                            case 2:
                                cost = 10 * move_count;
                                break;
                            default:
                                return;
                        }
                        if (user.Money < cost)
                        {
                            NoEnoughMoney?.Invoke(user);
                            Console.WriteLine("Нет денег");
                            return;
                        }
                        int previousAmount = user.Money;
                        user.Money -= cost;
                        Transaction?.Invoke(user, previousAmount, user.Money);
                    }
                }

                //Клетка налога
                if (Cell is Tax)
                {
                    var Tax = Cell as Tax;
                    if (user.Money < Tax.Amount)
                    {
                        NoEnoughMoney?.Invoke(user);
                        Console.WriteLine("У вас недостаточно денег");
                        return;
                    }
                    int previousAmount = user.Money;
                    user.Money -= Tax.Amount;
                    Transaction?.Invoke(user, previousAmount, user.Money);
                }

                //Клетка Railway
                if (Cell is Railway)
                {
                    var RailwayStation = Cell as Railway;
                    if (RailwayStation.Owner == null)
                    {
                        if (Buy?.Invoke(user) == true)
                        {
                            if (user.Money < RailwayStation.Cost)
                            {
                                NoEnoughMoney?.Invoke(user);
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= RailwayStation.Cost;
                            RailwayStation.Owner = user;
                        }
                    }

                    else
                    {
                        if (RailwayStation.Owner == user)
                            return;
                        List<Railway> ownerOfRailways;
                        ownerOfRailways = Cells.Where(a => a is Railway).Select(a =>
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
                                return;
                        }
                        if (user.Money < cost)
                        {
                            NoEnoughMoney?.Invoke(user);
                            Console.WriteLine("Вам нехватает денег");
                            return;
                        }
                        int previousUserMoney = user.Money;
                        user.Money -= cost;
                        Transaction?.Invoke(user, previousUserMoney, user.Money);
                        int previousOwnerMoney = user.Money;
                        RailwayStation.Owner.Money += cost;
                        Transaction?.Invoke(user, previousOwnerMoney, user.Money);
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
                                NoEnoughMoney?.Invoke(user);
                                Console.WriteLine("Нет денег");
                                return;
                            }
                            user.Money -= Location.Price;
                            Location.Owner = user;
                                                        
                            user.Properties.Add(Location);

                            var needed = Monopolies
                                            .Where(m => m.PropertiesInMonopoly.Contains(Location.ID))
                                            .ToArray()[0].PropertiesInMonopoly;
                            var have = user.Properties.Select(s => s.ID).ToArray();



                            GetUsersProperties?.Invoke(user);

                            if (!needed.Except(have).Any())
                            {
                                Console.WriteLine("У вас монополия");
                                user.Properties.ForEach(i => 
                                {
                                    i.InMonopoly = true;
                                });
                            }
                        
                        
                        }
                        //Логика торгов
                    }

                    else
                    {
                        if (Location.Owner == user)
                            return;
                        if (!Location.InMonopoly)
                        {   
                            if(user.Money < Location.PropertyOnly)
                            {
                                NoEnoughMoney?.Invoke(user);
                                Console.WriteLine("Недостаточно денег");
                                return;
                            }
                            //user.Money -= Location.PropertyOnly;
                            //Location.Owner.Money += Location.PropertyOnly;
                            int previousUserMoney = user.Money;
                            user.Money -= Location.PropertyOnly;
                            Transaction?.Invoke(user, previousUserMoney, user.Money);
                            int previousOwnerMoney = user.Money;
                            Location.Owner.Money += Location.PropertyOnly;
                            Transaction?.Invoke(user, previousOwnerMoney, user.Money);
                        }
                        else
                        {
                            int cost;
                            switch (Location.Houses)
                            {
                                case 1:
                                    cost = Location.OneHouse;
                                    break;
                                case 2:
                                    cost = Location.TwoHouses;
                                    break;
                                case 3:
                                    cost = Location.ThreeHouses;
                                    break;
                                case 4:
                                    cost = Location.FourHouses;
                                    break;
                                default:
                                    return;
                            }
                            if (Location.InMonopoly)
                                cost = Location.Hotel;
                            if (user.Money < cost)
                            {
                                NoEnoughMoney?.Invoke(user);
                                Console.WriteLine("Недостаточно денег");
                                return;
                            }

                            int previousUserMoney = user.Money;
                            user.Money -= cost;
                            Transaction?.Invoke(user, previousUserMoney, user.Money);
                            int previousOwnerMoney = user.Money;
                            Location.Owner.Money += cost;
                            Transaction?.Invoke(user, previousOwnerMoney, user.Money);
                        }
                    }
                }
                var PropertyMonopoly = Cell as Property;                               

            }
            while (dices[0] == dices[1] && !user.IsInPrison);
        }


        public void TestMove(User user)
        {
            var boof = user.Position;
            boof += 5;
            if(boof > 39)
            {
                user.Position = boof - 40;
            }
            else
            {
                user.Position = boof;
            }
        }
    }
}
