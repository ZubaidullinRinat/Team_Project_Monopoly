using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cards
{
    public class MoveCard : Card
    {
        public int MoveOn { get; set; }
        public MoveCard(string Name, int Id, int MoveOn) : base(Name, Id)
        {
            this.MoveOn = MoveOn;
        }
    }
}
