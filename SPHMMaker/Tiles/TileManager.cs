using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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

        private static readonly StringComparer PathComparer = OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

        private static List<TileData> tiles = new();
        private static List<string> tileFileNames = new();
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
            tileFileNames.Add(EnsureUniqueTileFileName(GenerateTileFileName(tile)));
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
            string fileName = tileFileNames.Count > index ? tileFileNames[index] : GenerateTileFileName(tile);
            fileName = EnsureUniqueTileFileName(fileName, index);
            tileFileNames[index] = fileName;
            SortTiles();
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

            tileFileNames = new List<string>(tiles.Count);
            var usedNames = new HashSet<string>(PathComparer);
            foreach (var tile in tiles)
            {
                string fileName = EnsureUniqueTileFileName(GenerateTileFileName(tile), usedNames: usedNames);
                tileFileNames.Add(fileName);
            }
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

            var loadedTiles = new List<(TileData Tile, string FileName)>();
            foreach (var file in files)
            {
                var rawData = File.ReadAllText(file);
                var tile = JsonConvert.DeserializeObject<TileData>(rawData, SerializerSettings);
                if (tile != null)
                {
                    string relative = Path.GetFileName(file);
                    loadedTiles.Add((tile, NormalizeFileName(relative)));
                }
            }

            if (loadedTiles.Count == 0)
            {
                LoadDefaults();
                return false;
            }

            var ordered = loadedTiles.OrderBy(entry => entry.Tile.ID).ToList();
            tiles = ordered.Select(entry => entry.Tile).ToList();
            tileFileNames = ordered.Select(entry => entry.FileName).ToList();
            RefreshDataSource();
            return true;
        }

        public static bool Save(string destination)
        {
            if (tiles.Count == 0)
            {
                return false;
            }

            Directory.CreateDirectory(destination);
            var usedNames = new HashSet<string>(PathComparer);
            var updatedFileNames = new List<string>(tiles.Count);

            for (int i = 0; i < tiles.Count; i++)
            {
                TileData tile = tiles[i];
                string fileName = tileFileNames.Count > i ? tileFileNames[i] : GenerateTileFileName(tile);
                fileName = EnsureUniqueTileFileName(fileName, i, usedNames);

                string fullPath = Path.Combine(destination, fileName);
                string json = JsonConvert.SerializeObject(tile, Formatting.Indented, SerializerSettings);
                File.WriteAllText(fullPath, json);

                usedNames.Add(fileName);
                updatedFileNames.Add(fileName);
            }

            tileFileNames = updatedFileNames;
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

        private static void SortTiles()
        {
            var ordered = tiles
                .Select((tile, index) => new { Tile = tile, FileName = tileFileNames.Count > index ? tileFileNames[index] : GenerateTileFileName(tile) })
                .OrderBy(entry => entry.Tile.ID)
                .ToList();

            tiles = ordered.Select(entry => entry.Tile).ToList();
            tileFileNames = ordered.Select(entry => entry.FileName).ToList();
        }

        private static string GenerateTileFileName(TileData tile)
        {
            return $"{tile.ID}_{SanitizeFileName(tile.Name)}.json";
        }

        private static string EnsureUniqueTileFileName(string fileName, int currentIndex = -1, HashSet<string>? usedNames = null)
        {
            fileName = NormalizeFileName(fileName);
            string baseName = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            HashSet<string> tracker = usedNames ?? new HashSet<string>(PathComparer);
            if (usedNames == null)
            {
                for (int i = 0; i < tileFileNames.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        continue;
                    }

                    tracker.Add(NormalizeFileName(tileFileNames[i]));
                }
            }

            string candidate = fileName;
            int counter = 1;
            while (!tracker.Add(candidate))
            {
                string suffix = $"_{counter++}";
                candidate = baseName + suffix + extension;
            }

            return candidate;
        }

        private static string NormalizeFileName(string name) => name.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "tile";
            }

            char[] invalid = Path.GetInvalidFileNameChars();
            var builder = new StringBuilder(name.Length);
            foreach (char c in name)
            {
                builder.Append(invalid.Contains(c) ? '_' : c);
            }

            string sanitized = builder.ToString().Trim();
            return string.IsNullOrEmpty(sanitized) ? "tile" : sanitized;
        }
    }
}
