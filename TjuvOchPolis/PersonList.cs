using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class PersonList
    {
        public List<Person> ThePersonList { get; private set; } = new List<Person>();

        public PersonList(City city, int citizens, int police, int thieves)
        {
            Person person;
            for (int i = 1; i <= citizens; i++)
                person = new Citizen(city);
            for (int i = 1; i <= police; i++)
                person = new Police(city);
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
