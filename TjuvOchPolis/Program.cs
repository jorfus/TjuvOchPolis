using System;
using System.Data.SqlTypes;
using System.Diagnostics.Tracing;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City(100, 25, 200, 110, 65);
            Console.WriteLine(city.DrawCity());
            
            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        RunSimulation(city);
                        break;
                    case ConsoleKey.Tab:
                        Console.Write(city);
                        break;
                    case ConsoleKey.R:
                        city = new City(4, 4, 2, 2, 2);
                        Console.Clear();
                        Console.WriteLine(city.DrawCity());
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }

        static void RunSimulation(City city)
        {
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                city.MovePersons();
                (int theftCountOne, int theftCountTwo) = city.GetInteractionCount('T');
                (int arrestCountOne, int arrestCountTwo) = city.GetInteractionCount('A');

                Console.SetCursorPosition(0, 0);
                Console.Write(city.DrawCity());

                city.InteractionMessages(theftCountOne, theftCountTwo, arrestCountOne, arrestCountTwo, "En medborgare har blivit rånad! ", "En tjuv har blivit arresterad! \t",
                                                                                                "Antal rånade: ", "Antal arresterade: ");
                city.SetInterActionCount('T');
                city.SetInterActionCount('A');

                Thread.Sleep(1000);
            }
        }
        
    }
}
