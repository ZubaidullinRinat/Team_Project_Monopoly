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
            cards.Add(new Transaction("Заплатите налог", 2, -1200));
            return cards;
        }
    }
}
