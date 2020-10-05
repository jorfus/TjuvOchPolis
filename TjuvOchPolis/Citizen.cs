using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Citizen : Person
    {
        public Citizen() : this(new City(0, 0, 0, 0, 0), new List<Person>()) { }
        public Citizen(City city, List<Person> thePersonList) : base(city, thePersonList)
        {
            PersonType = "Citizen";
        }

        public override string ToString()
        {
            string positionStr = $"{Position[1] + 1}, {Position[0] + 1}";
            string velocityStr = $"{Velocity[0]}, {Velocity[1]}";

            string listStr = "";
            foreach (Item item in Items)
                listStr += $"{item}\n";

            return $"Type: {PersonType}\nPos: {positionStr} Next move: {velocityStr}\n\nInventory:\n{listStr}";

        }
    }
}
