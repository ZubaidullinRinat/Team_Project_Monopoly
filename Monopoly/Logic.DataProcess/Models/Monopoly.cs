using Logic.DataProcess.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models
{
    public class Monopoly
    {
        public int Id { get; set; }
        public int[] PropertiesInMonopoly { get; set; }
        public Monopoly(int Id, int[] PropertiesInMonopoly)
        {
            this.Id = Id;
            this.PropertiesInMonopoly = PropertiesInMonopoly;
        }
    }
}
