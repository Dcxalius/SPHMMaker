using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SPHMMaker.Items;
using SPHMMaker.Tiles;

namespace SPHMMaker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;
        int editingTile = -1;


        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
            InitializeTiles();
        }
        private void loadDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? path = GetDirectory();

            if (path is null)
            {
                return;
            }

            //TODO: Check access?
            //DirectoryInfo di = new DirectoryInfo(path);
            //di.GetAccessControl().GetAccessRules();

            string[] foldersThatShouldBeHere = ["Items"];

            string[] folders = Directory.GetDirectories(path);


            foreach (string folder in foldersThatShouldBeHere)
            {
                if (!folders.Contains(path + "\\" + folder))
                {
                    MessageBox.Show($"Error, {folder} is not found");
                    return;
                }
            }

            ItemManager.Load(path + "\\Items");
            TileManager.Load(path + "\\Tiles");
        }

        private void saveDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? s = GetDirectory();

            if (s is null)
            {
                MessageBox.Show("Save aborted.");
                return;
            }

            //ItemManager.Save(s);
        }

        string? GetDirectory()
        {
            var fbg = new FolderBrowserDialog()
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbg.ShowDialog() != DialogResult.OK)
                return null;

            return fbg.SelectedPath;
        }

        private void itemCheckGeneratedTooltip_Click(object sender, EventArgs e)
        {
            ItemData item = FoldDataIntoItem;
            string tooltip = itemNameInput.Text;
            tooltip += "\n";
            tooltip += itemDescriptionInput.Text;
            tooltip += "\n";

            switch (itemTypeSelector.GetSingleCheckedIndexName)
            {
                case "Bag":
                    tooltip += "Bag slots: " + itemBagSizeSetter.Value;
                    break;
                case "Consumable":
                    //TODO: Check what type and do stuff
                    tooltip += "Not finished yet xdd";
                    break;
                case "Equipment":
                    if(itemEquipmentMaterialSetter.Text != EquipmentData.MaterialType.None.ToString())
                    {
                        tooltip += itemEquipmentMaterialSetter.Text;
                        tooltip += "\n";
                    }
                    tooltip += ((EquipmentData)item).StatReport;
                    break;
                case "Weapon":
                    tooltip += ((WeaponData)item).StatReport;
                    tooltip += "\n";
                    tooltip += ((WeaponData)item).GetAttack;
                    break;
                case "None":
                    break;
                default:
                    throw new NotImplementedException();
            }


            MessageBox.Show(tooltip);
        }

        private void fileDownloadInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string instructions = "To download files that are shared in the chat:" + Environment.NewLine +
                Environment.NewLine +
                "1. Hover the message that contains the attachment and select the download icon." + Environment.NewLine +
                "2. Pick a destination on your computer when the save dialog appears." + Environment.NewLine +
                "3. After the download finishes, open the saved file from the chosen folder." + Environment.NewLine +
                "4. If the download is a compressed archive (.zip), extract it before importing it into the game.";

            MessageBox.Show(instructions, "File Download Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
