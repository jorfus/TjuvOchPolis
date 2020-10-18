using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Thief : Person
    {
        public Thief() : this(new City(0, 0, 0, 0, 0), new List<Person>()) { }
        public Thief(City city, List<Person> thePersonList) : base(city, thePersonList)
        {
            PersonType = "Thief";
        }

        public override string ToString()
        {
            string positionStr = $"{Position[1] + 1}, {Position[0] + 1}";
            string velocityStr = $"{Velocity[1]}, {Velocity[0]}";

            string listStr = "";
            foreach (Item item in Items)
                listStr += $"{item}\n";

            return $"Type: {PersonType}\nPos: {positionStr} Direction: {velocityStr}\n\nInventory:\n{listStr}";
        }
    }
}
