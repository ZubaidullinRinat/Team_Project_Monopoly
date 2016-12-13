using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cards
{
    public class Card
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Card(string Name, int Id)
        {
            this.Name = Name;
            this.Id = Id;
        }
    }
}
