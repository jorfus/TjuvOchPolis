using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TjuvOchPolis
{
    class Person
    {
        public string PersonType { get; set; }
        public int[] Position { get; private set; } = new int[2];
        public int[] Velocity { get; private set; } = { 0, 0 };
        public List<Item> Items { get; set; } = new List<Item>();

        protected Person() : this(new City(0, 0)) { }
        protected Person(City city)
        {
            Random rand = new Random();

            rand.Next(0, city.TheCity.GetUpperBound(0) + 2);
            rand.Next(0, city.TheCity.GetUpperBound(1) + 2);

            Item item;
            
            for (int index = 0; index < 4; index++)
            {
                int trueFalse = rand.Next(0, 2);

                if (trueFalse == 0)
                {
                    item = new Item(index);
                    Items.Add(item);
                }
            }
        }
        
        public override string ToString()
        {
            string positionStr = $"{Position[0]}, {Position[1]}";
            string velocityStr = $"{Velocity[0]}, {Velocity[1]}";

            string listStr = "";
            foreach (Item item in Items)
                listStr += $"{item}\n";

            return $"(BASE CLASS)\nPos: {positionStr} Next move: {velocityStr}\n\nInventory:\n{listStr}";
            
        }
    }
}
