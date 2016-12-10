using Logic.DataProcess.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Seeder
{
    public static class CellsSeeder
    {
        public static List<Cell> SeedCells()
        {
            List<Cell> cells = new List<Cell>();

            cells.Add(new Cell("ВПЕРЕД", 0));
            cells.Add(new Property("ЖИТНАЯ УЛ.", 1, 60, 2, 10, 30, 90, 160, 230, 50, 50, 30, (user) => { Console.WriteLine("Action"); }));
            cells.Add(new CardPick("ОБЩЕСТВЕННАЯ КАЗНА", 2, Models.Cells.Type.CommunityChest));
            cells.Add(new Property("НАГАТИНСКАЯ УЛ.", 3, 60, 4, 20, 60, 180, 320, 450, 50, 50, 30, (user) => { Console.WriteLine("Action"); }));
            cells.Add(new Tax("ПОДОХОДНЫЙ НАЛОГ", 4, 200));
            cells.Add(new Railway("РИЖСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 5));
            cells.Add(new Property("ВАРШАВСКОЕ ШОССЕ", 6, 100, 6, 30, 90, 270, 400, 550, 50, 50, 50, (user) => { Console.WriteLine("Action"); }));
            cells.Add(new CardPick("ШАНС", 7, Models.Cells.Type.Chance));
            cells.Add(new Property("УЛ. ОГАРЕВА", 8, 100, 6, 30, 90, 270, 400, 550, 50, 50, 50, (user) => { Console.WriteLine("Action"); }));
            cells.Add(new Property("ПЕРВАЯ ПАРКОВАЯ УЛ.", 9, 110, 8, 40, 100, 300, 450, 600, 50, 50, 60, (user) => { Console.WriteLine("Action"); }));
            cells.Add(new Cell("ТЮРЬМА", 10));

            return cells;
        }
    }
}
