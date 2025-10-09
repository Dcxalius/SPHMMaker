using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SPHMMaker.Tiles;

namespace SPHMMaker.Tests.Tiles;

[TestClass]
public class TileManagerTests
{
    [TestMethod]
    public void Load_FromAggregateFile_PopulatesTilesAndSavesAggregate()
    {
        string tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempRoot);
        try
        {
            string aggregatePath = Path.Combine(tempRoot, "TileData.json");
            var expectedTiles = new List<TileData>
            {
                new TileData(5, "Crystal Cavern", "crystal_cavern", true, 1.10m, false),
                new TileData(2, "Molten Core", "molten_core", false, 0.20m, true),
                new TileData(9, "Ancient Library", "ancient_library", true, 0.95m, true),
            };

            string aggregateJson = JsonConvert.SerializeObject(expectedTiles, Formatting.Indented);
            File.WriteAllText(aggregatePath, aggregateJson);

            bool loadResult = TileManager.Load(aggregatePath);
            Assert.IsTrue(loadResult, "Loading an aggregate TileData.json file should succeed.");

            var orderedExpected = expectedTiles.OrderBy(tile => tile.ID).ToList();
            var actualTiles = TileManager.Tiles.ToList();

            Assert.AreEqual(orderedExpected.Count, actualTiles.Count, "TileManager should load every entry from the aggregate file.");
            for (int i = 0; i < orderedExpected.Count; i++)
            {
                Assert.AreEqual(orderedExpected[i].ID, actualTiles[i].ID, $"Tile {i} ID did not match.");
                Assert.AreEqual(orderedExpected[i].Name, actualTiles[i].Name, $"Tile {i} Name did not match.");
                Assert.AreEqual(orderedExpected[i].Texture, actualTiles[i].Texture, $"Tile {i} Texture did not match.");
                Assert.AreEqual(orderedExpected[i].IsWalkable, actualTiles[i].IsWalkable, $"Tile {i} walkable flag did not match.");
                Assert.AreEqual(orderedExpected[i].MovementCost, actualTiles[i].MovementCost, $"Tile {i} movement cost did not match.");
                Assert.AreEqual(orderedExpected[i].Transparent, actualTiles[i].Transparent, $"Tile {i} transparency did not match.");
            }

            string saveDestination = Path.Combine(tempRoot, "output");
            Directory.CreateDirectory(saveDestination);
            bool saveResult = TileManager.Save(saveDestination);
            Assert.IsTrue(saveResult, "TileManager.Save should succeed after loading aggregate data.");

            string savedAggregatePath = Path.Combine(saveDestination, "TileData.json");
            Assert.IsTrue(File.Exists(savedAggregatePath), "Saving tile data should emit a TileData.json aggregate file.");

            string savedJson = File.ReadAllText(savedAggregatePath);
            var savedTiles = JsonConvert.DeserializeObject<List<TileData>>(savedJson);
            Assert.IsNotNull(savedTiles, "Saved aggregate file should deserialize back into tile data.");

            var orderedSaved = savedTiles!.OrderBy(tile => tile.ID).ToList();
            Assert.AreEqual(orderedExpected.Count, orderedSaved.Count, "Aggregate save should preserve tile count.");
            for (int i = 0; i < orderedExpected.Count; i++)
            {
                Assert.AreEqual(orderedExpected[i].ID, orderedSaved[i].ID, $"Aggregate tile {i} ID mismatch.");
                Assert.AreEqual(orderedExpected[i].Name, orderedSaved[i].Name, $"Aggregate tile {i} Name mismatch.");
                Assert.AreEqual(orderedExpected[i].Texture, orderedSaved[i].Texture, $"Aggregate tile {i} Texture mismatch.");
                Assert.AreEqual(orderedExpected[i].IsWalkable, orderedSaved[i].IsWalkable, $"Aggregate tile {i} walkable flag mismatch.");
                Assert.AreEqual(orderedExpected[i].MovementCost, orderedSaved[i].MovementCost, $"Aggregate tile {i} movement cost mismatch.");
                Assert.AreEqual(orderedExpected[i].Transparent, orderedSaved[i].Transparent, $"Aggregate tile {i} transparency mismatch.");
            }
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
    public void Load_FromTilesDirectoryFallsBackToParentAggregate()
    {
        string tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempRoot);
        try
        {
            string aggregatePath = Path.Combine(tempRoot, "TileData.json");
            var expectedTiles = new List<TileData>
            {
                new TileData(1, "Test", "texture", true, 1.0m, true)
            };

            string aggregateJson = JsonConvert.SerializeObject(expectedTiles, Formatting.Indented);
            File.WriteAllText(aggregatePath, aggregateJson);

            string tilesFolder = Path.Combine(tempRoot, "Tiles");
            Directory.CreateDirectory(tilesFolder);

            bool loadResult = TileManager.Load(tilesFolder);
            Assert.IsTrue(loadResult, "TileManager should locate TileData.json in the parent directory when pointed at the Tiles folder.");
            Assert.AreEqual(1, TileManager.Tiles.Count, "Tiles should load from parent aggregate file.");
        }
        finally
        {
            if (Directory.Exists(tempRoot))
            {
                Directory.Delete(tempRoot, recursive: true);
            }
        }
    }
}
