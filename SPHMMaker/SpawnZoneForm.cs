using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SPHMMaker.SpawnZones;

namespace SPHMMaker
{
    public partial class MainForm
    {
        private TabPage? spawnZoneTabPage;
        private ListBox? unitDataListBox;
        private Label? selectedUnitLabel;
        private TextBox? unitNameInput;
        private NumericUpDown? unitLevelSetter;
        private TextBox? unitNotesInput;
        private Button? createUnitButton;
        private Button? updateUnitButton;
        private Button? deleteUnitButton;
        private ListBox? spawnZoneListBox;
        private Label? selectedZoneLabel;
        private TextBox? spawnZoneNameInput;
        private TextBox? spawnZoneNotesInput;
        private Button? createSpawnZoneButton;
        private Button? updateSpawnZoneButton;
        private Button? deleteSpawnZoneButton;
        private ListBox? spawnZoneAssignmentsListBox;
        private NumericUpDown? assignmentMinimumSetter;
        private NumericUpDown? assignmentMaximumSetter;
        private Button? assignUnitButton;
        private Button? removeAssignmentButton;

        private readonly BindingSource unitBindingSource = new();
        private readonly BindingSource spawnZoneBindingSource = new();
        private readonly BindingSource assignmentBindingSource = new();
        private readonly BindingList<UnitData> unitDefinitions = new();
        private readonly BindingList<SpawnZoneData> spawnZoneDefinitions = new();

        private void InitializeSpawnZoneTab()
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
            unitLayout.Controls.Add(unitButtonPanel, 0, 6);
            unitLayout.SetColumnSpan(unitButtonPanel, 2);

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
                Text = "Delete Unit"
            };
            deleteUnitButton.Click += DeleteUnitButton_Click;
            unitButtonPanel.Controls.Add(deleteUnitButton);

            var spawnZoneGroup = new GroupBox
            {
                Text = "Spawn Zone",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(spawnZoneGroup, 1, 0);

            var spawnLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6
            };
            spawnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            spawnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            spawnLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

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
            spawnLayout.Controls.Add(spawnZoneButtonPanel, 0, 5);
            spawnLayout.SetColumnSpan(spawnZoneButtonPanel, 2);

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
                Text = "Delete Zone"
            };
            deleteSpawnZoneButton.Click += DeleteSpawnZoneButton_Click;
            spawnZoneButtonPanel.Controls.Add(deleteSpawnZoneButton);

            var assignmentGroup = new GroupBox
            {
                Text = "Assignments",
                Dock = DockStyle.Fill
            };

            layout.Controls.Add(assignmentGroup, 1, 0);

            var assignmentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6
            };
            assignmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            assignmentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            assignmentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
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

            var assignmentMinimumLabel = new Label
            {
                Text = "Minimum Spawns",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(assignmentMinimumLabel, 0, 1);

            assignmentMinimumSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100
            };
            assignmentLayout.Controls.Add(assignmentMinimumSetter, 1, 1);

            var assignmentMaximumLabel = new Label
            {
                Text = "Maximum Spawns",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(assignmentMaximumLabel, 0, 2);

            assignmentMaximumSetter = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100
            };
            assignmentLayout.Controls.Add(assignmentMaximumSetter, 1, 2);

            assignUnitButton = new Button
            {
                Text = "Assign Unit"
            };
            assignUnitButton.Click += AssignUnitButton_Click;
            assignmentLayout.Controls.Add(assignUnitButton, 0, 3);

            removeAssignmentButton = new Button
            {
                Text = "Remove Assignment"
            };
            removeAssignmentButton.Click += RemoveAssignmentButton_Click;
            assignmentLayout.Controls.Add(removeAssignmentButton, 1, 3);

            var assignmentHintLabel = new Label
            {
                Text = "Assignments automatically update when the unit data changes.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            assignmentLayout.Controls.Add(assignmentHintLabel, 0, 5);
            assignmentLayout.SetColumnSpan(assignmentHintLabel, 2);
        }

        private void InitializeSpawnZoneDataBindings()
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

            PopulateUnitFields(unitDataListBox?.SelectedItem as UnitData);
            PopulateSpawnZoneFields(spawnZoneListBox?.SelectedItem as SpawnZoneData);
            PopulateAssignmentFields(spawnZoneAssignmentsListBox?.SelectedItem as SpawnZoneAssignment);
        }

        private void UnitDataListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateUnitFields(unitDataListBox?.SelectedItem as UnitData);
        }

        private void PopulateUnitFields(UnitData? unit)
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

        private void UpdateSelectedUnitLabel()
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

        private void CreateUnitButton_Click(object? sender, EventArgs e)
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

        private void UpdateUnitButton_Click(object? sender, EventArgs e)
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

        private void DeleteUnitButton_Click(object? sender, EventArgs e)
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

        private void RemoveUnitAssignments(UnitData unit)
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

        private void UpdateAssignmentDisplayForUnit(UnitData unit)
        {
            foreach (SpawnZoneData zone in spawnZoneDefinitions)
            {
                foreach (SpawnZoneAssignment assignment in zone.Assignments.Where(a => a.Unit == unit))
                {
                    assignment.NotifyUnitUpdated();
                }
            }
        }

        private void SpawnZoneListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateSpawnZoneFields(spawnZoneListBox?.SelectedItem as SpawnZoneData);
        }

        private void PopulateSpawnZoneFields(SpawnZoneData? zone)
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

        private void CreateSpawnZoneButton_Click(object? sender, EventArgs e)
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

        private void UpdateSpawnZoneButton_Click(object? sender, EventArgs e)
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

        private void DeleteSpawnZoneButton_Click(object? sender, EventArgs e)
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

        private void DetachAssignments(SpawnZoneData zone)
        {
            foreach (SpawnZoneAssignment assignment in zone.Assignments.ToList())
            {
                assignment.Detach();
            }

            zone.Assignments.Clear();
        }

        private void UpdateAssignmentBinding()
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

            PopulateAssignmentFields(spawnZoneAssignmentsListBox?.SelectedItem as SpawnZoneAssignment);
        }

        private void AssignUnitButton_Click(object? sender, EventArgs e)
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

        private void RemoveAssignmentButton_Click(object? sender, EventArgs e)
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

        private void SpawnZoneAssignmentsListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateAssignmentFields(spawnZoneAssignmentsListBox?.SelectedItem as SpawnZoneAssignment);
        }

        private void PopulateAssignmentFields(SpawnZoneAssignment? assignment)
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
    }
}

