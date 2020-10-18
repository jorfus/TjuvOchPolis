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
        protected int[] Position { get; private set; } = new int[2];
        protected int[] Velocity { get; private set; } = new int[2];
        protected List<Item> Items { get; private set; } = new List<Item>();
        static Random Rand { get; set; } = new Random();

        protected Person() : this(new City(0, 0, 0, 0, 0), new List<Person>()) { }
        protected Person(City city, List<Person> thePersonList)
        {
            int column = 0;
            int row = 0;

            while (true)
            {
                column = Rand.Next(0, city.TheCity.GetUpperBound(1) + 1);
                row = Rand.Next(0, city.TheCity.GetUpperBound(0) + 1);

                if (thePersonList.Count != 0)
                {
                    Person person = thePersonList.Find(x => x.GetPosition() == (column, row));
                    if (person == null)
                        break;
                }
                else
                    break;
            }

            Position[1] = column;
            Position[0] = row;
        }

        void GetVelocity(bool reverseVelocity)
        {
            if (reverseVelocity)
            {
                Velocity[1] *= -1;
                Velocity[0] *= -1;
            }
            else
                do
                {
                    Velocity[1] = Rand.Next(-1, 2);
                    Velocity[0] = Rand.Next(-1, 2);

                } while (Velocity[1] == 0 && Velocity[0] == 0);
        }
        public void Take(Person person, bool random)
        {
            if (person.Items.Count != 0)
            {
                if (random)
                {
                    int randIndex = Rand.Next(0, person.Items.Count - 1);
                    Items.Add(person.Items[randIndex]);
                    person.Items.RemoveAt(randIndex);
                }
                else
                {
                    Items.AddRange(person.Items);
                    person.Items.Clear();
                }
            }
        }
        public (int, int) GetPosition()
        {
            return (Position[1], Position[0]);
        }
        public void NewPosition(char[,] theCity, bool reverseVelocity)
        {
            GetVelocity(reverseVelocity);

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
        }
        public override string ToString()
        {
            string positionStr = $"{Position[1] + 1}, {Position[0] + 1}";
            string velocityStr = $"{Velocity[1]}, {Velocity[0]}";

            string listStr = "";
            foreach (Item item in Items)
                listStr += $"{item}\n";

            return $"(BASE CLASS)\nPos: {positionStr} Direction: {velocityStr}\n\nInventory:\n{listStr}";
            
        }
    }
}
