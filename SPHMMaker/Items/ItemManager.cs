using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SPHMMaker.Items.SubTypes;

namespace SPHMMaker.Items
{
    internal static class ItemManager
    {
        static readonly JsonSerializerSettings serializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        static readonly StringComparer PathComparer = OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

        static List<ItemData> items = new();
        static List<string> itemFileNames = new();
        static ListBox? itemListBox;

        public static ItemData GetItem(string aName) => items.First(x => x.Name == aName);
        public static bool FreeIdCheck(int aId) => items.All(x => x.ID != aId);
        public static ItemData GetItemById(int id) => items[id];
        public static ReadOnlyCollection<ItemData> Items => items.AsReadOnly();

        static ItemManager()
        {
            CreateDir();
        }

        public static void CreateItem(ItemData aItem)
        {
            items.Add(aItem);
            string relativePath = EnsureUniqueRelativeItemPath(GenerateItemFileName(aItem));
            itemFileNames.Add(relativePath);
            RefreshListBox();
        }

        public static void OverrideItem(int index, ItemData aItem)
        {
            if (index < 0 || index >= items.Count)
            {
                return;
            }

            items[index] = aItem;

            string folder = GetFolderNameForItem(aItem);
            string existingPath = itemFileNames.Count > index ? itemFileNames[index] : string.Empty;
            string fileName = Path.GetFileName(existingPath);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = GenerateItemFileName(aItem, includeFolder: false);
            }
            else
            {
                fileName = $"{aItem.ID}_{SanitizeFileName(aItem.Name)}.json";
            }

            string relativePath = Path.Combine(folder, fileName);
            itemFileNames[index] = EnsureUniqueRelativeItemPath(relativePath, index);
            RefreshListBox();
        }

        public static void SetListBox(ListBox aItemListBox)
        {
            itemListBox = aItemListBox;
            RefreshListBox();
        }

        static void RefreshListBox()
        {
            if (itemListBox == null)
            {
                return;
            }

            itemListBox.DataSource = null;
            itemListBox.DataSource = items;
        }

        [MemberNotNull(nameof(items))]
        static void CreateList(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                throw new FileNotFoundException();
            }

            var loadedItems = new List<(ItemData Item, string RelativePath)>();

            foreach (string file in files)
            {
                ItemData item = ConvertToItemData(file);
                string relative = NormalizeRelativePath(Path.GetRelativePath(path, file));
                loadedItems.Add((item, relative));
            }

            loadedItems.Sort((left, right) => left.Item.ID.CompareTo(right.Item.ID));

            items = new List<ItemData>(loadedItems.Count);
            itemFileNames = new List<string>(loadedItems.Count);

            foreach (var entry in loadedItems)
            {
                Debug.Assert(items.All(x => x.ID != entry.Item.ID), "Duplicate ID's found");
                items.Add(entry.Item);
                itemFileNames.Add(entry.RelativePath);
            }
        }

        static ItemData ConvertToItemData(string filePath)
        {
            string rawData = File.ReadAllText(filePath);
            string? parentDirectory = Path.GetFileName(Path.GetDirectoryName(filePath));

            ItemData? item = parentDirectory switch
            {
                "Consumable" => JsonConvert.DeserializeObject<ConsumableData>(rawData, serializerSettings),
                "Potion" => JsonConvert.DeserializeObject<PotionData>(rawData, serializerSettings),
                "Equipment" => JsonConvert.DeserializeObject<EquipmentData>(rawData, serializerSettings),
                "Weapon" => JsonConvert.DeserializeObject<WeaponData>(rawData, serializerSettings),
                "Container" => JsonConvert.DeserializeObject<BagData>(rawData, serializerSettings),
                "Trash" => JsonConvert.DeserializeObject<ItemData>(rawData, serializerSettings),
                _ => throw new WrongDirectoryException(),
            };

            if (item == null)
            {
                throw new FileLoadException();
            }

            return item;
        }

        static void CreateDir()
        {
            if (!Directory.Exists("Items"))
            {
                Directory.CreateDirectory("Items");
            }
        }

        public static bool Save(string destination)
        {
            if (items.Count == 0)
            {
                return false;
            }

            Directory.CreateDirectory(destination);
            var usedPaths = new HashSet<string>(PathComparer);

            for (int i = 0; i < items.Count; i++)
            {
                ItemData item = items[i];
                string relativePath = itemFileNames.Count > i ? itemFileNames[i] : GenerateItemFileName(item);
                relativePath = EnsureUniqueRelativeItemPath(relativePath, i, usedPaths);

                string fullPath = Path.Combine(destination, relativePath);
                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(item, Formatting.Indented, serializerSettings);
                File.WriteAllText(fullPath, json);

                usedPaths.Add(relativePath);
                if (itemFileNames.Count > i)
                {
                    itemFileNames[i] = relativePath;
                }
                else
                {
                    itemFileNames.Add(relativePath);
                }
            }

            return true;
        }

        public static bool Load(string path)
        {
            bool success = true;
            try
            {
                CreateList(path);
            }
            catch (Exception e)
            {
                success = false;
                if (e is DirectoryNotFoundException) MessageBox.Show("No subtypes folders found");
                else if (e is FileNotFoundException) MessageBox.Show("Empty folder found");
                else if (e is WrongDirectoryException) MessageBox.Show("Weird Directory Found");
                else throw;
            }

            RefreshListBox();
            return success;
        }

        static string GenerateItemFileName(ItemData item, bool includeFolder = true)
        {
            string fileName = $"{item.ID}_{SanitizeFileName(item.Name)}.json";
            if (!includeFolder)
            {
                return fileName;
            }

            string folder = GetFolderNameForItem(item);
            return Path.Combine(folder, fileName);
        }

        static string GetFolderNameForItem(ItemData item) => item switch
        {
            WeaponData => "Weapon",
            PotionData => "Potion",
            ConsumableData => "Consumable",
            EquipmentData => "Equipment",
            BagData => "Container",
            _ => "Trash",
        };

        static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "item";
            }

            char[] invalid = Path.GetInvalidFileNameChars();
            var builder = new StringBuilder(name.Length);

            foreach (char c in name)
            {
                builder.Append(invalid.Contains(c) ? '_' : c);
            }

            string sanitized = builder.ToString().Trim();
            return string.IsNullOrEmpty(sanitized) ? "item" : sanitized;
        }

        static string NormalizeRelativePath(string relativePath) => relativePath
            .Replace('\\', Path.DirectorySeparatorChar)
            .Replace('/', Path.DirectorySeparatorChar);

        static string EnsureUniqueRelativeItemPath(string relativePath, int currentIndex = -1, HashSet<string>? usedPaths = null)
        {
            relativePath = NormalizeRelativePath(relativePath);
            string directory = Path.GetDirectoryName(relativePath) ?? string.Empty;
            string baseName = Path.GetFileNameWithoutExtension(relativePath);
            string extension = Path.GetExtension(relativePath);

            HashSet<string> tracker = usedPaths ?? new HashSet<string>(PathComparer);
            if (usedPaths == null)
            {
                for (int i = 0; i < itemFileNames.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        continue;
                    }

                    tracker.Add(NormalizeRelativePath(itemFileNames[i]));
                }
            }

            string candidate = relativePath;
            int counter = 1;
            while (!tracker.Add(candidate))
            {
                string suffix = $"_{counter++}";
                string newFileName = baseName + suffix + extension;
                candidate = string.IsNullOrEmpty(directory) ? newFileName : Path.Combine(directory, newFileName);
            }

            return candidate;
        }
    }

    [Serializable]
    public class WrongDirectoryException : Exception
    {
        public WrongDirectoryException() { }
        public WrongDirectoryException(string message) : base(message) { }
        public WrongDirectoryException(string message, Exception inner) : base(message, inner) { }
    }
}
