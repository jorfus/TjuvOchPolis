using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Thief : Person
    {
        public Thief() : this(new City(0, 0), new bool[] { true, true, true, true }) { }
        public Thief(City city, bool[] itemsArray) : base(city, itemsArray)
        {
            PersonType = "Thief";
        }

        public override string ToString()
        {
            string positionStr = $"{Position[0]}, {Position[1]}";
            string velocityStr = $"{Velocity[0]}, {Velocity[1]}";

            string listStr = "";
            foreach (Item item in Items)
                listStr += $"{item}\n";

            return $"Type: {PersonType}\nPos: {positionStr} Next move: {velocityStr}\n\nInventory:\n{listStr}";
        }
    }
}
