using Logic.DataProcess.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Seeder
{
    public static class MonopolySeeder
    {
        public static List<Monopoly> SeederMonopoly()
        {
            List<Monopoly> Monopolies = new List<Monopoly>();
            Monopolies.Add(new Monopoly(1, new int[] {1,3}));
            Monopolies.Add(new Monopoly(2, new int[] {6,8,9}));
            Monopolies.Add(new Monopoly(3, new int[] {11,13,14}));
            Monopolies.Add(new Monopoly(4, new int[] {16,18,19}));
            Monopolies.Add(new Monopoly(5, new int[] {21,23,24}));
            Monopolies.Add(new Monopoly(6, new int[] {26,27,29}));
            Monopolies.Add(new Monopoly(7, new int[] {31,32,34}));
            Monopolies.Add(new Monopoly(8, new int[] {37,39}));
            return Monopolies;

        }
    }
    
}
