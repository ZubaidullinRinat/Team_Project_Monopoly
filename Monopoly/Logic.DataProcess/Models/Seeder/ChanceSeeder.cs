using Logic.DataProcess.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Seeder
{
    public static class ChanceSeeder
    {
        public static List<Card> SeedCards()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Transaction("Получите ", 1, 100));
            cards.Add(new Transaction("Получите ", 2, 150));
            cards.Add(new Transaction("Получите ", 3, 50));
            cards.Add(new Transaction("Заплатите ", 4, -15));
            cards.Add(new Transaction("Заплатите ", 5, -20));
            cards.Add(new Transaction("Заплатите ", 6, -150));
            cards.Add(new Motion("Идите на клетку Курская", 7, 15));
            cards.Add(new Motion("Идите на клетку Площадь Маяковского", 8, 24));
            cards.Add(new Motion("Идите на клетку Полянку", 9, 11));
            cards.Add(new Motion("Идите на клетку Арбат", 10, 39));
            cards.Add(new PrisonCard("Идите в тюрьму", 11));
            cards.Add(new JailRelease("Вы получаете карту 'Освобождение из тюрьмы'", 12));            
            cards.Add(new Motion("Перейдите на поле вперед", 13, 0));
            cards.Add(new MoveCard("Перейдите на 3 клетки назад", 14, -3));
            return cards;
        }
    }
}
