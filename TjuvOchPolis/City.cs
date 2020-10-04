using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TjuvOchPolis
{
    class City
    {
        public string[,] TheCity { get; private set; }
        public City(int column, int row)
        {
            TheCity = new string[column, row];
        }

        public void DrawCity(PersonList personList)
        {
            for (int column = 0; column < TheCity.GetUpperBound(0); column++)
                for (int row = 0; column < TheCity.GetUpperBound(1); row++)
                    foreach (Person item in personList.ThePersonList)
                    {
                        if (item.Position[0] == column && item.Position[1] == row)
                        {
                            switch (item)
                            {
                                case Citizen citizen:
                                    Console.Write('M');
                                    break;
                                case Police police:
                                    Console.Write('P');
                                    break;
                                case Thief thief:
                                    Console.Write('T');
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        else
                            Console.Write(" ");
                    }
        }
    }
}
