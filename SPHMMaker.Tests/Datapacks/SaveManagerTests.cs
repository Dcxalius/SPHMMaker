using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPHMMaker.Datapacks;
using SPHMMaker.Items;
using SPHMMaker.Tiles;

namespace SPHMMaker.Tests.Datapacks;

[TestClass]
public class SaveManagerTests
{
    [TestInitialize]
    public void Initialize() => ResetManagers();

    [TestCleanup]
    public void Cleanup() => ResetManagers();

    [TestMethod]
    public void SaveToDirectory_WritesItemAndTileSubfolders()
    {
        SeedSampleData();

        string exportRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(exportRoot);

        try
        {
            SaveManager.SaveToDirectory(exportRoot);

            string itemsDirectory = Path.Combine(exportRoot, "Items");
            string tilesDirectory = Path.Combine(exportRoot, "Tiles");

            Assert.IsTrue(Directory.Exists(itemsDirectory), "Items directory should be created.");
            Assert.IsTrue(Directory.Exists(tilesDirectory), "Tiles directory should be created.");

            int itemFileCount = Directory.GetFiles(itemsDirectory, "*.json", SearchOption.AllDirectories).Length;
            int tileFileCount = Directory.GetFiles(tilesDirectory, "*.json", SearchOption.AllDirectories).Length;

            Assert.IsTrue(itemFileCount > 0, "Item export should produce at least one JSON file.");
            Assert.IsTrue(tileFileCount > 0, "Tile export should produce at least one JSON file.");
        }
        finally
        {
            if (Directory.Exists(exportRoot))
            {
                Directory.Delete(exportRoot, recursive: true);
            }
        }
    }

    [TestMethod]
    public void SaveToArchive_CreatesZipWithExportedData()
    {
        SeedSampleData();

        string tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        string archivePath = Path.Combine(tempRoot, "datapack.zip");

        try
        {
            SaveManager.SaveToArchive(archivePath);

            Assert.IsTrue(File.Exists(archivePath), "Archive should be created.");

            using ZipArchive archive = ZipFile.OpenRead(archivePath);
            var entryNames = archive.Entries.Select(entry => entry.FullName).ToList();

            Assert.IsTrue(entryNames.Any(name => name.StartsWith("Items/", StringComparison.OrdinalIgnoreCase)), "Archive should contain item data.");
            Assert.IsTrue(entryNames.Any(name => name.StartsWith("Tiles/", StringComparison.OrdinalIgnoreCase)), "Archive should contain tile data.");
        }
        finally
        {
            if (Directory.Exists(tempRoot))
            {
                Directory.Delete(tempRoot, recursive: true);
            }
        }
    }

    [TestMethod]
    public void Save_WhenNoData_ThrowsInvalidOperationException()
    {
        string exportRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(exportRoot);

        try
        {
            Assert.ThrowsException<InvalidOperationException>(() => SaveManager.SaveToDirectory(exportRoot));
        }
        finally
        {
            if (Directory.Exists(exportRoot))
            {
                Directory.Delete(exportRoot, recursive: true);
            }
        }

        string archivePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "datapack.zip");
        Assert.ThrowsException<InvalidOperationException>(() => SaveManager.SaveToArchive(archivePath));
    }

    static void SeedSampleData()
    {
        ResetManagers();

        var items = new List<ItemData>
        {
            new ItemData(1, "sword_icon", "Test Sword", "A reliable testing blade.", 1, ItemData.ItemQuality.Common, 100),
        };

        var tiles = new List<TileData>
        {
            new TileData(1, "Test Tile", "test_tile", true, 1, "Tile used for testing."),
        };

        SetField(typeof(ItemManager), "items", items);
        SetField(typeof(ItemManager), "itemFileNames", new List<string>());
        SetField(typeof(TileManager), "tiles", tiles);
        SetField(typeof(TileManager), "tileFileNames", new List<string>());
    }

    static void ResetManagers()
    {
        SetField(typeof(ItemManager), "items", new List<ItemData>());
        SetField(typeof(ItemManager), "itemFileNames", new List<string>());
        SetField(typeof(TileManager), "tiles", new List<TileData>());
        SetField(typeof(TileManager), "tileFileNames", new List<string>());
    }

    static void SetField(Type type, string fieldName, object value)
    {
        FieldInfo? field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
        Assert.IsNotNull(field, $"Field '{fieldName}' was not found on type '{type.FullName}'.");
        field!.SetValue(null, value);
    }
}
