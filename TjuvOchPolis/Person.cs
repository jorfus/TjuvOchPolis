using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        protected Person() : this(new City(0, 0, 0, 0, 0), new List<Person>()) { }
        protected Person(City city, List<Person> thePersonList)
        {
            Random rand = new Random();
            int column = 0;
            int row = 0;

            bool loop = true;
            while (loop)
            {
                column = rand.Next(0, city.TheCity.GetUpperBound(1) + 1);
                row = rand.Next(0, city.TheCity.GetUpperBound(0) + 1);

                if (thePersonList.Count != 0)
                {
                    foreach (Person person in thePersonList)
                    {
                        if (column == person.Position[1] && row == person.Position[0])
                        {
                            loop = true;
                            break;
                        }
                        else
                            loop = false;
                    }
                }
                else
                    loop = false;
            }

            Position[1] = column;
            Position[0] = row;
            city.PlacePerson(this);

            Item item;
            for (int index = 0; index < 4; index++)
            {
                int falseTrue = rand.Next(0, 2);

                if (falseTrue == 0)
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

            return $"(BASE CLASS)\nPos: {positionStr} Next move: {velocityStr}\n\nInventory:\n{listStr}";
            
        }
    }
}
