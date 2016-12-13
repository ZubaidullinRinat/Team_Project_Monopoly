using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cards
{
    public class Transaction : Card
    {
        public int Cost { get; set; }
        public Transaction(string Name, int Id, int Cost) : base(Name, Id)
        {
            this.Cost = Cost;
        }
    }
}
