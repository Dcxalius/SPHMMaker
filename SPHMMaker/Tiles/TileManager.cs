using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SPHMMaker.Tiles
{
    internal static class TileManager
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            TypeNameHandling = TypeNameHandling.None
        };

        private static List<TileData> tiles = new();
        private static ListBox? tileListBox;
        private static string? sourcePath;

        public static ReadOnlyCollection<TileData> Tiles => tiles.AsReadOnly();
        public static string? SourcePath => sourcePath;

        public static void SetListBox(ListBox listBox)
        {
            tileListBox = listBox;
            RefreshDataSource();
        }

        public static bool Load(string path)
        {
            string? aggregatePath = ResolveAggregatePath(path);
            if (aggregatePath is null || !File.Exists(aggregatePath))
            {
                return false;
            }

            string json = File.ReadAllText(aggregatePath);
            var loadedTiles = JsonConvert.DeserializeObject<List<TileData>>(json, SerializerSettings);
            if (loadedTiles is null)
            {
                return false;
            }

            tiles = loadedTiles.OrderBy(tile => tile.ID).ToList();
            sourcePath = aggregatePath;
            RefreshDataSource();
            return true;
        }

        public static bool Save(string destination)
        {
            if (tiles.Count == 0)
            {
                return false;
            }

            string aggregatePath = ResolveAggregatePath(destination) ?? BuildAggregatePath(destination);
            string? directory = Path.GetDirectoryName(aggregatePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var ordered = tiles.OrderBy(tile => tile.ID).ToList();
            string json = JsonConvert.SerializeObject(ordered, Formatting.Indented, SerializerSettings);
            File.WriteAllText(aggregatePath, json);

            sourcePath = aggregatePath;
            return true;
        }

        public static bool SaveCurrent()
        {
            return sourcePath is not null && Save(sourcePath);
        }

        public static void CreateTile(TileData tile)
        {
            tiles.Add(tile);
            SortTiles();
            RefreshDataSource();
        }

        public static void OverrideTile(int index, TileData tile)
        {
            if (index < 0 || index >= tiles.Count)
            {
                return;
            }

            tiles[index] = tile;
            SortTiles();
            RefreshDataSource();
        }

        public static bool FreeIdCheck(int id) => FreeIdCheck(id, null);

        public static bool FreeIdCheck(int id, int? ignoreIndex)
        {
            return !tiles.Where((tile, index) => ignoreIndex is null || index != ignoreIndex.Value)
                         .Any(tile => tile.ID == id);
        }

        public static int GetNextAvailableId()
        {
            return tiles.Count == 0 ? 0 : tiles.Max(tile => tile.ID) + 1;
        }

        public static void LoadDefaults()
        {
            tiles = new List<TileData>();
            sourcePath = null;
            RefreshDataSource();
        }

        private static void RefreshDataSource()
        {
            if (tileListBox == null)
            {
                return;
            }

            tileListBox.DataSource = null;
            tileListBox.DataSource = tiles.ToList();
        }

        private static void SortTiles()
        {
            tiles = tiles.OrderBy(tile => tile.ID).ToList();
        }

        private static string? ResolveAggregatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            if (File.Exists(path))
            {
                return path;
            }

            if (Directory.Exists(path))
            {
                string candidate = Path.Combine(path, "TileData.json");
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                candidate = Path.Combine(path, "Tiles", "TileData.json");
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                string? parent = Path.GetDirectoryName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                if (!string.IsNullOrEmpty(parent))
                {
                    candidate = Path.Combine(parent, "TileData.json");
                    if (File.Exists(candidate))
                    {
                        return candidate;
                    }
                }
            }

            return null;
        }

        private static string BuildAggregatePath(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentException("Destination cannot be null or whitespace.", nameof(basePath));
            }

            if (Directory.Exists(basePath) || string.IsNullOrEmpty(Path.GetExtension(basePath)))
            {
                return Path.Combine(basePath, "TileData.json");
            }

            return basePath;
        }
    }
}
