using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TjuvOchPolis
{
    class Item
    {
        static string[] AllItems { get; set; } = { "Nycklar", "Mobiltelefon", "Pengar", "Klocka" };
        public string TheItem { get; private set; }

        public Item(int index)
        {
            TheItem = AllItems[index];
        }

        public override string ToString()
        {
            return TheItem;
        }
    }
}
