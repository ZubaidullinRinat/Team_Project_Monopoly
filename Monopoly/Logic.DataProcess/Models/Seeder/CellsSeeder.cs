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
            cells.Add(new Property("ЖИТНАЯ УЛ.", 1, 60, 2, 10, 30, 90, 160, 250, 50, 50, 30));
            cells.Add(new CardPick("ОБЩЕСТВЕННАЯ КАЗНА", 2, Models.Cells.Type.CommunityChest));
            cells.Add(new Property("НАГАТИНСКАЯ УЛ.", 3, 60, 4, 20, 50, 180, 320, 450, 50, 50, 30));
            cells.Add(new Tax("ПОДОХОДНЫЙ НАЛОГ", 4, 200));
            cells.Add(new Railway("РИЖСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 5, 200));
            cells.Add(new Property("ВАРШАВСКОЕ ШОССЕ", 6, 100, 6, 30, 90, 270, 400, 550, 50, 50, 50));
            cells.Add(new CardPick("ШАНС", 7, Models.Cells.Type.Chance));
            cells.Add(new Property("УЛ. ОГАРЕВА", 8, 100, 6, 30, 90, 270, 400, 550, 50, 50, 50));
            cells.Add(new Property("ПЕРВАЯ ПАРКОВАЯ УЛ.", 9, 120, 8, 40, 100, 300, 450, 600, 50, 50, 60));
            cells.Add(new Cell("ТЮРЬМА", 10));
            cells.Add(new Property("ПОЛЯНКА",11,140,10,50,150,450,625,750,100,100,70));
            cells.Add(new Utilities("ЭЛЕКТРОСТАНЦИЯ",12,150));
            cells.Add(new Property("СРЕТЕНКА",13,140,10,50,150,450,625,750,100,100,70));
            cells.Add(new Property("РОСТОВСКАЯ НАБ",14,160,12,50,180,500,700,900,100,100,80));
            cells.Add(new Railway("КУРСКАЯ ЖЕЛЕЗНАЯ ДОРОГА",15,200));
            cells.Add(new Property("РЯЗАНСКИЙ ПРОСПЕКТ",16,180,14,70,200,550,700,900,100,100,90));
            cells.Add(new CardPick("ОБЩЕСТВЕННАЯ КАЗНА", 17, Models.Cells.Type.CommunityChest));
            cells.Add(new Property("УЛ. ВАВИЛОВА",18,180,14,70,200,550,700,950,100,100,90));
            cells.Add(new Property("РУБЛЕВСКОЙ ШОССЕ",19,200,16,80,2220,600,800,1000,100,100,100));
            cells.Add(new Cell("БЕСПЛАТНАЯ СТОЯНКА",20));
            cells.Add(new Property("ТВЕРСКАЯ УЛ.",21,220,18,90,250,700,875,1050,150,150,110));
            cells.Add(new CardPick("ШАНС", 22, Models.Cells.Type.Chance));
            cells.Add(new Property("ПУШКИНСКА УЛ.",23, 220, 18, 90,250,700,875,1050,150,150,110));
            cells.Add(new Property("ПЛОЩАДЬ МАЯКОВСКОГО", 24, 240, 20,100,300,750,925,1100,150,150,120));
            cells.Add(new Railway("КАЗАНСКАЯ ЖЕЛЕЗНАЯ ДОРОГА",25,200));
            cells.Add(new Property("УЛ. ГРУЗИНСКИЙ ВАЛ",26,260,22,110,330,800,975,1150,150,150,130));
            cells.Add(new Property("УЛ. ЧАЙКОВСКОГО",27,260,22,110,330,800,975,1150,150,150,130));
            cells.Add(new Utilities("ВОДОПРОВОД",28,150));
            cells.Add(new Property("СМОЛЕНСКАЯ ПЛОЩАДЬ", 29,280, 24,120,360,850,1025,1200,150,150,140));
            cells.Add(new Prison("ОТПРАВЛЯЙТЕСЬ В ТЮРЬМУ",30));
            cells.Add(new Property("УЛ. ЩУСЕВА", 31,300,26,130,390,900,1100,1275,200,200,150));
            cells.Add(new Property("ГОГОЛЕВСКИЙ БУЛЬВАР",32,300,26,130,390,900,1100,1275,200,200,150));
            cells.Add(new CardPick("ОБЩЕСТВЕННАЯ КАЗНА",33,Models.Cells.Type.CommunityChest));
            cells.Add(new Property("КУТУЗОВСКИЙ ПРОСПЕКТ",34, 320, 28, 150, 450, 1000, 1200, 1400, 200, 200, 150));
            cells.Add(new Railway("ЛЕНИНГРАДСКАЯ ЖЕЛЕЗНАЯ ДОРОГА",35,200));
            cells.Add(new CardPick("ШАНС",36,Models.Cells.Type.Chance));
            cells.Add(new Property("МАЛАЯ БРОННАЯ", 37,350,35,175,500,1100,1300,1500,200,200,175));
            cells.Add(new Tax("УПЛАТИТЕ СВЕРХНАЛОГ", 38, 100));
            cells.Add(new Property("АРБАТ",39,400,50,200,600,1400,1700,2000,200,200,200));
         

            return cells;
        }
    }
}
