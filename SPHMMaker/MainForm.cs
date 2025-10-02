using System.ComponentModel;
using System.Drawing;
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
        LootTable? selectedLootTable;


        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
            InitializeLoot();
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

        void InitializeLoot()
        {
            lootTableListBox.DisplayMember = nameof(LootTable.Display);
            lootTableListBox.DataSource = LootManager.LootTables;
            lootEntriesGrid.AutoGenerateColumns = false;
            lootEntriesGrid.DataSource = new BindingList<LootEntry>();

            lootEntriesGrid.CellValueChanged += (_, _) => UpdateLootChart();
            lootEntriesGrid.CurrentCellDirtyStateChanged += (_, _) =>
            {
                if (lootEntriesGrid.IsCurrentCellDirty)
                {
                    lootEntriesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            lootEntriesGrid.SelectionChanged += (_, _) => UpdateLootChart();
            lootEntriesGrid.RowsRemoved += (_, _) => UpdateLootChart();
            lootEntriesGrid.RowsAdded += (_, _) => UpdateLootChart();

            lootKillCountSetter.ValueChanged += (_, _) => UpdateLootChart();

            lootTableIdInput.ValueChanged += (_, _) =>
            {
                if (selectedLootTable is null)
                {
                    return;
                }

                int requestedId = (int)lootTableIdInput.Value;
                if (requestedId == selectedLootTable.Id)
                {
                    return;
                }

                if (LootManager.ContainsId(requestedId))
                {
                    MessageBox.Show($"Loot table id {requestedId} already exists.", "Duplicate Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lootTableIdInput.Value = selectedLootTable.Id;
                    return;
                }

                selectedLootTable.Id = requestedId;
            };

            lootTableNameInput.TextChanged += (_, _) =>
            {
                if (selectedLootTable is null)
                {
                    return;
                }

                selectedLootTable.Name = lootTableNameInput.Text;
            };

            lootAddTableButton.Click += (_, _) =>
            {
                int id = (int)lootTableIdInput.Value;
                if (LootManager.ContainsId(id))
                {
                    MessageBox.Show($"Loot table id {id} already exists.", "Duplicate Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LootTable table = LootManager.CreateLootTable(id, lootTableNameInput.Text);
                lootTableListBox.SelectedItem = table;
            };

            lootRemoveTableButton.Click += (_, _) =>
            {
                if (selectedLootTable is null)
                {
                    return;
                }

                LootManager.Remove(selectedLootTable);
                selectedLootTable = null;
                lootEntriesGrid.DataSource = new BindingList<LootEntry>();
                lootTableNameInput.Clear();
                lootTableIdInput.Value = 0;
                UpdateLootChart();
            };

            lootTableListBox.SelectedIndexChanged += (_, _) =>
            {
                if (lootTableListBox.SelectedItem is LootTable table)
                {
                    SelectLootTable(table);
                }
                else
                {
                    selectedLootTable = null;
                    lootEntriesGrid.DataSource = new BindingList<LootEntry>();
                }

                UpdateLootChart();
            };

            lootEntriesGrid.DataError += (_, args) =>
            {
                MessageBox.Show("Invalid value entered. Please use numeric values for the item id and drop chance (0-1).", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                args.Cancel = true;
            };

            InitializeLootChart();
        }

        void SelectLootTable(LootTable table)
        {
            if (selectedLootTable == table)
            {
                return;
            }

            selectedLootTable = table;
            lootTableIdInput.Value = Math.Max(lootTableIdInput.Minimum, Math.Min(lootTableIdInput.Maximum, table.Id));
            lootTableNameInput.Text = table.Name;

            BindingList<LootEntry> entries = table.Entries;
            lootEntriesGrid.DataSource = entries;
        }

        void InitializeLootChart()
        {
            lootProbabilityChart.Series.Clear();
            lootProbabilityChart.ChartAreas.Clear();
            ChartArea chartArea = new("LootProbabilityArea");
            chartArea.AxisX.Title = "Number of drops";
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Title = "Probability (%)";
            chartArea.AxisY.LabelStyle.Format = "{0:P1}";
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            lootProbabilityChart.ChartAreas.Add(chartArea);

            lootProbabilityChart.Legends.Clear();
            lootProbabilityChart.Titles.Clear();
            lootProbabilityChart.Titles.Add("Drop probability distribution");
        }

        LootEntry? SelectedLootEntry
        {
            get
            {
                if (lootEntriesGrid.CurrentRow?.DataBoundItem is LootEntry entry)
                {
                    return entry;
                }

                return null;
            }
        }

        void UpdateLootChart()
        {
            lootProbabilityChart.Series.Clear();

            LootEntry? entry = SelectedLootEntry;
            int kills = (int)lootKillCountSetter.Value;

            if (entry is null || selectedLootTable is null)
            {
                return;
            }

            var distribution = LootProbability.CalculateDistribution(entry, kills);

            Series series = new($"Item {entry.ItemId}")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.Int32,
                YValueType = ChartValueType.Double
            };

            foreach (var point in distribution)
            {
                double percentage = point.Probability;
                series.Points.AddXY(point.Drops, percentage);
            }

            lootProbabilityChart.Series.Add(series);
            lootProbabilityChart.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
