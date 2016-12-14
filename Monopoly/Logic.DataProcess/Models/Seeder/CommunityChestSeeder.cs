using Logic.DataProcess.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Seeder
{
    public static class CommunityChestSeeder
    {
        public static List<Card> SeedCards()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Transaction("Получите ", 1, 100));
            cards.Add(new Transaction("Получите ", 2, 50));
            cards.Add(new Transaction("Получите ", 3, 25));
            cards.Add(new Transaction("Получите ", 4, 10));
            cards.Add(new Transaction("Получите ", 5, 10));
            cards.Add(new Transaction("Получите ", 6, 200));
            cards.Add(new Transaction("Получите ", 7, 100));
            cards.Add(new Transaction("Получите ", 8, 20));           
            cards.Add(new Transaction("Заплатите в казну ", 9, -50));
            cards.Add(new Transaction("Заплатите в казну ", 10, -50));
            cards.Add(new Transaction("Заплатите в казну ", 11, -100));
            cards.Add(new PrisonCard("Идите в тюрьму", 12));          
            cards.Add(new JailRelease("Вы получили освобождение из тюрьмы", 13));
            cards.Add(new Motion("Вернитесь на житную",14,12));
            return cards;
        }
    }
}
