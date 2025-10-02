using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SPHMMaker.Items;
using SPHMMaker.Loot;

namespace SPHMMaker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;
        private readonly BindingSource lootTableBinding = new();
        private readonly BindingSource lootEntryBinding = new();
        private LootTable? activeLootTable;


        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
            InitializeLootTab();
        }

        private void InitializeLootTab()
        {
            lootAddTableButton.Click += lootAddTableButton_Click;
            lootSaveTableButton.Click += lootSaveTableButton_Click;
            lootDeleteTableButton.Click += lootDeleteTableButton_Click;
            lootAddEntryButton.Click += lootAddEntryButton_Click;
            lootRemoveEntryButton.Click += lootRemoveEntryButton_Click;

            lootTablesListBox.DisplayMember = nameof(LootTable.Id);
            lootTableBinding.DataSource = LootManager.LootTables;
            lootTablesListBox.DataSource = lootTableBinding;
            lootTablesListBox.SelectedIndexChanged += LootTablesListBox_SelectedIndexChanged;

            lootEntriesGrid.AutoGenerateColumns = false;
            lootEntriesGrid.Columns.Clear();
            lootEntriesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LootEntry.ItemId),
                HeaderText = "Item ID",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 120,
                Name = "lootItemIdColumn"
            });
            lootEntriesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(LootEntry.DropRatePercent),
                HeaderText = "Drop Rate (%)",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" },
                Name = "lootDropRateColumn"
            });
            lootEntriesGrid.SelectionChanged += LootEntriesGrid_SelectionChanged;
            lootEntriesGrid.CellValueChanged += LootEntriesGrid_CellValueChanged;
            lootEntriesGrid.CurrentCellDirtyStateChanged += LootEntriesGrid_CurrentCellDirtyStateChanged;
            lootEntriesGrid.DataError += LootEntriesGrid_DataError;

            lootEntryBinding.DataSource = typeof(LootEntry);
            lootEntriesGrid.DataSource = lootEntryBinding;

            lootKillsCounter.ValueChanged += LootKillsCounter_ValueChanged;

            lootTableIdTextBox.Enabled = false;
            lootSaveTableButton.Enabled = false;
            lootDeleteTableButton.Enabled = false;
            lootEntriesGrid.Enabled = false;
            lootAddEntryButton.Enabled = false;
            lootRemoveEntryButton.Enabled = false;
            lootKillsCounter.Enabled = false;

            if (lootDistributionChart.ChartAreas.Count > 0)
            {
                ChartArea area = lootDistributionChart.ChartAreas[0];
                area.AxisX.Title = "Number of Drops";
                area.AxisY.Title = "Probability (%)";
                area.AxisY.Minimum = 0;
                area.AxisY.Maximum = 100;
                area.AxisY.LabelStyle.Format = "0.##";
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            }

            lootDistributionChart.Series.Clear();
            lootChartHintLabel.Visible = true;

            UpdateLootTableDetails();
        }

        private void LootTablesListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateLootTableDetails();
        }

        private void LootEntriesGrid_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (lootEntriesGrid.IsCurrentCellDirty)
            {
                lootEntriesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LootEntriesGrid_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is not null)
            {
                MessageBox.Show("Please enter numeric values for drop rates.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            e.ThrowException = false;
        }

        private void LootEntriesGrid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UpdateLootChart();
            }
        }

        private void LootEntriesGrid_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateRemoveEntryButton();
            UpdateLootChart();
        }

        private void LootKillsCounter_ValueChanged(object? sender, EventArgs e)
        {
            UpdateLootChart();
        }

        private void lootAddTableButton_Click(object? sender, EventArgs e)
        {
            string id = lootTableIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                id = CreateDefaultLootTableId();
            }
            else if (LootManager.ContainsId(id))
            {
                MessageBox.Show("A loot table with that ID already exists.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LootTable table = LootManager.Create(id);
            lootTableBinding.ResetBindings(false);
            lootTablesListBox.SelectedItem = table;
            lootTableIdTextBox.Focus();
            lootTableIdTextBox.SelectAll();
        }

        private void lootSaveTableButton_Click(object? sender, EventArgs e)
        {
            if (activeLootTable is null)
            {
                return;
            }

            string id = lootTableIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Loot table ID cannot be empty.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lootTableIdTextBox.Focus();
                return;
            }

            if (!string.Equals(activeLootTable.Id, id, StringComparison.OrdinalIgnoreCase) && LootManager.ContainsId(id))
            {
                MessageBox.Show("A loot table with that ID already exists.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            activeLootTable.Id = id;
            lootTableBinding.ResetBindings(false);
        }

        private void lootDeleteTableButton_Click(object? sender, EventArgs e)
        {
            if (activeLootTable is null)
            {
                return;
            }

            DialogResult result = MessageBox.Show($"Delete loot table '{activeLootTable.Id}'?", "Delete Loot Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            DetachFromActiveLootTable();
            LootManager.Remove(activeLootTable);
            activeLootTable = null;
            lootTableBinding.ResetBindings(false);
            UpdateLootTableDetails();
        }

        private void lootAddEntryButton_Click(object? sender, EventArgs e)
        {
            if (activeLootTable is null)
            {
                return;
            }

            LootEntry entry = new()
            {
                ItemId = "item_id",
                DropRatePercent = 10d
            };

            activeLootTable.Entries.Add(entry);
            lootEntryBinding.ResetBindings(false);

            int index = activeLootTable.Entries.Count - 1;
            if (index >= 0)
            {
                lootEntriesGrid.ClearSelection();
                lootEntriesGrid.Rows[index].Selected = true;
                lootEntriesGrid.CurrentCell = lootEntriesGrid.Rows[index].Cells[0];
            }
        }

        private void lootRemoveEntryButton_Click(object? sender, EventArgs e)
        {
            if (activeLootTable is null)
            {
                return;
            }

            if (SelectedLootEntry is LootEntry entry)
            {
                activeLootTable.Entries.Remove(entry);
                lootEntryBinding.ResetBindings(false);
                UpdateLootChart();
            }
        }

        private void UpdateLootTableDetails()
        {
            DetachFromActiveLootTable();

            activeLootTable = lootTablesListBox.SelectedItem as LootTable;

            if (activeLootTable is not null)
            {
                lootTableIdTextBox.Enabled = true;
                lootSaveTableButton.Enabled = true;
                lootDeleteTableButton.Enabled = true;
                lootEntriesGrid.Enabled = true;
                lootAddEntryButton.Enabled = true;
                lootKillsCounter.Enabled = true;

                lootTableIdTextBox.Text = activeLootTable.Id;
                lootEntryBinding.DataSource = activeLootTable.Entries;
                activeLootTable.Entries.ListChanged += LootEntries_ListChanged;
                lootEntryBinding.ResetBindings(false);
            }
            else
            {
                lootTableIdTextBox.Enabled = false;
                lootSaveTableButton.Enabled = false;
                lootDeleteTableButton.Enabled = false;
                lootEntriesGrid.Enabled = false;
                lootAddEntryButton.Enabled = false;
                lootKillsCounter.Enabled = false;

                lootTableIdTextBox.Text = string.Empty;
                lootEntryBinding.DataSource = typeof(LootEntry);
                lootEntryBinding.ResetBindings(false);
            }

            UpdateRemoveEntryButton();
            UpdateLootChart();
        }

        private void UpdateRemoveEntryButton()
        {
            lootRemoveEntryButton.Enabled = activeLootTable is not null && SelectedLootEntry is not null;
        }

        private void LootEntries_ListChanged(object? sender, ListChangedEventArgs e)
        {
            UpdateRemoveEntryButton();
            UpdateLootChart();
        }

        private LootEntry? SelectedLootEntry => lootEntriesGrid.CurrentRow?.DataBoundItem as LootEntry;

        private void UpdateLootChart()
        {
            lootDistributionChart.Series.Clear();

            if (activeLootTable is null)
            {
                lootChartHintLabel.Text = "Create or select a loot table to view drop probabilities.";
                lootChartHintLabel.Visible = true;
                return;
            }

            if (SelectedLootEntry is not LootEntry entry)
            {
                lootChartHintLabel.Text = activeLootTable.Entries.Count == 0
                    ? "Add loot entries to show probabilities."
                    : "Select an entry to see drop distribution.";
                lootChartHintLabel.Visible = true;
                return;
            }

            lootChartHintLabel.Visible = false;

            int kills = (int)lootKillsCounter.Value;
            double dropChance = Math.Clamp(entry.DropRate, 0d, 1d);

            Series series = new(entry.ItemId)
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = kills <= 30
            };

            for (int drops = 0; drops <= kills; drops++)
            {
                double probability = BinomialProbability(kills, drops, dropChance) * 100d;
                series.Points.AddXY(drops, Math.Round(probability, 4));
            }

            lootDistributionChart.Series.Add(series);

            if (lootDistributionChart.ChartAreas.Count > 0)
            {
                ChartArea area = lootDistributionChart.ChartAreas[0];
                area.AxisX.Minimum = 0;
                area.AxisX.Maximum = kills;
                area.AxisX.Interval = kills <= 20 ? 1 : Math.Max(1, kills / 10);
                area.AxisY.Maximum = 100;
                area.AxisY.Minimum = 0;
                area.RecalculateAxesScale();
            }
        }

        private static double BinomialProbability(int n, int k, double p)
        {
            if (k < 0 || k > n)
            {
                return 0d;
            }

            if (p <= 0d)
            {
                return k == 0 ? 1d : 0d;
            }

            if (p >= 1d)
            {
                return k == n ? 1d : 0d;
            }

            double combinations = Combination(n, k);
            double probability = Math.Pow(p, k) * Math.Pow(1d - p, n - k);
            return combinations * probability;
        }

        private static double Combination(int n, int k)
        {
            if (k < 0 || k > n)
            {
                return 0d;
            }

            k = Math.Min(k, n - k);
            double result = 1d;

            for (int i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }

            return result;
        }

        private static string CreateDefaultLootTableId()
        {
            int index = 1;
            string id;

            do
            {
                id = $"loot_table_{index++}";
            }
            while (LootManager.ContainsId(id));

            return id;
        }

        private void DetachFromActiveLootTable()
        {
            if (activeLootTable is not null)
            {
                activeLootTable.Entries.ListChanged -= LootEntries_ListChanged;
            }
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
    }
}
