using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SPHMMaker.Tiles;

namespace SPHMMaker.Tests.Tiles;

[TestClass]
public class TileManagerTests
{
    [TestMethod]
    public void Load_FromAggregateFile_PopulatesTilesAndGeneratesFileNames()
    {
        string tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempRoot);
        try
        {
            string aggregatePath = Path.Combine(tempRoot, "TileData.json");
            var expectedTiles = new List<TileData>
            {
                new TileData(5, "Crystal Cavern", "crystal_cavern", true, 1, "Glittering crystals line the walls."),
                new TileData(2, "Molten Core", "molten_core", false, 0, "Scorching lava flows freely."),
                new TileData(9, "Ancient Library", "ancient_library", true, 1, "Stacks of dusty tomes."),
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
                Assert.AreEqual(orderedExpected[i].Notes, actualTiles[i].Notes, $"Tile {i} notes did not match.");
            }

            string saveDestination = Path.Combine(tempRoot, "output");
            Directory.CreateDirectory(saveDestination);
            bool saveResult = TileManager.Save(saveDestination);
            Assert.IsTrue(saveResult, "TileManager.Save should succeed after loading aggregate data.");

            var savedFiles = Directory.GetFiles(saveDestination, "*.json", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .OrderBy(name => name)
                .ToList();

            var expectedFileNames = orderedExpected
                .Select(tile => GenerateExpectedFileName(tile))
                .OrderBy(name => name)
                .ToList();

            CollectionAssert.AreEqual(expectedFileNames, savedFiles, "TileManager should generate stable file names for individual tiles.");
        }
        finally
        {
            if (Directory.Exists(tempRoot))
            {
                Directory.Delete(tempRoot, recursive: true);
            }
        }
    }

    private static string GenerateExpectedFileName(TileData tile)
    {
        return $"{tile.ID}_{SanitizeFileName(tile.Name)}.json";
    }

    private static string SanitizeFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "tile";
        }

        char[] invalidCharacters = Path.GetInvalidFileNameChars();
        var builder = new StringBuilder(name.Length);
        foreach (char character in name)
        {
            builder.Append(invalidCharacters.Contains(character) ? '_' : character);
        }

        string sanitized = builder.ToString().Trim();
        return string.IsNullOrEmpty(sanitized) ? "tile" : sanitized;
    }
}
