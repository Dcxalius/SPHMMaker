using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPHMMaker.Tiles;

namespace SPHMMaker
{
    public partial class MainForm
    {
        private int editingTile = -1;

        private void InitializeTiles()
        {
            TileManager.SetListBox(tileList);
            LoadTilesFromPreferredLocation();

            tileList.SelectedIndexChanged += tileList_SelectedIndexChanged;

            if (tileList.Items.Count > 0)
            {
                tileList.SelectedIndex = 0;
            }
            else
            {
                ResetTileEditor();
            }
        }

        private void LoadTilesFromPreferredLocation()
        {
            string? aggregatePath = ResolvePreferredTileDataPath();
            bool loaded = aggregatePath is not null && TileManager.Load(aggregatePath);

            if (!loaded)
            {
                TileManager.LoadDefaults();
            }
        }

        private static string? ResolveAggregateFromBase(string? basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                return null;
            }

            string candidate = Path.Combine(basePath, "TileData.json");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            candidate = Path.Combine(basePath, "Tiles", "TileData.json");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return null;
        }

        private string? ResolvePreferredTileDataPath()
        {
            string? aggregate = ResolveAggregateFromBase(datapackRootPath);
            if (!string.IsNullOrEmpty(aggregate))
            {
                return aggregate;
            }

#if DEBUG
            string? debugOverride = Environment.GetEnvironmentVariable(DebugDatapackDirectoryEnvironmentVariable);
            aggregate = ResolveAggregateFromBase(debugOverride);
            if (!string.IsNullOrEmpty(aggregate))
            {
                return aggregate;
            }

            aggregate = ResolveAggregateFromBase(DefaultDatapackPath);
            if (!string.IsNullOrEmpty(aggregate))
            {
                return aggregate;
            }
#endif

            aggregate = ResolveAggregateFromBase(AppDomain.CurrentDomain.BaseDirectory);
            return aggregate;
        }

        private string GetTileDataSavePath()
        {
            if (!string.IsNullOrWhiteSpace(TileManager.SourcePath))
            {
                return TileManager.SourcePath!;
            }

            string? basePath = datapackRootPath;

#if DEBUG
            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = Environment.GetEnvironmentVariable(DebugDatapackDirectoryEnvironmentVariable);
            }

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = DefaultDatapackPath;
            }
#endif

            if (string.IsNullOrWhiteSpace(basePath))
            {
                string fallbackDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tiles");
                Directory.CreateDirectory(fallbackDirectory);
                return Path.Combine(fallbackDirectory, "TileData.json");
            }

            Directory.CreateDirectory(basePath);
            return Path.Combine(basePath, "TileData.json");
        }

        private void PersistTilesToDisk()
        {
            string destination = GetTileDataSavePath();

            if (!TileManager.Save(destination))
            {
                MessageBox.Show(
                    "Tile data was updated, but it could not be saved to disk. Verify that the destination path is writable.",
                    "Tile Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private TileData? FoldDataIntoTile
        {
            get
            {
                int id = (int)tileIdInput.Value;
                string name = tileNameInput.Text;
                string texture = tileTextureInput.Text;
                bool isWalkable = tileWalkableCheckbox.Checked;
                decimal movementCost = tileMovementCostInput.Value;
                bool transparent = tileTransparentCheckbox.Checked;

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Tile name cannot be empty.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(texture))
                {
                    MessageBox.Show("Texture name cannot be empty.");
                    return null;
                }

                int? ignoreIndex = editingTile >= 0 ? editingTile : null;
                if (!TileManager.FreeIdCheck(id, ignoreIndex))
                {
                    MessageBox.Show("Tile ID must be unique.");
                    return null;
                }

                if (movementCost <= 0)
                {
                    MessageBox.Show("Movement cost must be greater or equal to zero.");
                    return null;
                }

                return new TileData(id, name.Trim(), texture.Trim(), isWalkable, movementCost, transparent);
            }
        }

        private void ResetTileEditor()
        {
            editingTile = -1;
            int nextId = TileManager.GetNextAvailableId();
            nextId = Math.Clamp(nextId, (int)tileIdInput.Minimum, (int)tileIdInput.Maximum);

            tileIdInput.Value = Math.Clamp(nextId, (int)tileIdInput.Minimum, (int)tileIdInput.Maximum);
            tileNameInput.Text = string.Empty;
            tileTextureInput.Text = string.Empty;
            tileWalkableCheckbox.Checked = true;
            tileTransparentCheckbox.Checked = true;
            tileMovementCostInput.Value = Math.Clamp(1.00m, tileMovementCostInput.Minimum, tileMovementCostInput.Maximum);
            tileList.ClearSelected();
        }

        private void tileList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tileList.SelectedItem is not TileData tile)
            {
                return;
            }

            editingTile = tileList.SelectedIndex;
            int clampedId = Math.Clamp(tile.ID, (int)tileIdInput.Minimum, (int)tileIdInput.Maximum);
            tileIdInput.Value = clampedId;
            tileNameInput.Text = tile.Name;
            tileTextureInput.Text = tile.Texture;
            tileWalkableCheckbox.Checked = tile.IsWalkable;
            tileTransparentCheckbox.Checked = tile.Transparent;
            decimal movementValue = Math.Clamp(tile.MovementCost, tileMovementCostInput.Minimum, tileMovementCostInput.Maximum);
            tileMovementCostInput.Value = movementValue;
        }

        private void tileCreateButton_Click(object sender, EventArgs e)
        {
            TileData? tile = FoldDataIntoTile;
            if (tile is null)
            {
                return;
            }

            TileManager.CreateTile(tile);
            tileList.SelectedItem = TileManager.Tiles.FirstOrDefault(t => t.ID == tile.ID);
            PersistTilesToDisk();
            ResetTileEditor();
        }

        private void tileSaveButton_Click(object sender, EventArgs e)
        {
            if (tileList.SelectedIndex < 0)
            {
                MessageBox.Show("Select a tile to update.");
                return;
            }

            editingTile = tileList.SelectedIndex;
            TileData? tile = FoldDataIntoTile;
            if (tile is null)
            {
                return;
            }

            TileManager.OverrideTile(tileList.SelectedIndex, tile);
            tileList.SelectedItem = TileManager.Tiles.FirstOrDefault(t => t.ID == tile.ID);
            PersistTilesToDisk();
        }

        private void tileResetButton_Click(object sender, EventArgs e) => ResetTileEditor();
    }
}
