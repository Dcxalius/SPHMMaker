using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace SPHMMaker.Items
{
    internal static class ItemManager
    {
        public static ReadOnlyCollection<Item> Items => items.AsReadOnly();
        static List<Item> items;


        static ItemManager()
        {
            CreateDir();
            CreateList();
        }

        [MemberNotNull("items")]
        static void CreateList()
        {
            items = new List<Item>();
            string[] paths = Directory.GetDirectories("Items");

            if (paths.Length == 0) return;

            foreach (string path in paths)
            {
                string[] subtypes = Directory.GetFiles(path);
                foreach (string subtype in subtypes)
                {
                    Item? i = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(subtype);
                    if (i == null) continue;
                    items.Add(i);
                }
            }
        }

        static void CreateDir()
        {
            if (Directory.Exists("Items")) return;

            Directory.CreateDirectory("Items");

        }
    }
}
