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
using System.Diagnostics.Eventing.Reader;

namespace SPHMMaker.Items
{
    internal static class ItemManager
    {
        static JsonSerializerSettings serializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor };
        public static ItemData GetItem(string aName) => items.First(x => x.Name == aName);
        public static bool FreeIdCheck(int aId) => items.Where(x => x.ID == aId).Count() == 0;
        public static ItemData GetItemById(int id) => items[id]; //TODO: Make sure that items can only be changed in the list and in the Listbox at the same time.
        //TODO: Also make up ur mind, is id always going to be index?
        public static ReadOnlyCollection<ItemData> Items => items.AsReadOnly();
        static List<ItemData> items;
        static ListBox itemListBox;

        public static void CreateItem(ItemData aItem)
        {
            items.Add(aItem);
            itemListBox.DataSource = null;
            itemListBox.DataSource = items;
        }

        public static void OverrideItem(int aIdToOverride, ItemData aItem)
        {
            items[aIdToOverride] = aItem;
            //itemListBox.DataSource = null;
            //itemListBox.DataSource = items;
        }

        public static void SetListBox(ListBox aItemListBox)
        {
            itemListBox = aItemListBox;
            itemListBox.DataSource = items;
        }

        static ItemManager()
        {
            CreateDir();
            //CreateList();
            //listBox = Form1.Instance.Controls.Find("items", true).First() as ListBox;
        }

        [MemberNotNull("items")]
        static void CreateList(string aPath)
        {
            items = new List<ItemData>();
            string[] subTypes = Directory.GetDirectories(aPath);
            //Directory.GetDirectoryRoot(aPath);

            if (subTypes.Length == 0) throw new DirectoryNotFoundException();

            foreach (string path in subTypes)
            {
                string[] items = Directory.GetFiles(path);
                if (items.Length == 0) throw new FileNotFoundException();

                foreach (string item in items)
                {
                    ConvertToItemDataAndAddToList(item);
                }
            }

            items.Sort((x, y) => x.ID - y.ID);
        }

        static void ConvertToItemDataAndAddToList(string filePath)
        {
            ItemData? i;
            string rawData = File.ReadAllText(filePath);
            string[] split = filePath.Split("\\");
            switch (split[split.Length - 2])
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
                    throw new WrongDirectoryException();
            }
            if (i == null) throw new FileLoadException();
            items.Add(i);
        }

        static void CreateDir()
        {
            if (Directory.Exists("Items")) return;

            Directory.CreateDirectory("Items");

        }

        public static bool Save(string aFilePath)
        {
            return false;
        }

        public static bool Load(string aFilePath)
        {
            try
            {
                CreateList(aFilePath);
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException) MessageBox.Show("No subtypes folders found");
                else if (e is FileNotFoundException) MessageBox.Show("Empty folder found");
                else if (e is WrongDirectoryException) MessageBox.Show("Weird Directory Found");
                else throw;
            }

            itemListBox.DataSource = items;
            return true;
        }
    }


    [Serializable]
    public class WrongDirectoryException : Exception
    {
        public WrongDirectoryException() { }
        public WrongDirectoryException(string message) : base(message) { }
        public WrongDirectoryException(string message, Exception inner) : base(message, inner) { }
        protected WrongDirectoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
