using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace Logic.DataProcess.Models.Cells
{
    public class Property : Cell
    {
        public int PropertyOnly { get; private set; }

        public int OneHouse { get; private set; }
        public int TwoHouses { get; private set; }
        public int ThreeHouses { get; private set; }
        public int FourHouses { get; private set; }
        public int Hotel { get; private set; }

        public int HouseCost { get; private set; }
        public int HotelCost { get; private set; }

        public int Mortgage { get; private set; }
        public bool IsMortaged { get; private set; }

        public User Owner { get; private set; }

        public int Houses { get; private set; }
        public bool IsHotel { get; private set; }

        //TODO 
        //MONOPOLY 

        public Property(string _name, int _ID, int _propertyOnly, int _oneHouse, int _twoHouses, int _threeHouses, 
                        int _fourHouses, int _hotel, int _houseCost, int _hotelCost, int _mortgage) 
                        : base(_name, _ID)
        {
            PropertyOnly = _propertyOnly;
            OneHouse = _oneHouse;
            TwoHouses = _twoHouses;
            ThreeHouses = _threeHouses;
            FourHouses = _fourHouses;
            Hotel = _hotel;
            HouseCost = _houseCost;
            HotelCost = _houseCost;
            Mortgage = _mortgage;           
        }
    }
}
