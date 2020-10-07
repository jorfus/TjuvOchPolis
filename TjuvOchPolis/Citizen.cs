using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Citizen : Person
    {
        public Citizen() : this(new City(0, 0, 0, 0, 0), new List<Person>(), false) { }
        public Citizen(City city, List<Person> thePersonList, bool randomItems) : base(city, thePersonList)
        {
            PersonType = "Citizen";

            Random rand = new Random();
            Item item;

            for (int index = 0; index < 4; index++)
            {
                int falseTrue = rand.Next(0, 2);

                if (falseTrue == 1 || randomItems == false)
                {
                    item = new Item(index);
                    Items.Add(item);
                }
            }
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
