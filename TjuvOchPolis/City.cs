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
using System.Xml;
using System.Threading;

namespace TjuvOchPolis
{
    class City
    {
        public char[,] TheCity { get; private set; }
        List<Person> ThePersonList { get; set; } = new List<Person>();
        int[] TheftCounter { get; set; } = { 0, 0 }; 
        int[] ArrestCounter { get; set; } = { 0, 0 };

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

        void PersonInteractions(List<List<Person>> movedPersonsLists)
        {
            TheftCounter[0] = ArrestCounter[0] = 0;

            foreach (List<Person> personList in movedPersonsLists)
            {
                if (personList.Count > 1)
                {
                    Person thief = personList.Find(x => x.PersonType == "Thief"); //Add logic for removing one thief per police in collision

                    if (thief == null)
                        continue;
                    else
                    {
                        Person police = personList.Find(x => x.PersonType == "Police");

                        if (police == null)
                        {
                            Person citizen = personList.Find(x => x.PersonType == "Citizen");

                            if (citizen == null)
                                continue;
                            else
                            {
                                thief.Take(citizen, true);
                                TheftCounter[0]++;
                            }
                        }
                        else if (police.PersonType == "Police")
                        {
                            police.Take(thief, false);
                            personList.Remove(thief);
                            ArrestCounter[0]++;
                        }
                    }
                }
            }
        }
        public void MovePersons()
        {
            foreach (Person person in ThePersonList)
            {
                (int column, int row) = person.GetPosition();
                TheCity[row, column] = ' ';

                person.NewPosition(TheCity, false);
            }

            List<List<Person>> maybeCollidedPersonsLists = new List<List<Person>>(); 
            List<Person> maybeCollidedPersons = new List<Person>();

            while (ThePersonList.Count > 1)
            {
                maybeCollidedPersons = ThePersonList.FindAll(x => x.GetPosition() == ThePersonList[0].GetPosition());
                ThePersonList.RemoveAll(x => x.GetPosition() == maybeCollidedPersons[0].GetPosition());
                maybeCollidedPersonsLists.Add(maybeCollidedPersons);
            }

            PersonInteractions(maybeCollidedPersonsLists);

            List<Person> reversedPersons = new List<Person>();
            foreach (List<Person> personList in maybeCollidedPersonsLists)
            {
                if (personList.Count > 1)
                    foreach (Person person in personList)
                    {
                        person.NewPosition(TheCity, true);
                        reversedPersons.Add(person);
                    }
                else
                    ThePersonList.Add(personList[0]);
            }

            maybeCollidedPersonsLists.Clear();
            maybeCollidedPersons.Clear();

            List<Person> collidedPersons = new List<Person>();

            while (true)
            {
                foreach (Person person in reversedPersons)
                {
                    Person collidedPerson = ThePersonList.Find(x => x.GetPosition() == person.GetPosition());

                    if (collidedPerson != null)
                    {
                        collidedPerson.NewPosition(TheCity, true);
                        collidedPersons.Add(collidedPerson);
                        ThePersonList.Remove(collidedPerson);
                    }
                }

                ThePersonList.AddRange(reversedPersons);
                reversedPersons.Clear();

                if (collidedPersons.Count == 0)
                    break;
                else
                {
                    reversedPersons.AddRange(collidedPersons);
                    collidedPersons.Clear();
                }
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
        public void InteractionMessages(int interactionCountOneOne, int interactionCountOneTwo, int interactionCountTwoOne, int interactionCountTwoTwo,
                                        string interactionMessageOne, string interactionMessageTwo, string countMessageOne, string countMessageTwo)
        {
            Console.SetCursorPosition(interactionMessageOne.Length, TheCity.GetUpperBound(0) + 1);
            Console.Write($"{countMessageOne}{interactionCountOneTwo}");
            Console.SetCursorPosition(interactionMessageTwo.Length, TheCity.GetUpperBound(0) + 2);
            Console.Write($"{countMessageTwo}{interactionCountTwoTwo}");

            Thread.Sleep(100);

            InteractionMessages(interactionCountOneOne, interactionCountOneTwo, interactionMessageOne, countMessageOne.Length, 1);
            InteractionMessages(interactionCountTwoOne, interactionCountTwoTwo, interactionMessageTwo, countMessageTwo.Length, 2);
        }
        void InteractionMessages(int countOne, int countTwo, string interactionMessage, int countMessageLength, int row)
        {
            for (int counter = 1; counter <= countOne; counter++)
            {
                Console.SetCursorPosition(0, TheCity.GetUpperBound(0) + row);
                Console.Write(interactionMessage);

                Console.SetCursorPosition(interactionMessage.Length + countMessageLength, TheCity.GetUpperBound(0) + row);
                Console.Write(countTwo + counter);
                
                Thread.Sleep(2000);

                Console.SetCursorPosition(0, TheCity.GetUpperBound(0) + row);
                Console.Write(new string(' ', interactionMessage.Length));
                Thread.Sleep(100);
            }
        }
        public (int, int) GetInteractionCount(char messageType)
        {
            if (messageType == 'T')
                return (TheftCounter[0], TheftCounter[1]);
            else if (messageType == 'A')
                return
                    (ArrestCounter[0], ArrestCounter[1]);
            else
                return (-1, -1);
        }
        public void SetInterActionCount(char countType)
        {
            if (countType == 'T')
            {
                TheftCounter[1] += TheftCounter[0];
                TheftCounter[0] = 0;
            }
            else if (countType == 'A')
            {
                ArrestCounter[1] += ArrestCounter[0];
                ArrestCounter[0] = 0;
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
