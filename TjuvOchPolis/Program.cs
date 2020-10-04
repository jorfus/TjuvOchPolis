using System;

namespace TjuvOchPolis
{
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City(0, 0);
            PersonList personList = new PersonList(3);
            
            Person person;

            while (true)
            {
                

                Console.Write(personList);
                Console.ReadKey(true);
            }
        }
    }
}
