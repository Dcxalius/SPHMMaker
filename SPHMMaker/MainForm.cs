using System;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
        TabPage? spawnZoneTabPage;
        ListBox? unitDataListBox;
        Label? selectedUnitLabel;
        TextBox? unitNameInput;
        NumericUpDown? unitLevelSetter;
        TextBox? unitNotesInput;
        Button? createUnitButton;
        Button? updateUnitButton;
        Button? deleteUnitButton;
        ListBox? spawnZoneListBox;
        Label? selectedZoneLabel;
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

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private readonly BindingSource lootTableBinding = new();
        private readonly BindingSource lootEntryBinding = new();
        private readonly BindingSource classBindingSource = new();
        private readonly BindingSource unitBindingSource = new();
        private readonly BindingSource spawnZoneBindingSource = new();
        private readonly BindingSource assignmentBindingSource = new();
        private readonly BindingList<ClassData> classDefinitions = new();
        private readonly BindingList<UnitData> unitDefinitions = new();
        private readonly BindingList<SpawnZoneData> spawnZoneDefinitions = new();

        string? datapackSourcePath;
        string? datapackExtractionRoot;
        string? datapackRootPath;
        bool datapackLoadedFromArchive;

#if DEBUG
        private const string DebugDatapackDirectoryEnvironmentVariable = "SPHMMaker_DebugDatapackDirectory";
#endif


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

        private void loadDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            string debugDirectory = GetDebugDatapackDirectory();

            if (!Directory.Exists(debugDirectory))
            {
                MessageBox.Show($"The debug datapack directory '{debugDirectory}' does not exist.", "Load Datapack", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadDatapack(debugDirectory, isArchive: false);
#else
            var selection = PromptForDatapackLoad();
            if (selection.Path is null)
            {
                return;
            }

            LoadDatapack(selection.Path, selection.IsArchive);
#endif
        }

        private void saveDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            string debugDirectory = GetDebugDatapackDirectory();
            SaveDatapack(debugDirectory, asArchive: false);
#else
            var selection = PromptForDatapackSave();
            if (selection.Path is null)
            {
                MessageBox.Show("Save aborted.");
                return;
            }

            SaveDatapack(selection.Path, selection.IsArchive);
#endif
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
#if DEBUG
            return GetDebugDatapackDirectory();
#else
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
#endif
        }

#if DEBUG
        private static string GetDebugDatapackDirectory()
        {
            string? configuredPath = Environment.GetEnvironmentVariable(DebugDatapackDirectoryEnvironmentVariable);

            if (!string.IsNullOrWhiteSpace(configuredPath))
            {
                string expandedPath = Environment.ExpandEnvironmentVariables(configuredPath).Trim();
                if (!Directory.Exists(expandedPath))
                {
                    throw new DirectoryNotFoundException($"The path configured in the '{DebugDatapackDirectoryEnvironmentVariable}' environment variable does not exist: '{expandedPath}'.");
                }

                return Path.GetFullPath(expandedPath);
            }

            DirectoryInfo? directory = new(AppDomain.CurrentDomain.BaseDirectory);

            while (directory is not null)
            {
                string candidate = Path.Combine(directory.FullName, "docs", "datapack");

                if (Directory.Exists(candidate))
                {
                    return Path.GetFullPath(candidate);
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException($"Unable to determine the debug datapack directory. Set the '{DebugDatapackDirectoryEnvironmentVariable}' environment variable to a valid path.");
        }
#endif

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
                SpawnZoneAssignment? currentSelection = spawnZoneAssignmentsListBox.SelectedItem as SpawnZoneAssignment;

                assignmentBindingSource.DataSource = zone.Assignments;
                spawnZoneAssignmentsListBox.DataSource = assignmentBindingSource;
                spawnZoneAssignmentsListBox.DisplayMember = nameof(SpawnZoneAssignment.DisplayText);
                assignmentBindingSource.ResetBindings(false);

                if (currentSelection != null && zone.Assignments.Contains(currentSelection))
                {
                    spawnZoneAssignmentsListBox.SelectedItem = currentSelection;
                    PopulateAssignmentFields(currentSelection);
                }
                else
                {
                    spawnZoneAssignmentsListBox.ClearSelected();
                    PopulateAssignmentFields(null);
                }
            }
            else
            {
                assignmentBindingSource.DataSource = null;
                spawnZoneAssignmentsListBox.DataSource = null;
                spawnZoneAssignmentsListBox.ClearSelected();
                PopulateAssignmentFields(null);
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

        private void markdownEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = new MarkdownEditorForm();
            editor.Show(this);
        }

    }
}
