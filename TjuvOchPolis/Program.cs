using System;

namespace TjuvOchPolis
{
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City(5, 5, 2, 2, 2);
            Console.Write(city.DrawCity());

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        RunSimulation(city);
                        break;
                    case ConsoleKey.Tab:
                        Console.Write(city);
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public static void RunSimulation(City city)
        {
            city.MovePersons();

            Console.Clear();
            Console.Write(city.DrawCity());
        }
    }
}
