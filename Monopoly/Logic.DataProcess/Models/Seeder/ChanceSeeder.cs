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
            cards.Add(new Motion("Идите на клетку Курская", 1, 12));
            cards.Add(new Transaction("Получите взятку", 2, 1200));
            cards.Add(new PrisonCard("Идите в тюрьму", 3));
            cards.Add(new JailRelease("Вы получаете карту 'Освобождение из тюрьмы'", 4));
            return cards;
        }
    }
}
