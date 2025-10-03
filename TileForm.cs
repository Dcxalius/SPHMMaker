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
        private TileData? FoldDataIntoTile
        {
            get
            {
                int id = (int)tileIdInput.Value;
                string name = tileNameInput.Text;
                string texture = tileTextureInput.Text;
                bool isWalkable = tileWalkableCheckbox.Checked;
                int movementCost = (int)tileMovementCostInput.Value;
                string notes = tileNotesInput.Text;

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

                if (!TileManager.FreeIdCheck(id) && (editingTile < 0 || TileManager.Tiles[editingTile].ID != id))
                {
                    MessageBox.Show("Tile ID must be unique.");
                    return null;
                }

                return new TileData(id, name, texture, isWalkable, movementCost, notes);
            }
        }

        private void InitializeTiles()
        {
            TileManager.SetListBox(tileList);
            if (!TileManager.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tiles")))
            {
                TileManager.LoadDefaults();
            }

            tileList.SelectedIndexChanged += tileList_SelectedIndexChanged;
            ResetTileEditor();
        }

        private int GetNextTileId()
        {
            int highest = -1;
            foreach (TileData tile in TileManager.Tiles)
            {
                if (tile.ID > highest)
                {
                    highest = tile.ID;
                }
            }

            return highest + 1;
        }

        private void ResetTileEditor()
        {
            editingTile = -1;
            int nextId = GetNextTileId();
            if (nextId < tileIdInput.Minimum)
            {
                nextId = (int)tileIdInput.Minimum;
            }
            if (nextId > tileIdInput.Maximum)
            {
                nextId = (int)tileIdInput.Maximum;
            }

            tileIdInput.Value = nextId;
            tileNameInput.Text = string.Empty;
            tileTextureInput.Text = string.Empty;
            tileWalkableCheckbox.Checked = true;
            tileMovementCostInput.Value = Math.Max(tileMovementCostInput.Minimum, 1m);
            tileNotesInput.Text = string.Empty;
            tileList.ClearSelected();
        }

        private void tileList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tileList.SelectedItem is not TileData tile)
            {
                return;
            }

            editingTile = tileList.SelectedIndex;
            tileIdInput.Value = Math.Min(Math.Max(tile.ID, (int)tileIdInput.Minimum), (int)tileIdInput.Maximum);
            tileNameInput.Text = tile.Name;
            tileTextureInput.Text = tile.Texture;
            tileWalkableCheckbox.Checked = tile.IsWalkable;
            int movementValue = Math.Min(Math.Max(tile.MovementCost, (int)tileMovementCostInput.Minimum), (int)tileMovementCostInput.Maximum);
            tileMovementCostInput.Value = movementValue;
            tileNotesInput.Text = tile.Notes;
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
        }

        private void tileResetButton_Click(object sender, EventArgs e) => ResetTileEditor();
    }
}
