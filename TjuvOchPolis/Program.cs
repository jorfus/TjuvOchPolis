using System;

namespace TjuvOchPolis
{
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City(10, 5, 4, 2, 2);
            city.DrawCity();

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        city.DrawCity();
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
    }
}
