using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using Newtonsoft.Json;

namespace SPHMMaker.Items
{
    internal static class ItemManager
    {
        static JsonSerializerSettings serializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor };
        public static ItemData GetItem(string aName) => items.First(x => x.Name == aName);
        public static ItemData GetItemById(int id) => items[id]; //TODO: Make sure that items can only be changed in the list and in the Listbox at the same time.
        public static ReadOnlyCollection<ItemData> Items => items.AsReadOnly();
        static List<ItemData> items;
        static ListBox itemListBox;

        public static void CreateItem(ItemData aItem)
        {
            items.Add(aItem);
        }

        public static void SetListBox(ListBox aItemListBox)
        {
            itemListBox = aItemListBox;
            itemListBox.DataSource = items;
        }

        static ItemManager()
        {
            CreateDir();
            CreateList();
            //listBox = Form1.Instance.Controls.Find("items", true).First() as ListBox;
        }

        [MemberNotNull("items")]
        static void CreateList()
        {
            items = new List<ItemData>();
            string[] paths = Directory.GetDirectories("Items");

            if (paths.Length == 0) return;

            foreach (string path in paths)
            {
                string[] subtypes = Directory.GetFiles(path);
                foreach (string subtype in subtypes)
                {
                    ConvertToItemDataAndAddToList(subtype);
                }
            }
        }

        static void ConvertToItemDataAndAddToList(string filePath)
        {
            ItemData? i;
            string rawData = File.ReadAllText(filePath);

            switch (filePath.Split("\\")[1])
            {
                case "Consumable":
                    i = JsonConvert.DeserializeObject<ConsumableData>(rawData, serializerSettings);
                    break;
                case "Equipment":
                    i = JsonConvert.DeserializeObject<EquipmentData>(rawData, serializerSettings);
                    break;
                case "Weapon":
                    i = JsonConvert.DeserializeObject<WeaponData>(rawData, serializerSettings);
                    break;
                case "Container":
                    i = JsonConvert.DeserializeObject<BagData>(rawData, serializerSettings);
                    break;
                case "Trash":
                    i = JsonConvert.DeserializeObject<ItemData>(rawData, serializerSettings);
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (i == null) return;
            items.Add(i);
        }

        static void CreateDir()
        {
            if (Directory.Exists("Items")) return;

            Directory.CreateDirectory("Items");

        }
    }
}
