using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess.Models.Cells
{
    public class CardPick : Cell
    {
        public Type Type { get; private set; }

        //TODO
        //Action

        public CardPick(string _name, int _ID, Type _type) : base(_name, _ID)
        {
            Type = _type;
        }
    }
    public enum Type
    {
        CommunityChest,
        Chance
    }
}
