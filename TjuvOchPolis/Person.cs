using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace TjuvOchPolis
{
    class Person
    {
        public string PersonType { get; protected set; }
        public int[] Position { get; private set; } = new int[2];
        protected int[] Velocity { get; set; } = { 0, 0 };
        public List<Item> Items { get; private set; } = new List<Item>();

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
            city.PlacePersons(this);

            (Velocity[1], Velocity[0]) = GetVelocity();
        }

        (int, int) GetVelocity()
        {
            Random rand = new Random();
            int column;
            int row;

            if (rand.Next(0, 2) == 0)
                column = -1;
            else
                column = 1;

            if (rand.Next(0, 2) == 0)
                row = -1;
            else
                row = 1;

            return (column, row);
        }
        public (int, int) GetPosition(char[,] theCity)
        {
            Position[1] += Velocity[1];
            Position[0] += Velocity[0];

            if (Position[1] > theCity.GetUpperBound(1))
                Position[1] = 0;
            else if (Position[1] < 0)
                Position[1] = theCity.GetUpperBound(1);

            if (Position[0] > theCity.GetUpperBound(0))
                Position[0] = 0;
            else if (Position[0] < 0)
                Position[0] = theCity.GetUpperBound(0);

            (Velocity[1], Velocity[0]) = GetVelocity();

            return (Position[1], Position[0]);
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
