using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SPHMMaker.Items;
using SPHMMaker.Classes;
using SPHMMaker.SpawnZones;
using SPHMMaker.Tiles;
using SPHMMaker.Loot;
using SPHMMaker.Races;

namespace SPHMMaker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        const int ItemImageSize = 48;
        const int ItemHorizontalPadding = 8;
        const int ItemVerticalPadding = 4;
        static readonly string[] SupportedImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };

        readonly Dictionary<string, Image> itemImageCache = new();
        readonly Image defaultItemImage;
        bool imagesDisposed;
        TabPage? classTabPage;
        ListBox? classListBox;
        Label? selectedClassLabel;
        TextBox? classNameInput;
        TextBox? classRoleInput;
        NumericUpDown? classBaseHealthSetter;
        NumericUpDown? classBaseManaSetter;
        TextBox? classDescriptionInput;
        Button? createClassButton;
        Button? updateClassButton;
        Button? deleteClassButton;
        Label? selectedRaceLabel;
        TextBox? raceNameInput;
        TextBox? raceDescriptionInput;
        Button? createRaceButton;
        Button? updateRaceButton;
        Button? deleteRaceButton;
        static readonly IReadOnlyDictionary<ItemData.ItemQuality, Color> ItemQualityColors = new Dictionary<ItemData.ItemQuality, Color>
        {
            { ItemData.ItemQuality.Poor, Color.DimGray },
            { ItemData.ItemQuality.Common, Color.WhiteSmoke },
            { ItemData.ItemQuality.Uncommon, Color.MediumSeaGreen },
            { ItemData.ItemQuality.Rare, Color.RoyalBlue },
            { ItemData.ItemQuality.Epic, Color.MediumPurple },
            { ItemData.ItemQuality.Legendary, Color.Orange }
        };
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;
        private readonly BindingSource lootTableBinding = new();
        private readonly BindingSource lootEntryBinding = new();
        private readonly BindingSource classBindingSource = new();
        private readonly BindingList<ClassData> classDefinitions = new();
        private readonly BindingSource raceBindingSource = new();
        private readonly BindingList<RaceData> raceDefinitions = new();
        private LootTable? activeLootTable;
        int editingTile = -1;

        string? datapackSourcePath;
        string? datapackExtractionRoot;
        string? datapackRootPath;
        bool datapackLoadedFromArchive;


        public MainForm()
        {
            Instance = this;
            defaultItemImage = CreateDefaultItemImage();
            InitializeComponent();
            AllocConsole();

            InitializeItems();

            InitializeClassTab();
            InitializeClassDataBindings();

            InitializeLootTab();
            InitializeRaceTab();
            InitializeRaceDataBindings();
            InitializeSpawnZoneTab();
            InitializeSpawnZoneDataBindings();
        }

        void InitializeClassTab()
        {
            if (MainTab == null)
            {
                return;
            }

            classTabPage = new TabPage("Classes")
            {
                Padding = new Padding(8)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            classTabPage.Controls.Add(layout);
            MainTab.Controls.Add(classTabPage);

            var classListGroup = new GroupBox
            {
                Text = "Available Classes",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(classListGroup, 0, 0);

            var classListLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            classListLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            classListLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            classListGroup.Controls.Add(classListLayout);

            classListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            classListBox.SelectedIndexChanged += ClassListBox_SelectedIndexChanged;
            classListLayout.Controls.Add(classListBox, 0, 0);

            selectedClassLabel = new Label
            {
                Text = "Selected class: None",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classListLayout.Controls.Add(selectedClassLabel, 0, 1);

            var classDetailsGroup = new GroupBox
            {
                Text = "Class Details",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(classDetailsGroup, 1, 0);

            var classDetailsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6
            };
            classDetailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            classDetailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            classDetailsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            classDetailsGroup.Controls.Add(classDetailsLayout);

            var classNameLabel = new Label
            {
                Text = "Name",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classDetailsLayout.Controls.Add(classNameLabel, 0, 0);

            classNameInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            classDetailsLayout.Controls.Add(classNameInput, 1, 0);

            var classRoleLabel = new Label
            {
                Text = "Role",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classDetailsLayout.Controls.Add(classRoleLabel, 0, 1);

            classRoleInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            classDetailsLayout.Controls.Add(classRoleInput, 1, 1);

            var baseHealthLabel = new Label
            {
                Text = "Base Health",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classDetailsLayout.Controls.Add(baseHealthLabel, 0, 2);

            classBaseHealthSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 100000,
                Value = 100
            };
            classDetailsLayout.Controls.Add(classBaseHealthSetter, 1, 2);

            var baseManaLabel = new Label
            {
                Text = "Base Mana",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classDetailsLayout.Controls.Add(baseManaLabel, 0, 3);

            classBaseManaSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100000
            };
            classDetailsLayout.Controls.Add(classBaseManaSetter, 1, 3);

            var classDescriptionLabel = new Label
            {
                Text = "Description",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            classDetailsLayout.Controls.Add(classDescriptionLabel, 0, 4);

            classDescriptionInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            classDetailsLayout.Controls.Add(classDescriptionInput, 1, 4);

            var classButtonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };
            classDetailsLayout.Controls.Add(classButtonPanel, 0, 5);
            classDetailsLayout.SetColumnSpan(classButtonPanel, 2);

            createClassButton = new Button
            {
                Text = "Add Class"
            };
            createClassButton.Click += CreateClassButton_Click;
            classButtonPanel.Controls.Add(createClassButton);

            updateClassButton = new Button
            {
                Text = "Update Class"
            };
            updateClassButton.Click += UpdateClassButton_Click;
            classButtonPanel.Controls.Add(updateClassButton);

            deleteClassButton = new Button
            {
                Text = "Remove Class"
            };
            deleteClassButton.Click += DeleteClassButton_Click;
            classButtonPanel.Controls.Add(deleteClassButton);

            UpdateClassButtonStates();
        }

        void InitializeClassDataBindings()
        {
            classBindingSource.DataSource = classDefinitions;

            if (classListBox != null)
            {
                classListBox.DisplayMember = nameof(ClassData.DisplayText);
                classListBox.DataSource = classBindingSource;
            }

            PopulateClassFields(null);
        }

        void ClassListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateClassFields(classListBox?.SelectedItem as ClassData);
        }

        void PopulateClassFields(ClassData? classData)
        {
            if (classNameInput == null ||
                classRoleInput == null ||
                classBaseHealthSetter == null ||
                classBaseManaSetter == null ||
                classDescriptionInput == null)
            {
                return;
            }

            if (classData == null)
            {
                ClearClassInputs();
            }
            else
            {
                classNameInput.Text = classData.Name;
                classRoleInput.Text = classData.Role;

                decimal healthValue = Math.Clamp((decimal)classData.BaseHealth, classBaseHealthSetter.Minimum, classBaseHealthSetter.Maximum);
                classBaseHealthSetter.Value = healthValue;

                decimal manaValue = Math.Clamp((decimal)classData.BaseMana, classBaseManaSetter.Minimum, classBaseManaSetter.Maximum);
                classBaseManaSetter.Value = manaValue;

                classDescriptionInput.Text = classData.Description;
            }

            UpdateSelectedClassLabel();
            UpdateClassButtonStates();
        }

        void ClearClassInputs()
        {
            if (classNameInput != null)
            {
                classNameInput.Text = string.Empty;
            }

            if (classRoleInput != null)
            {
                classRoleInput.Text = string.Empty;
            }

            if (classBaseHealthSetter != null)
            {
                decimal defaultHealth = Math.Clamp(100m, classBaseHealthSetter.Minimum, classBaseHealthSetter.Maximum);
                classBaseHealthSetter.Value = defaultHealth;
            }

            if (classBaseManaSetter != null)
            {
                decimal defaultMana = Math.Clamp(0m, classBaseManaSetter.Minimum, classBaseManaSetter.Maximum);
                classBaseManaSetter.Value = defaultMana;
            }

            if (classDescriptionInput != null)
            {
                classDescriptionInput.Text = string.Empty;
            }
        }

        void UpdateSelectedClassLabel()
        {
            if (selectedClassLabel == null)
            {
                return;
            }

            if (classListBox?.SelectedItem is ClassData classData)
            {
                selectedClassLabel.Text = $"Selected class: {classData.DisplayText}";
            }
            else
            {
                selectedClassLabel.Text = "Selected class: None";
            }
        }

        void UpdateClassButtonStates()
        {
            bool hasSelection = classListBox?.SelectedItem is ClassData;

            if (updateClassButton != null)
            {
                updateClassButton.Enabled = hasSelection;
            }

            if (deleteClassButton != null)
            {
                deleteClassButton.Enabled = hasSelection;
            }
        }

        void CreateClassButton_Click(object? sender, EventArgs e)
        {
            if (classNameInput == null ||
                classRoleInput == null ||
                classBaseHealthSetter == null ||
                classBaseManaSetter == null ||
                classDescriptionInput == null)
            {
                return;
            }

            string name = classNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Class name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                classNameInput.Focus();
                return;
            }

            var newClass = new ClassData
            {
                Name = name,
                Role = classRoleInput.Text.Trim(),
                BaseHealth = (int)classBaseHealthSetter.Value,
                BaseMana = (int)classBaseManaSetter.Value,
                Description = classDescriptionInput.Text
            };

            classDefinitions.Add(newClass);
            classBindingSource.ResetBindings(false);

            if (classListBox != null)
            {
                classListBox.SelectedItem = newClass;
            }
        }

        void UpdateClassButton_Click(object? sender, EventArgs e)
        {
            if (classNameInput == null ||
                classRoleInput == null ||
                classBaseHealthSetter == null ||
                classBaseManaSetter == null ||
                classDescriptionInput == null)
            {
                return;
            }

            if (classListBox?.SelectedItem is not ClassData classData)
            {
                return;
            }

            string name = classNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Class name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                classNameInput.Focus();
                return;
            }

            classData.Name = name;
            classData.Role = classRoleInput.Text.Trim();
            classData.BaseHealth = (int)classBaseHealthSetter.Value;
            classData.BaseMana = (int)classBaseManaSetter.Value;
            classData.Description = classDescriptionInput.Text;

            classBindingSource.ResetCurrentItem();
            UpdateSelectedClassLabel();
        }

        void DeleteClassButton_Click(object? sender, EventArgs e)
        {
            if (classListBox?.SelectedItem is not ClassData classData)
            {
                return;
            }

            DialogResult result = MessageBox.Show($"Delete class '{classData.DisplayText}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            classDefinitions.Remove(classData);
            classBindingSource.ResetBindings(false);

            PopulateClassFields(classListBox?.SelectedItem as ClassData);
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
            string idText = lootTableIdTextBox.Text.Trim();
            int id;

            if (string.IsNullOrEmpty(idText))
            {
                id = CreateDefaultLootTableId();
            }
            else if (!int.TryParse(idText, out id))
            {
                MessageBox.Show("Loot table ID must be a whole number.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lootTableIdTextBox.Focus();
                lootTableIdTextBox.SelectAll();
                return;
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

            string idText = lootTableIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(idText))
            {
                MessageBox.Show("Loot table ID cannot be empty.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lootTableIdTextBox.Focus();
                return;
            }

            if (!int.TryParse(idText, out int id))
            {
                MessageBox.Show("Loot table ID must be a whole number.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lootTableIdTextBox.Focus();
                lootTableIdTextBox.SelectAll();
                return;
            }

            if (activeLootTable.Id != id && LootManager.ContainsId(id))
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

                lootTableIdTextBox.Text = activeLootTable.Id.ToString();
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

        private static int CreateDefaultLootTableId()
        {
            int index = 1;

            while (LootManager.ContainsId(index))
            {
                index++;
            }

            return index;
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
            var selection = PromptForDatapackLoad();
            if (selection.Path is null)
            {
                return;
            }

            LoadDatapack(selection.Path, selection.IsArchive);
        }

        private void items_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= items.Items.Count)
            {
                return;
            }

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backgroundColor = isSelected ? SystemColors.Highlight : items.BackColor;
            Color textColor = isSelected ? SystemColors.HighlightText : items.ForeColor;

            using (SolidBrush backgroundBrush = new(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            if (items.Items[e.Index] is not ItemData item)
            {
                TextRenderer.DrawText(e.Graphics, items.Items[e.Index].ToString() ?? string.Empty, e.Font, e.Bounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.DrawFocusRectangle();
                return;
            }

            Rectangle imageRect = CalculateImageRectangle(e.Bounds);
            Image image = GetItemImage(item);
            e.Graphics.DrawImage(image, imageRect);

            Rectangle borderRect = Rectangle.Inflate(imageRect, 1, 1);
            using (Pen borderPen = new(GetQualityColor(item.Quality), 2f))
            {
                e.Graphics.DrawRectangle(borderPen, borderRect);
            }

            Rectangle textRect = new(imageRect.Right + ItemHorizontalPadding, e.Bounds.Y, e.Bounds.Width - imageRect.Width - (ItemHorizontalPadding * 2), e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, item.Name, e.Font, textRect, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            e.DrawFocusRectangle();
        }

        static Rectangle CalculateImageRectangle(Rectangle bounds)
        {
            int availableHeight = Math.Max(bounds.Height - (ItemVerticalPadding * 2), 1);
            int size = Math.Min(ItemImageSize, availableHeight);
            int imageY = bounds.Y + ((bounds.Height - size) / 2);
            return new Rectangle(bounds.X + ItemHorizontalPadding, imageY, size, size);
        }

        Image GetItemImage(ItemData item)
        {
            if (item.GfxPath == null || string.IsNullOrWhiteSpace(item.GfxPath.Name))
            {
                return defaultItemImage;
            }

            string cacheKey = item.GfxPath.ToString();

            if (itemImageCache.TryGetValue(cacheKey, out Image? cachedImage))
            {
                return cachedImage;
            }

            foreach (string candidate in GetCandidatePaths(item))
            {
                if (!TryLoadImage(candidate, out Image? loadedImage))
                {
                    continue;
                }

                itemImageCache[cacheKey] = loadedImage;
                return loadedImage;
            }

            itemImageCache[cacheKey] = defaultItemImage;
            return defaultItemImage;
        }

        static IEnumerable<string> GetCandidatePaths(ItemData item)
        {
            if (item.GfxPath == null)
            {
                yield break;
            }

            string relativePath = item.GfxPath.ToString();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            yield return Path.Combine(baseDirectory, relativePath);
            yield return Path.Combine(baseDirectory, "Gfx", relativePath);
            yield return Path.Combine(baseDirectory, item.GfxPath.Type.ToString(), item.GfxPath.Name);
            yield return Path.Combine(baseDirectory, "Gfx", item.GfxPath.Type.ToString(), item.GfxPath.Name);
        }

        static bool TryLoadImage(string basePath, out Image? image)
        {
            if (TryLoadImageCore(basePath, out image))
            {
                return true;
            }

            foreach (string extension in SupportedImageExtensions)
            {
                string candidate = basePath + extension;
                if (TryLoadImageCore(candidate, out image))
                {
                    return true;
                }
            }

            image = null;
            return false;
        }

        static bool TryLoadImageCore(string path, out Image? image)
        {
            if (!File.Exists(path))
            {
                image = null;
                return false;
            }

            try
            {
                image = Image.FromFile(path);
                return true;
            }
            catch (Exception)
            {
                image = null;
                return false;
            }
        }

        static Image CreateDefaultItemImage()
        {
            Bitmap bitmap = new(ItemImageSize, ItemImageSize);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.FromArgb(48, 48, 48));

                using Font font = new(FontFamily.GenericSansSerif, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                using SolidBrush brush = new(Color.LightGray);
                using StringFormat format = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphics.DrawString("?", font, brush, new RectangleF(0, 0, ItemImageSize, ItemImageSize), format);
            }

            return bitmap;
        }

        static Color GetQualityColor(ItemData.ItemQuality quality)
        {
            if (ItemQualityColors.TryGetValue(quality, out Color color))
            {
                return color;
            }

            return Color.White;
        }

        void DisposeItemImages()
        {
            if (imagesDisposed)
            {
                return;
            }

            foreach (Image image in itemImageCache.Values)
            {
                if (!ReferenceEquals(image, defaultItemImage))
                {
                    image.Dispose();
                }
            }

            defaultItemImage.Dispose();
            itemImageCache.Clear();
            imagesDisposed = true;
        }

        private void saveDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selection = PromptForDatapackSave();
            if (selection.Path is null)
            {
                MessageBox.Show("Save aborted.");
                return;
            }

            SaveDatapack(selection.Path, selection.IsArchive);
        }

        private (string? Path, bool IsArchive) PromptForDatapackLoad()
        {
            DialogResult choice = MessageBox.Show(
                "Is the datapack stored as a .zip archive?\nChoose Yes to load a .zip file or No to load from an extracted folder.",
                "Load Datapack",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choice == DialogResult.Cancel)
            {
                return (null, false);
            }

            if (choice == DialogResult.Yes)
            {
                using OpenFileDialog dialog = new()
                {
                    Filter = "Datapack archive (*.zip)|*.zip",
                    Title = "Select Datapack Archive",
                    InitialDirectory = GetInitialDatapackDirectory()
                };

                return dialog.ShowDialog() == DialogResult.OK
                    ? (dialog.FileName, true)
                    : (null, false);
            }

            using FolderBrowserDialog folderDialog = new()
            {
                Description = "Select Datapack Folder",
                SelectedPath = GetInitialDatapackDirectory()
            };

            return folderDialog.ShowDialog() == DialogResult.OK
                ? (folderDialog.SelectedPath, false)
                : (null, false);
        }

        private (string? Path, bool IsArchive) PromptForDatapackSave()
        {
            DialogResult choice = MessageBox.Show(
                "Would you like to save the datapack as a .zip archive?\nChoose Yes for a .zip file or No to export to a folder.",
                "Save Datapack",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choice == DialogResult.Cancel)
            {
                return (null, false);
            }

            if (choice == DialogResult.Yes)
            {
                using SaveFileDialog dialog = new()
                {
                    Filter = "Datapack archive (*.zip)|*.zip",
                    Title = "Save Datapack",
                    FileName = GetDefaultDatapackFileName(includeExtension: true),
                    InitialDirectory = GetInitialDatapackDirectory()
                };

                return dialog.ShowDialog() == DialogResult.OK
                    ? (dialog.FileName, true)
                    : (null, false);
            }

            using FolderBrowserDialog folderDialog = new()
            {
                Description = "Select Folder to Save Datapack",
                SelectedPath = GetInitialDatapackDirectory()
            };

            return folderDialog.ShowDialog() == DialogResult.OK
                ? (folderDialog.SelectedPath, false)
                : (null, false);
        }

        private void LoadDatapack(string path, bool isArchive)
        {
            string? previousExtraction = datapackExtractionRoot;
            string? newExtractionRoot = null;

            try
            {
                string datapackRoot = path;

                if (isArchive)
                {
                    var extraction = DatapackArchive.ExtractToTemporaryDirectory(path);
                    datapackRoot = extraction.DatapackRoot;
                    newExtractionRoot = extraction.ExtractionRoot;
                }

                string itemsDirectory = Path.Combine(datapackRoot, "Items");
                if (!Directory.Exists(itemsDirectory))
                {
                    throw new DirectoryNotFoundException("The datapack does not contain an Items directory.");
                }

                bool itemsLoaded = ItemManager.Load(itemsDirectory);
                if (!itemsLoaded)
                {
                    throw new InvalidDataException("Failed to load item data from the datapack.");
                }

                string tilesDirectory = Path.Combine(datapackRoot, "Tiles");
                TileManager.Load(tilesDirectory);

                datapackSourcePath = path;
                datapackRootPath = datapackRoot;
                datapackLoadedFromArchive = isArchive;
                datapackExtractionRoot = newExtractionRoot;

                if (!string.IsNullOrEmpty(previousExtraction) && previousExtraction != newExtractionRoot)
                {
                    TryDeleteDirectory(previousExtraction);
                }

                MessageBox.Show("Datapack loaded successfully.", "Load Datapack", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (newExtractionRoot != null)
                {
                    TryDeleteDirectory(newExtractionRoot);
                }

                datapackExtractionRoot = previousExtraction;

                MessageBox.Show($"Failed to load datapack: {ex.Message}", "Load Datapack", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDatapack(string destinationPath, bool asArchive)
        {
            try
            {
                if (asArchive)
                {
                    string tempRoot = Path.Combine(Path.GetTempPath(), "SPHMMaker", "export", Guid.NewGuid().ToString("N"));
                    Directory.CreateDirectory(tempRoot);

                    try
                    {
                        bool itemsSaved = ItemManager.Save(Path.Combine(tempRoot, "Items"));
                        bool tilesSaved = TileManager.Save(Path.Combine(tempRoot, "Tiles"));

                        if (!itemsSaved && !tilesSaved)
                        {
                            throw new InvalidOperationException("There is no data to save.");
                        }

                        DatapackArchive.CreateArchive(tempRoot, destinationPath);
                    }
                    finally
                    {
                        TryDeleteDirectory(tempRoot);
                    }
                }
                else
                {
                    Directory.CreateDirectory(destinationPath);

                    string itemsDirectory = Path.Combine(destinationPath, "Items");
                    string tilesDirectory = Path.Combine(destinationPath, "Tiles");

                    if (Directory.Exists(itemsDirectory))
                    {
                        Directory.Delete(itemsDirectory, true);
                    }

                    if (Directory.Exists(tilesDirectory))
                    {
                        Directory.Delete(tilesDirectory, true);
                    }

                    bool itemsSaved = ItemManager.Save(itemsDirectory);
                    bool tilesSaved = TileManager.Save(tilesDirectory);

                    if (!itemsSaved && !tilesSaved)
                    {
                        throw new InvalidOperationException("There is no data to save.");
                    }
                }

                MessageBox.Show("Datapack saved successfully.", "Save Datapack", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save datapack: {ex.Message}", "Save Datapack", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDefaultDatapackFileName(bool includeExtension)
        {
            string baseName = "datapack";

            if (!string.IsNullOrEmpty(datapackSourcePath))
            {
                if (datapackLoadedFromArchive)
                {
                    baseName = Path.GetFileNameWithoutExtension(datapackSourcePath) ?? baseName;
                }
                else
                {
                    string trimmed = datapackSourcePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    string? folderName = Path.GetFileName(trimmed);
                    if (!string.IsNullOrEmpty(folderName))
                    {
                        baseName = folderName;
                    }
                }
            }

            return includeExtension ? baseName + ".zip" : baseName;
        }

        private string GetInitialDatapackDirectory()
        {
            if (string.IsNullOrEmpty(datapackSourcePath))
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }

            if (datapackLoadedFromArchive)
            {
                string? directory = Path.GetDirectoryName(datapackSourcePath);
                return string.IsNullOrEmpty(directory) ? AppDomain.CurrentDomain.BaseDirectory : directory;
            }

            return Directory.Exists(datapackSourcePath) ? datapackSourcePath : AppDomain.CurrentDomain.BaseDirectory;
        }

        private static void TryDeleteDirectory(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
            {
                // Ignore cleanup failures.
            }
        }

        private void itemCheckGeneratedTooltip_Click(object sender, EventArgs e)
        {
            ItemData? item = FoldDataIntoItem();
            if (item is null)
            {
                return;
            }
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            TryDeleteDirectory(datapackExtractionRoot);
            datapackExtractionRoot = null;
            base.OnFormClosed(e);
        }

        private void xdd()
        {
            MessageBox.Show("instructions", "File Download Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void spriteEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = new SpriteEditorForm();
            editor.Show(this);
        }

        void InitializeRaceTab()
        {
            if (MainTab == null)
            {
                return;
            }

            raceTabPage = new TabPage("Races")
            {
                Padding = new Padding(8)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            raceTabPage.Controls.Add(layout);
            MainTab.Controls.Add(raceTabPage);

            raceListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            raceListBox.SelectedIndexChanged += RaceListBox_SelectedIndexChanged;
            layout.Controls.Add(raceListBox, 0, 0);
            layout.SetRowSpan(raceListBox, 2);

            var detailGroup = new GroupBox
            {
                Text = "Race Details",
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(detailGroup, 1, 0);

            var detailLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            detailGroup.Controls.Add(detailLayout);

            selectedRaceLabel = new Label
            {
                Text = "Selected race: None",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(selectedRaceLabel, 0, 0);
            detailLayout.SetColumnSpan(selectedRaceLabel, 2);

            var nameLabel = new Label
            {
                Text = "Name",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(nameLabel, 0, 1);

            raceNameInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            detailLayout.Controls.Add(raceNameInput, 1, 1);

            var descriptionLabel = new Label
            {
                Text = "Description",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(descriptionLabel, 0, 2);
            detailLayout.SetColumnSpan(descriptionLabel, 2);

            raceDescriptionInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            detailLayout.Controls.Add(raceDescriptionInput, 0, 3);
            detailLayout.SetColumnSpan(raceDescriptionInput, 2);

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };
            layout.Controls.Add(buttonPanel, 1, 1);

            createRaceButton = new Button
            {
                Text = "Create Race"
            };
            createRaceButton.Click += CreateRaceButton_Click;
            buttonPanel.Controls.Add(createRaceButton);

            updateRaceButton = new Button
            {
                Text = "Save Changes",
                Enabled = false
            };
            updateRaceButton.Click += UpdateRaceButton_Click;
            buttonPanel.Controls.Add(updateRaceButton);

            deleteRaceButton = new Button
            {
                Text = "Remove Race",
                Enabled = false
            };
            deleteRaceButton.Click += DeleteRaceButton_Click;
            buttonPanel.Controls.Add(deleteRaceButton);
        }

        void InitializeRaceDataBindings()
        {
            raceBindingSource.DataSource = raceDefinitions;

            if (raceListBox != null)
            {
                raceListBox.DisplayMember = nameof(RaceData.DisplayText);
                raceListBox.DataSource = raceBindingSource;
            }

            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }

        void RaceListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }

        void PopulateRaceFields(RaceData? race)
        {
            if (raceNameInput == null || raceDescriptionInput == null)
            {
                return;
            }

            if (race == null)
            {
                raceNameInput.Text = string.Empty;
                raceDescriptionInput.Text = string.Empty;
            }
            else
            {
                raceNameInput.Text = race.Name;
                raceDescriptionInput.Text = race.Description;
            }

            if (selectedRaceLabel != null)
            {
                selectedRaceLabel.Text = race == null ? "Selected race: None" : $"Selected race: {race.DisplayText}";
            }

            if (updateRaceButton != null)
            {
                updateRaceButton.Enabled = race != null;
            }

            if (deleteRaceButton != null)
            {
                deleteRaceButton.Enabled = race != null;
            }
        }

        void CreateRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceNameInput == null)
            {
                return;
            }

            string name = raceNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a race name before adding it.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var race = new RaceData
            {
                Name = name,
                Description = raceDescriptionInput?.Text.Trim() ?? string.Empty
            };

            raceDefinitions.Add(race);
            raceBindingSource.ResetBindings(false);

            if (raceListBox != null)
            {
                raceListBox.SelectedItem = race;
            }
        }

        void UpdateRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceNameInput == null)
            {
                return;
            }

            if (raceListBox?.SelectedItem is not RaceData race)
            {
                MessageBox.Show("Select a race to update first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = raceNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("A race must have a name.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            race.Name = name;
            race.Description = raceDescriptionInput?.Text.Trim() ?? string.Empty;

            raceBindingSource.ResetCurrentItem();
            PopulateRaceFields(race);
        }

        void DeleteRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceListBox?.SelectedItem is not RaceData race)
            {
                MessageBox.Show("Select a race to remove first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            raceDefinitions.Remove(race);
            raceBindingSource.ResetBindings(false);
            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }

    }
}
