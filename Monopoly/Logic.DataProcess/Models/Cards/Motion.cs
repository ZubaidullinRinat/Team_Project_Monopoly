using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cards
{
    public class Motion : Card
    {
        public int Position { get; set; }
        public Motion(string Name, int Id, int Position) : base(Name, Id)
        {
            this.Position = Position;
        }
    }
}
