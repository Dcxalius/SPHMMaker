using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SPHMMaker.Items;
using SPHMMaker.SpawnZones;
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

        TabPage? spawnZoneTabPage;
        ListBox? unitDataListBox;
        TextBox? unitNameInput;
        NumericUpDown? unitLevelSetter;
        TextBox? unitNotesInput;
        Button? createUnitButton;
        Button? updateUnitButton;
        Button? deleteUnitButton;
        Label? selectedUnitLabel;
        Label? selectedZoneLabel;

        ListBox? spawnZoneListBox;
        TextBox? spawnZoneNameInput;
        TextBox? spawnZoneNotesInput;
        Button? createSpawnZoneButton;
        Button? updateSpawnZoneButton;
        Button? deleteSpawnZoneButton;

        ListBox? spawnZoneAssignmentsListBox;
        NumericUpDown? assignmentMinimumSetter;
        NumericUpDown? assignmentMaximumSetter;
        Button? assignUnitButton;
        Button? removeAssignmentButton;

        readonly BindingList<UnitData> unitDefinitions = new();
        readonly BindingList<SpawnZoneData> spawnZoneDefinitions = new();
        readonly BindingSource unitBindingSource = new();
        readonly BindingSource spawnZoneBindingSource = new();
        readonly BindingSource assignmentBindingSource = new();


        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
            InitializeSpawnZoneTab();
            InitializeSpawnZoneDataBindings();
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

        void InitializeSpawnZoneTab()
        {
            if (MainTab == null)
            {
                return;
            }

            spawnZoneTabPage = new TabPage("Spawn Zones")
            {
                Padding = new Padding(8)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            spawnZoneTabPage.Controls.Add(layout);
            MainTab.Controls.Add(spawnZoneTabPage);

            var unitGroup = new GroupBox
            {
                Text = "Unit Data",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(unitGroup, 0, 0);

            var unitLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7
            };
            unitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            unitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            unitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            unitGroup.Controls.Add(unitLayout);

            unitDataListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            unitDataListBox.SelectedIndexChanged += UnitDataListBox_SelectedIndexChanged;
            unitLayout.Controls.Add(unitDataListBox, 0, 0);
            unitLayout.SetColumnSpan(unitDataListBox, 2);

            selectedUnitLabel = new Label
            {
                Text = "Selected unit: None",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            unitLayout.Controls.Add(selectedUnitLabel, 0, 1);
            unitLayout.SetColumnSpan(selectedUnitLabel, 2);

            var unitNameLabel = new Label
            {
                Text = "Name",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            unitLayout.Controls.Add(unitNameLabel, 0, 2);

            unitNameInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            unitLayout.Controls.Add(unitNameInput, 1, 2);

            var unitLevelLabel = new Label
            {
                Text = "Level",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            unitLayout.Controls.Add(unitLevelLabel, 0, 3);

            unitLevelSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 200,
                Value = 1
            };
            unitLayout.Controls.Add(unitLevelSetter, 1, 3);

            var unitNotesLabel = new Label
            {
                Text = "Notes",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            unitLayout.Controls.Add(unitNotesLabel, 0, 4);
            unitLayout.SetColumnSpan(unitNotesLabel, 2);

            unitNotesInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            unitLayout.Controls.Add(unitNotesInput, 0, 5);
            unitLayout.SetColumnSpan(unitNotesInput, 2);

            var unitButtonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            createUnitButton = new Button
            {
                Text = "Add Unit"
            };
            createUnitButton.Click += CreateUnitButton_Click;
            unitButtonPanel.Controls.Add(createUnitButton);

            updateUnitButton = new Button
            {
                Text = "Update Unit"
            };
            updateUnitButton.Click += UpdateUnitButton_Click;
            unitButtonPanel.Controls.Add(updateUnitButton);

            deleteUnitButton = new Button
            {
                Text = "Remove Unit"
            };
            deleteUnitButton.Click += DeleteUnitButton_Click;
            unitButtonPanel.Controls.Add(deleteUnitButton);

            unitLayout.Controls.Add(unitButtonPanel, 0, 6);
            unitLayout.SetColumnSpan(unitButtonPanel, 2);

            var spawnZoneGroup = new GroupBox
            {
                Text = "Spawn Zones",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(spawnZoneGroup, 1, 0);

            var spawnLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7
            };
            spawnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            spawnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            spawnZoneGroup.Controls.Add(spawnLayout);

            spawnZoneListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            spawnZoneListBox.SelectedIndexChanged += SpawnZoneListBox_SelectedIndexChanged;
            spawnLayout.Controls.Add(spawnZoneListBox, 0, 0);
            spawnLayout.SetColumnSpan(spawnZoneListBox, 2);

            selectedZoneLabel = new Label
            {
                Text = "Selected zone: None",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            spawnLayout.Controls.Add(selectedZoneLabel, 0, 1);
            spawnLayout.SetColumnSpan(selectedZoneLabel, 2);

            var spawnZoneNameLabel = new Label
            {
                Text = "Name",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            spawnLayout.Controls.Add(spawnZoneNameLabel, 0, 2);

            spawnZoneNameInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            spawnLayout.Controls.Add(spawnZoneNameInput, 1, 2);

            var spawnZoneNotesLabel = new Label
            {
                Text = "Notes",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            spawnLayout.Controls.Add(spawnZoneNotesLabel, 0, 3);
            spawnLayout.SetColumnSpan(spawnZoneNotesLabel, 2);

            spawnZoneNotesInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            spawnLayout.Controls.Add(spawnZoneNotesInput, 0, 4);
            spawnLayout.SetColumnSpan(spawnZoneNotesInput, 2);

            var spawnZoneButtonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            createSpawnZoneButton = new Button
            {
                Text = "Add Zone"
            };
            createSpawnZoneButton.Click += CreateSpawnZoneButton_Click;
            spawnZoneButtonPanel.Controls.Add(createSpawnZoneButton);

            updateSpawnZoneButton = new Button
            {
                Text = "Update Zone"
            };
            updateSpawnZoneButton.Click += UpdateSpawnZoneButton_Click;
            spawnZoneButtonPanel.Controls.Add(updateSpawnZoneButton);

            deleteSpawnZoneButton = new Button
            {
                Text = "Remove Zone"
            };
            deleteSpawnZoneButton.Click += DeleteSpawnZoneButton_Click;
            spawnZoneButtonPanel.Controls.Add(deleteSpawnZoneButton);

            spawnLayout.Controls.Add(spawnZoneButtonPanel, 0, 5);
            spawnLayout.SetColumnSpan(spawnZoneButtonPanel, 2);

            var assignmentGroup = new GroupBox
            {
                Text = "Assigned Units",
                Dock = DockStyle.Fill
            };
            spawnLayout.Controls.Add(assignmentGroup, 0, 6);
            spawnLayout.SetColumnSpan(assignmentGroup, 2);

            var assignmentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6
            };
            assignmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            assignmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            assignmentGroup.Controls.Add(assignmentLayout);

            spawnZoneAssignmentsListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            spawnZoneAssignmentsListBox.SelectedIndexChanged += SpawnZoneAssignmentsListBox_SelectedIndexChanged;
            assignmentLayout.Controls.Add(spawnZoneAssignmentsListBox, 0, 0);
            assignmentLayout.SetColumnSpan(spawnZoneAssignmentsListBox, 2);

            var assignmentInstructionsLabel = new Label
            {
                Text = "Select a unit on the left and set the spawn range.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(assignmentInstructionsLabel, 0, 1);
            assignmentLayout.SetColumnSpan(assignmentInstructionsLabel, 2);

            var minLabel = new Label
            {
                Text = "Minimum",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(minLabel, 0, 2);

            assignmentMinimumSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 999,
                Value = 1
            };
            assignmentLayout.Controls.Add(assignmentMinimumSetter, 1, 2);

            var maxLabel = new Label
            {
                Text = "Maximum",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(maxLabel, 0, 3);

            assignmentMaximumSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 999,
                Value = 1
            };
            assignmentLayout.Controls.Add(assignmentMaximumSetter, 1, 3);

            var assignmentButtonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            assignUnitButton = new Button
            {
                Text = "Assign / Update"
            };
            assignUnitButton.Click += AssignUnitButton_Click;
            assignmentButtonPanel.Controls.Add(assignUnitButton);

            removeAssignmentButton = new Button
            {
                Text = "Remove"
            };
            removeAssignmentButton.Click += RemoveAssignmentButton_Click;
            assignmentButtonPanel.Controls.Add(removeAssignmentButton);

            assignmentLayout.Controls.Add(assignmentButtonPanel, 0, 4);
            assignmentLayout.SetColumnSpan(assignmentButtonPanel, 2);

            var assignmentHintLabel = new Label
            {
                Text = "Assignments automatically update when the unit data changes.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(assignmentHintLabel, 0, 5);
            assignmentLayout.SetColumnSpan(assignmentHintLabel, 2);
        }

        void InitializeSpawnZoneDataBindings()
        {
            unitBindingSource.DataSource = unitDefinitions;
            spawnZoneBindingSource.DataSource = spawnZoneDefinitions;

            if (unitDataListBox != null)
            {
                unitDataListBox.DataSource = unitBindingSource;
                unitDataListBox.DisplayMember = nameof(UnitData.DisplayText);
            }

            if (spawnZoneListBox != null)
            {
                spawnZoneListBox.DataSource = spawnZoneBindingSource;
                spawnZoneListBox.DisplayMember = nameof(SpawnZoneData.DisplayText);
            }
        }

        void UnitDataListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateUnitFields(unitDataListBox?.SelectedItem as UnitData);
        }

        void PopulateUnitFields(UnitData? unit)
        {
            if (unitNameInput == null || unitLevelSetter == null || unitNotesInput == null)
            {
                return;
            }

            if (unit == null)
            {
                unitNameInput.Text = string.Empty;
                unitLevelSetter.Value = unitLevelSetter.Minimum;
                unitNotesInput.Text = string.Empty;
            }
            else
            {
                unitNameInput.Text = unit.Name;
                decimal levelValue = Math.Min(Math.Max(unit.Level, (int)unitLevelSetter.Minimum), (int)unitLevelSetter.Maximum);
                unitLevelSetter.Value = levelValue;
                unitNotesInput.Text = unit.Notes;
            }

            UpdateSelectedUnitLabel();
        }

        void UpdateSelectedUnitLabel()
        {
            if (selectedUnitLabel == null)
            {
                return;
            }

            if (unitDataListBox?.SelectedItem is UnitData unit)
            {
                selectedUnitLabel.Text = $"Selected unit: {unit.DisplayText}";
            }
            else
            {
                selectedUnitLabel.Text = "Selected unit: None";
            }
        }

        void CreateUnitButton_Click(object? sender, EventArgs e)
        {
            if (unitNameInput == null || unitLevelSetter == null)
            {
                return;
            }

            string name = unitNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a unit name before adding it.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var unit = new UnitData
            {
                Name = name,
                Level = (int)unitLevelSetter.Value,
                Notes = unitNotesInput?.Text.Trim() ?? string.Empty
            };

            unitDefinitions.Add(unit);
            unitBindingSource.ResetBindings(false);
            if (unitDataListBox != null)
            {
                unitDataListBox.SelectedItem = unit;
            }
        }

        void UpdateUnitButton_Click(object? sender, EventArgs e)
        {
            if (unitNameInput == null || unitLevelSetter == null)
            {
                return;
            }

            if (unitDataListBox?.SelectedItem is not UnitData unit)
            {
                MessageBox.Show("Select a unit to update first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = unitNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("A unit must have a name.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            unit.Name = name;
            unit.Level = (int)unitLevelSetter.Value;
            unit.Notes = unitNotesInput?.Text.Trim() ?? string.Empty;

            unitBindingSource.ResetCurrentItem();
            UpdateAssignmentDisplayForUnit(unit);
            UpdateSelectedUnitLabel();
        }

        void DeleteUnitButton_Click(object? sender, EventArgs e)
        {
            if (unitDataListBox?.SelectedItem is not UnitData unit)
            {
                MessageBox.Show("Select a unit to remove first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            unitDefinitions.Remove(unit);
            unitBindingSource.ResetBindings(false);
            RemoveUnitAssignments(unit);
            UpdateAssignmentBinding();
        }

        void RemoveUnitAssignments(UnitData unit)
        {
            foreach (SpawnZoneData zone in spawnZoneDefinitions)
            {
                var toRemove = zone.Assignments.Where(a => a.Unit == unit).ToList();
                foreach (SpawnZoneAssignment assignment in toRemove)
                {
                    assignment.Detach();
                    zone.Assignments.Remove(assignment);
                }
            }
        }

        void UpdateAssignmentDisplayForUnit(UnitData unit)
        {
            foreach (SpawnZoneData zone in spawnZoneDefinitions)
            {
                foreach (SpawnZoneAssignment assignment in zone.Assignments.Where(a => a.Unit == unit))
                {
                    assignment.NotifyUnitUpdated();
                }
            }
        }

        void SpawnZoneListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateSpawnZoneFields(spawnZoneListBox?.SelectedItem as SpawnZoneData);
        }

        void PopulateSpawnZoneFields(SpawnZoneData? zone)
        {
            if (spawnZoneNameInput == null || spawnZoneNotesInput == null)
            {
                return;
            }

            if (zone == null)
            {
                spawnZoneNameInput.Text = string.Empty;
                spawnZoneNotesInput.Text = string.Empty;
            }
            else
            {
                spawnZoneNameInput.Text = zone.Name;
                spawnZoneNotesInput.Text = zone.Notes;
            }

            if (selectedZoneLabel != null)
            {
                selectedZoneLabel.Text = zone == null ? "Selected zone: None" : $"Selected zone: {zone.DisplayText}";
            }

            UpdateAssignmentBinding();
        }

        void CreateSpawnZoneButton_Click(object? sender, EventArgs e)
        {
            if (spawnZoneNameInput == null)
            {
                return;
            }

            string name = spawnZoneNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a spawn zone name before adding it.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var zone = new SpawnZoneData
            {
                Name = name,
                Notes = spawnZoneNotesInput?.Text.Trim() ?? string.Empty
            };

            spawnZoneDefinitions.Add(zone);
            spawnZoneBindingSource.ResetBindings(false);

            if (spawnZoneListBox != null)
            {
                spawnZoneListBox.SelectedItem = zone;
            }
        }

        void UpdateSpawnZoneButton_Click(object? sender, EventArgs e)
        {
            if (spawnZoneNameInput == null)
            {
                return;
            }

            if (spawnZoneListBox?.SelectedItem is not SpawnZoneData zone)
            {
                MessageBox.Show("Select a spawn zone to update first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = spawnZoneNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("A spawn zone must have a name.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            zone.Name = name;
            zone.Notes = spawnZoneNotesInput?.Text.Trim() ?? string.Empty;

            spawnZoneBindingSource.ResetCurrentItem();
        }

        void DeleteSpawnZoneButton_Click(object? sender, EventArgs e)
        {
            if (spawnZoneListBox?.SelectedItem is not SpawnZoneData zone)
            {
                MessageBox.Show("Select a spawn zone to remove first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DetachAssignments(zone);
            spawnZoneDefinitions.Remove(zone);
            spawnZoneBindingSource.ResetBindings(false);
            UpdateAssignmentBinding();
        }

        void DetachAssignments(SpawnZoneData zone)
        {
            foreach (SpawnZoneAssignment assignment in zone.Assignments.ToList())
            {
                assignment.Detach();
            }

            zone.Assignments.Clear();
        }

        void UpdateAssignmentBinding()
        {
            if (spawnZoneAssignmentsListBox == null)
            {
                return;
            }

            if (spawnZoneListBox?.SelectedItem is SpawnZoneData zone)
            {
                assignmentBindingSource.DataSource = zone.Assignments;
                spawnZoneAssignmentsListBox.DataSource = assignmentBindingSource;
                spawnZoneAssignmentsListBox.DisplayMember = nameof(SpawnZoneAssignment.DisplayText);
            }
            else
            {
                assignmentBindingSource.DataSource = null;
                spawnZoneAssignmentsListBox.DataSource = null;
            }
        }

        void AssignUnitButton_Click(object? sender, EventArgs e)
        {
            if (spawnZoneListBox?.SelectedItem is not SpawnZoneData zone)
            {
                MessageBox.Show("Select a spawn zone to assign the unit to.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (unitDataListBox?.SelectedItem is not UnitData unit)
            {
                MessageBox.Show("Select a unit to assign from the list on the left.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (assignmentMinimumSetter == null || assignmentMaximumSetter == null)
            {
                return;
            }

            int minimum = (int)assignmentMinimumSetter.Value;
            int maximum = (int)assignmentMaximumSetter.Value;

            if (minimum > maximum)
            {
                MessageBox.Show("Minimum spawns cannot be greater than the maximum.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SpawnZoneAssignment? assignment = zone.Assignments.FirstOrDefault(a => a.Unit == unit);

            if (assignment == null)
            {
                assignment = new SpawnZoneAssignment(unit, minimum, maximum);
                zone.Assignments.Add(assignment);
            }
            else
            {
                assignment.Minimum = minimum;
                assignment.Maximum = maximum;
            }

            assignmentBindingSource.ResetBindings(false);
            spawnZoneAssignmentsListBox!.SelectedItem = assignment;
        }

        void RemoveAssignmentButton_Click(object? sender, EventArgs e)
        {
            if (spawnZoneListBox?.SelectedItem is not SpawnZoneData zone ||
                spawnZoneAssignmentsListBox?.SelectedItem is not SpawnZoneAssignment assignment)
            {
                MessageBox.Show("Select an assigned unit to remove.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            assignment.Detach();
            zone.Assignments.Remove(assignment);
            assignmentBindingSource.ResetBindings(false);
            PopulateAssignmentFields(null);
        }

        void SpawnZoneAssignmentsListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateAssignmentFields(spawnZoneAssignmentsListBox?.SelectedItem as SpawnZoneAssignment);
        }

        void PopulateAssignmentFields(SpawnZoneAssignment? assignment)
        {
            if (assignmentMinimumSetter == null || assignmentMaximumSetter == null)
            {
                return;
            }

            if (assignment == null)
            {
                assignmentMinimumSetter.Value = assignmentMinimumSetter.Minimum;
                assignmentMaximumSetter.Value = assignmentMaximumSetter.Minimum;
                return;
            }

            decimal min = Math.Min(Math.Max(assignment.Minimum, (int)assignmentMinimumSetter.Minimum), (int)assignmentMinimumSetter.Maximum);
            decimal max = Math.Min(Math.Max(assignment.Maximum, (int)assignmentMaximumSetter.Minimum), (int)assignmentMaximumSetter.Maximum);

            assignmentMinimumSetter.Value = min;
            assignmentMaximumSetter.Value = max;

            if (unitDataListBox != null)
            {
                unitDataListBox.SelectedItem = assignment.Unit;
            }
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
