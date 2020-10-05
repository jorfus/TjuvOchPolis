using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TjuvOchPolis
{
    class City
    {
        public char[,] TheCity { get; private set; }
        public List<Person> ThePersonList { get; private set; } = new List<Person>();

        public City(int column, int row, int citizens, int police, int thieves)
        {
            TheCity = new char[row, column];

            for (row = 0; row <= TheCity.GetUpperBound(0); row++)
                for (column = 0; column <= TheCity.GetUpperBound(1); column++)
                    TheCity[row, column] = ' ';

            Person person;
            for (int i = 1; i <= citizens; i++)
            {
                person = new Citizen(this, ThePersonList);
                ThePersonList.Add(person);
            }
            for (int i = 1; i <= police; i++)
            {
                person = new Police(this, ThePersonList);
                ThePersonList.Add(person);
            }
            for (int i = 1; i <= thieves; i++)
            {
                person = new Thief(this, ThePersonList);
                ThePersonList.Add(person);
            }
        }

        public void PlacePerson(Person person)
        {
            int row = person.Position[0];
            int column = person.Position[1];

            switch (person)
            {
                case Citizen citizen:
                    TheCity[row, column] = 'M';
                    break;
                case Police police:
                    TheCity[row, column] = 'P';
                    break;
                case Thief thief:
                    TheCity[row, column] = 'T';
                    break;
                default:
                    break;
            }
        }
        public void DrawCity()
        {
            for (int row = 0; row <= TheCity.GetUpperBound(0); row++)
            {
                for (int column = 0; column <= TheCity.GetUpperBound(1); column++)
                    Console.Write(TheCity[row, column]);

                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            string listStr = "";
            foreach (Person item in ThePersonList)
                listStr += $"{item}\n";

            return listStr;
        }
    }
}
