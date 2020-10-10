using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Tracing;

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
                person = new Citizen(this, ThePersonList, false);
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

            PlacePersons();
        }

        void PlacePersons()
        {
            foreach (Person person in ThePersonList)
            {
                (int column, int row) = person.GetPosition();

                switch (person.PersonType)
                {
                    case "Citizen":
                        TheCity[row, column] = 'M';
                        break;
                    case "Police":
                        TheCity[row, column] = 'P';
                        break;
                    case "Thief":
                        TheCity[row, column] = 'T';
                        break;
                    default:
                        break;
                }
            }
        }
        public void MovePersons()
        {
            foreach (Person person in ThePersonList)
            {
                (int column, int row) = person.GetPosition();
                TheCity[row, column] = ' ';

                person.NewPosition(TheCity);
            }

            List<List<Person>> personCollisionLists = new List<List<Person>>();

            for (int index = 0; index < ThePersonList.Count - 1;)
            {
                List<Person> positionMatches = ThePersonList.FindAll(x => x.GetPosition() == ThePersonList[index].GetPosition());

                if (positionMatches.Count > 1)
                {
                    (int column, int row) = ThePersonList[index].GetPosition();
                    personCollisionLists.Add(positionMatches);
                    ThePersonList.RemoveAll(x => x.GetPosition() == (column, row));
                }
                else
                    index++;
            }

            foreach (List<Person> personList in personCollisionLists)
            {
                Person thief = personList.Find(x => x.PersonType == "Thief");
                
                if (thief == null)
                    break;
                else
                {
                    Person police = personList.Find(x => x.PersonType == "Police");

                    if (police == null)
                    {
                        Person citizen = personList.Find(x => x.PersonType == "Citizen");

                        if (citizen == null)
                            break;
                        else
                            thief.Take(citizen);
                    }
                    else if (police.PersonType == "Police")
                    {
                        police.Take(thief);
                        personList.Remove(thief);
                    }
                } //BACKNING EFTER KOLLISION ?????
            }

            PlacePersons();
        }
        public string DrawCity()
        {
            string str = "";

            for (int row = 0; row <= TheCity.GetUpperBound(0); row++)
            {
                for (int column = 0; column <= TheCity.GetUpperBound(1); column++)
                    str += TheCity[row, column];

                str += "\n";
            }
            return str;
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
