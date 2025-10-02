using System.Collections.ObjectModel;
using System.Collections.Generic;
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

        public static ReadOnlyCollection<TileData> Tiles => tiles.AsReadOnly();

        public static void SetListBox(ListBox listBox)
        {
            tileListBox = listBox;
            RefreshDataSource();
        }

        public static void CreateTile(TileData tile)
        {
            tiles.Add(tile);
            tiles = tiles.OrderBy(t => t.ID).ToList();
            RefreshDataSource();
        }

        public static void OverrideTile(int index, TileData tile)
        {
            if (index < 0 || index >= tiles.Count)
            {
                return;
            }

            tiles[index] = tile;
            tiles = tiles.OrderBy(t => t.ID).ToList();
            RefreshDataSource();
        }

        public static bool FreeIdCheck(int id) => tiles.All(t => t.ID != id);

        public static void LoadDefaults()
        {
            if (tiles.Count > 0)
            {
                return;
            }

            tiles = new List<TileData>
            {
                new TileData(0, "Grass", "grass", true, 1, "Standard grassy terrain."),
                new TileData(1, "Dirt", "dirt", true, 1, "Lightly trodden path."),
                new TileData(2, "Stone", "stone", true, 1, "Worked stone floor."),
                new TileData(3, "Water", "water", false, 0, "Cannot be traversed without special abilities."),
                new TileData(4, "Lava", "lava", false, 0, "Deadly lava tile."),
                new TileData(5, "Snow", "snow", true, 2, "Slows movement slightly."),
            };
            RefreshDataSource();
        }

        public static bool Load(string path)
        {
            if (!Directory.Exists(path))
            {
                LoadDefaults();
                return false;
            }

            var files = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                LoadDefaults();
                return false;
            }

            var loadedTiles = new List<TileData>();
            foreach (var file in files)
            {
                var rawData = File.ReadAllText(file);
                var tile = JsonConvert.DeserializeObject<TileData>(rawData, SerializerSettings);
                if (tile != null)
                {
                    loadedTiles.Add(tile);
                }
            }

            if (loadedTiles.Count == 0)
            {
                LoadDefaults();
                return false;
            }

            tiles = loadedTiles.OrderBy(t => t.ID).ToList();
            RefreshDataSource();
            return true;
        }

        private static void RefreshDataSource()
        {
            if (tileListBox == null)
            {
                return;
            }

            tileListBox.DataSource = null;
            tileListBox.DataSource = tiles;
        }
    }
}
