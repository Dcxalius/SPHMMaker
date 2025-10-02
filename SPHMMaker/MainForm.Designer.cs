using System.Windows.Forms.DataVisualization.Charting;
using SPHMMaker.Items;

namespace SPHMMaker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                DisposeItemImages();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();

            itemNameInput = new TextBox();
            items = new ListBox();
            itemNameLabel = new Label();
            itemSelectBoxLabel = new Label();
            MainTab = new TabControl();
            lootTabPage = new TabPage();
            lootDistributionChart = new Chart();
            lootChartHintLabel = new Label();
            lootKillsCounter = new NumericUpDown();
            lootKillsLabel = new Label();
            lootRemoveEntryButton = new Button();
            lootAddEntryButton = new Button();
            lootEntriesGrid = new DataGridView();
            lootDeleteTableButton = new Button();
            lootSaveTableButton = new Button();
            lootAddTableButton = new Button();
            lootTableIdTextBox = new TextBox();
            lootTableIdLabel = new Label();
            lootTablesListBox = new ListBox();
            ItemPageTab = new TabPage();
            TilesPageTab = new TabPage();
            tileDetailsGroup = new GroupBox();
            tileResetButton = new Button();
            tileSaveButton = new Button();
            tileCreateButton = new Button();
            tileNotesInput = new RichTextBox();
            tileNotesLabel = new Label();
            tileMovementCostInput = new NumericUpDown();
            tileMovementCostLabel = new Label();
            tileWalkableCheckbox = new CheckBox();
            tileTextureInput = new TextBox();
            tileTextureLabel = new Label();
            tileNameInput = new TextBox();
            tileNameLabel = new Label();
            tileIdInput = new NumericUpDown();
            tileIdLabel = new Label();
            tileListLabel = new Label();
            tileList = new ListBox();
            OverrideItemButton = new Button();
            EditItemButton = new Button();
            itemWindowTabControl = new TabControl();
            createItemPage = new TabPage();
            itemCheckGeneratedTooltip = new Button();
            goldCostCounter = new NumericUpDown();
            goldCostLabel = new Label();
            itemToCreate = new TabControl();
            itemTypeTab = new TabPage();
            itemTypeTabControl = new TabControl();
            itemTypeNoneTab = new TabPage();
            nonePageLabel = new Label();
            itemTypeBagTab = new TabPage();
            itemBagSizeLabel = new Label();
            itemBagSizeSetter = new NumericUpDown();
            itemTypeConsumableTab = new TabPage();
            itemConsumableTabController = new TabControl();
            itemConsumablePotionPage = new TabPage();
            itemPotionMaximumLabel = new Label();
            itemPotionMinimumLabel = new Label();
            itemPotionValueFlowLayout = new FlowLayoutPanel();
            itemPotionHealthPanel = new Panel();
            itemPotionHealthLabel = new Label();
            itemPotionHealthMaximumSetter = new NumericUpDown();
            itemPotionHealthMinimumSetter = new NumericUpDown();
            itemPotionManaPanel = new Panel();
            itemPotionManaMaximumSetter = new NumericUpDown();
            itemPotionManaLabel = new Label();
            itemPotionManaMinimumSetter = new NumericUpDown();
            itemPotionEnergyPanel = new Panel();
            itemPotionEnergyLabel = new Label();
            itemPotionEnergyMinimumSetter = new NumericUpDown();
            itemPotionEnergyMaximumSetter = new NumericUpDown();
            itemPotionTypeSetter = new CheckedListBox();
            itemConsumableFoodPage = new TabPage();
            itemTypeEquipmentTab = new TabPage();
            itemEquipmentMaterialSetter = new ComboBox();
            itemEquipmentMaterialLabel = new Label();
            itemEquipmentTypeLabel = new Label();
            itemTypeEquipmentTypeSetter = new ComboBox();
            itemStats = new Panel();
            itemStatsArmorSetter = new NumericUpDown();
            itemStatsArmorLabel = new Label();
            itemStatsSpiritLabel = new Label();
            itemStatsIntelligenceLabel = new Label();
            itemStatsStaminaLabel = new Label();
            itemStatsStrengthLabel = new Label();
            itemStatsAgilityLabel = new Label();
            itemStatsSpiritSetter = new NumericUpDown();
            itemStatsIntelligenceSetter = new NumericUpDown();
            itemStatsStaminaSetter = new NumericUpDown();
            itemStatsStrengthSetter = new NumericUpDown();
            itemStatsAgilitySetter = new NumericUpDown();
            itemTypeWeaponTab = new TabPage();
            itemDummyDpsChangingLabel = new Label();
            itemDummyDpsLabel = new Label();
            itemAttackSpeedLabel = new Label();
            itemMaximumDamageLabel = new Label();
            itemMinimumDamageLabel = new Label();
            itemAttackSpeedSetter = new NumericUpDown();
            itemMaximumDamageSetter = new NumericUpDown();
            itemMinimumDamageSetter = new NumericUpDown();
            itemWeaponEQTypeLabel = new Label();
            itemWeaponEQTypeSetter = new ComboBox();
            itemWeaponTypeSetter = new ComboBox();
            itemWeaponTypeLabel = new Label();
            itemQualitySelector = new ExtendedForm.ExtendedCheckedListBox();
            itemTypeSelector = new ExtendedForm.ExtendedCheckedListBox();
            descriptionTab = new TabPage();
            itemDescriptionInput = new RichTextBox();
            itemMaxCountSetter = new NumericUpDown();
            itemCountLabel = new Label();
            createItemButton = new Button();
            itemEffectTab = new TabPage();
            label2 = new Label();
            label1 = new Label();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            otherPage = new TabPage();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveDatapackToolStripMenuItem = new ToolStripMenuItem();
            loadDatapackToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            fileDownloadInstructionsToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            spriteEditorToolStripMenuItem = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)lootDistributionChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lootKillsCounter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lootEntriesGrid).BeginInit();
            MainTab.SuspendLayout();
            lootTabPage.SuspendLayout();
            ItemPageTab.SuspendLayout();
            TilesPageTab.SuspendLayout();
            tileDetailsGroup.SuspendLayout();
            itemWindowTabControl.SuspendLayout();
            createItemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)goldCostCounter).BeginInit();
            itemToCreate.SuspendLayout();
            itemTypeTab.SuspendLayout();
            itemTypeTabControl.SuspendLayout();
            itemTypeNoneTab.SuspendLayout();
            itemTypeBagTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemBagSizeSetter).BeginInit();
            itemTypeConsumableTab.SuspendLayout();
            itemConsumableTabController.SuspendLayout();
            itemConsumablePotionPage.SuspendLayout();
            itemPotionValueFlowLayout.SuspendLayout();
            itemPotionHealthPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionHealthMaximumSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionHealthMinimumSetter).BeginInit();
            itemPotionManaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionManaMaximumSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionManaMinimumSetter).BeginInit();
            itemPotionEnergyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionEnergyMinimumSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionEnergyMaximumSetter).BeginInit();
            itemTypeEquipmentTab.SuspendLayout();
            itemStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemStatsArmorSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsSpiritSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsIntelligenceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStaminaSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStrengthSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsAgilitySetter).BeginInit();
            itemTypeWeaponTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemAttackSpeedSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemMaximumDamageSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemMinimumDamageSetter).BeginInit();
            descriptionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).BeginInit();
            itemEffectTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tileMovementCostInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileIdInput).BeginInit();

            lootTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lootEntriesGrid).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // itemNameInput
            // 
            itemNameInput.Location = new Point(81, 8);
            itemNameInput.Name = "itemNameInput";
            itemNameInput.Size = new Size(192, 23);
            itemNameInput.TabIndex = 0;
            // 
            // items
            // 
            items.DrawMode = DrawMode.OwnerDrawFixed;
            items.FormattingEnabled = true;
            items.IntegralHeight = false;
            items.ItemHeight = 56;
            items.Location = new Point(582, 30);
            items.Name = "items";
            items.Size = new Size(202, 364);
            items.TabIndex = 1;
            items.DrawItem += items_DrawItem;
            items.MouseDoubleClick += items_MouseDoubleClick;
            // 
            // itemNameLabel
            // 
            itemNameLabel.AutoSize = true;
            itemNameLabel.Location = new Point(6, 11);
            itemNameLabel.Name = "itemNameLabel";
            itemNameLabel.Size = new Size(69, 15);
            itemNameLabel.TabIndex = 2;
            itemNameLabel.Text = "Item Name:";
            // 
            // itemSelectBoxLabel
            // 
            itemSelectBoxLabel.AutoSize = true;
            itemSelectBoxLabel.Location = new Point(582, 12);
            itemSelectBoxLabel.Name = "itemSelectBoxLabel";
            itemSelectBoxLabel.Size = new Size(39, 15);
            itemSelectBoxLabel.TabIndex = 3;
            itemSelectBoxLabel.Text = "Items:";
            // 
            // MainTab
            // 
            MainTab.Controls.Add(ItemPageTab);
            MainTab.Controls.Add(TilesPageTab);
            MainTab.Controls.Add(lootTabPage);
            MainTab.Controls.Add(otherPage);
            MainTab.Location = new Point(0, 27);
            MainTab.Name = "MainTab";
            MainTab.SelectedIndex = 0;
            MainTab.Size = new Size(800, 511);
            MainTab.TabIndex = 4;
            //
            // lootTabPage
            //
            lootTabPage.Controls.Add(lootDistributionChart);
            lootTabPage.Controls.Add(lootChartHintLabel);
            lootTabPage.Controls.Add(lootKillsCounter);
            lootTabPage.Controls.Add(lootKillsLabel);
            lootTabPage.Controls.Add(lootRemoveEntryButton);
            lootTabPage.Controls.Add(lootAddEntryButton);
            lootTabPage.Controls.Add(lootEntriesGrid);
            lootTabPage.Controls.Add(lootDeleteTableButton);
            lootTabPage.Controls.Add(lootSaveTableButton);
            lootTabPage.Controls.Add(lootAddTableButton);
            lootTabPage.Controls.Add(lootTableIdTextBox);
            lootTabPage.Controls.Add(lootTableIdLabel);
            lootTabPage.Controls.Add(lootTablesListBox);
            lootTabPage.Location = new Point(4, 24);
            lootTabPage.Name = "lootTabPage";
            lootTabPage.Padding = new Padding(3);
            lootTabPage.Size = new Size(792, 483);
            lootTabPage.TabIndex = 1;
            lootTabPage.Text = "Loot";
            lootTabPage.UseVisualStyleBackColor = true;
            //
            // lootTablesListBox
            //
            lootTablesListBox.FormattingEnabled = true;
            lootTablesListBox.ItemHeight = 15;
            lootTablesListBox.Location = new Point(6, 122);
            lootTablesListBox.Name = "lootTablesListBox";
            lootTablesListBox.Size = new Size(188, 319);
            lootTablesListBox.TabIndex = 5;
            //
            // lootTableIdLabel
            //
            lootTableIdLabel.AutoSize = true;
            lootTableIdLabel.Location = new Point(6, 8);
            lootTableIdLabel.Name = "lootTableIdLabel";
            lootTableIdLabel.Size = new Size(85, 15);
            lootTableIdLabel.TabIndex = 0;
            lootTableIdLabel.Text = "Loot Table ID:";
            //
            // lootTableIdTextBox
            //
            lootTableIdTextBox.Location = new Point(6, 26);
            lootTableIdTextBox.Name = "lootTableIdTextBox";
            lootTableIdTextBox.Size = new Size(188, 23);
            lootTableIdTextBox.TabIndex = 1;
            //
            // lootAddTableButton
            //
            lootAddTableButton.Location = new Point(6, 55);
            lootAddTableButton.Name = "lootAddTableButton";
            lootAddTableButton.Size = new Size(92, 27);
            lootAddTableButton.TabIndex = 2;
            lootAddTableButton.Text = "New Table";
            lootAddTableButton.UseVisualStyleBackColor = true;
            //
            // lootSaveTableButton
            //
            lootSaveTableButton.Location = new Point(102, 55);
            lootSaveTableButton.Name = "lootSaveTableButton";
            lootSaveTableButton.Size = new Size(92, 27);
            lootSaveTableButton.TabIndex = 3;
            lootSaveTableButton.Text = "Save ID";
            lootSaveTableButton.UseVisualStyleBackColor = true;
            //
            // lootDeleteTableButton
            //
            lootDeleteTableButton.Location = new Point(6, 88);
            lootDeleteTableButton.Name = "lootDeleteTableButton";
            lootDeleteTableButton.Size = new Size(188, 27);
            lootDeleteTableButton.TabIndex = 4;
            lootDeleteTableButton.Text = "Delete Table";
            lootDeleteTableButton.UseVisualStyleBackColor = true;
            //
            // lootEntriesGrid
            //
            lootEntriesGrid.AllowUserToAddRows = false;
            lootEntriesGrid.AllowUserToDeleteRows = false;
            lootEntriesGrid.AllowUserToResizeRows = false;
            lootEntriesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            lootEntriesGrid.Location = new Point(208, 26);
            lootEntriesGrid.MultiSelect = false;
            lootEntriesGrid.Name = "lootEntriesGrid";
            lootEntriesGrid.RowHeadersVisible = false;
            lootEntriesGrid.RowTemplate.Height = 25;
            lootEntriesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lootEntriesGrid.Size = new Size(280, 250);
            lootEntriesGrid.TabIndex = 6;
            //
            // lootAddEntryButton
            //
            lootAddEntryButton.Location = new Point(208, 282);
            lootAddEntryButton.Name = "lootAddEntryButton";
            lootAddEntryButton.Size = new Size(130, 27);
            lootAddEntryButton.TabIndex = 7;
            lootAddEntryButton.Text = "Add Entry";
            lootAddEntryButton.UseVisualStyleBackColor = true;
            //
            // lootRemoveEntryButton
            //
            lootRemoveEntryButton.Location = new Point(358, 282);
            lootRemoveEntryButton.Name = "lootRemoveEntryButton";
            lootRemoveEntryButton.Size = new Size(130, 27);
            lootRemoveEntryButton.TabIndex = 8;
            lootRemoveEntryButton.Text = "Remove Entry";
            lootRemoveEntryButton.UseVisualStyleBackColor = true;
            //
            // lootKillsLabel
            //
            lootKillsLabel.AutoSize = true;
            lootKillsLabel.Location = new Point(208, 318);
            lootKillsLabel.Name = "lootKillsLabel";
            lootKillsLabel.Size = new Size(118, 15);
            lootKillsLabel.TabIndex = 9;
            lootKillsLabel.Text = "Kills for distribution:";
            //
            // lootKillsCounter
            //
            lootKillsCounter.Location = new Point(332, 316);
            lootKillsCounter.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lootKillsCounter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            lootKillsCounter.Name = "lootKillsCounter";
            lootKillsCounter.Size = new Size(80, 23);
            lootKillsCounter.TabIndex = 10;
            lootKillsCounter.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // lootChartHintLabel
            //
            lootChartHintLabel.AutoSize = true;
            lootChartHintLabel.Location = new Point(506, 26);
            lootChartHintLabel.Name = "lootChartHintLabel";
            lootChartHintLabel.Size = new Size(238, 15);
            lootChartHintLabel.TabIndex = 11;
            lootChartHintLabel.Text = "Select an entry to see drop distribution.";
            //
            // lootDistributionChart
            //
            chartArea1.Name = "ChartArea1";
            lootDistributionChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            lootDistributionChart.Legends.Add(legend1);
            lootDistributionChart.Location = new Point(506, 56);
            lootDistributionChart.Name = "lootDistributionChart";
            lootDistributionChart.Size = new Size(280, 345);
            lootDistributionChart.TabIndex = 12;
            lootDistributionChart.Text = "lootDistributionChart";
            //
            // ItemPageTab
            //
            ItemPageTab.Controls.Add(OverrideItemButton);
            ItemPageTab.Controls.Add(EditItemButton);
            ItemPageTab.Controls.Add(itemWindowTabControl);
            ItemPageTab.Controls.Add(itemSelectBoxLabel);
            ItemPageTab.Controls.Add(items);
            ItemPageTab.Location = new Point(4, 24);
            ItemPageTab.Name = "ItemPageTab";
            ItemPageTab.Padding = new Padding(3);
            ItemPageTab.Size = new Size(792, 483);
            ItemPageTab.TabIndex = 0;
            ItemPageTab.Text = "Items";
            ItemPageTab.UseVisualStyleBackColor = true;
            //
            // TilesPageTab
            //
            TilesPageTab.Controls.Add(tileDetailsGroup);
            TilesPageTab.Controls.Add(tileListLabel);
            TilesPageTab.Controls.Add(tileList);
            TilesPageTab.Location = new Point(4, 24);
            TilesPageTab.Name = "TilesPageTab";
            TilesPageTab.Padding = new Padding(3);
            TilesPageTab.Size = new Size(792, 483);
            TilesPageTab.TabIndex = 1;
            TilesPageTab.Text = "Tiles";
            TilesPageTab.UseVisualStyleBackColor = true;
            //
            // tileDetailsGroup
            //
            tileDetailsGroup.Controls.Add(tileResetButton);
            tileDetailsGroup.Controls.Add(tileSaveButton);
            tileDetailsGroup.Controls.Add(tileCreateButton);
            tileDetailsGroup.Controls.Add(tileNotesInput);
            tileDetailsGroup.Controls.Add(tileNotesLabel);
            tileDetailsGroup.Controls.Add(tileMovementCostInput);
            tileDetailsGroup.Controls.Add(tileMovementCostLabel);
            tileDetailsGroup.Controls.Add(tileWalkableCheckbox);
            tileDetailsGroup.Controls.Add(tileTextureInput);
            tileDetailsGroup.Controls.Add(tileTextureLabel);
            tileDetailsGroup.Controls.Add(tileNameInput);
            tileDetailsGroup.Controls.Add(tileNameLabel);
            tileDetailsGroup.Controls.Add(tileIdInput);
            tileDetailsGroup.Controls.Add(tileIdLabel);
            tileDetailsGroup.Location = new Point(6, 6);
            tileDetailsGroup.Name = "tileDetailsGroup";
            tileDetailsGroup.Size = new Size(570, 471);
            tileDetailsGroup.TabIndex = 0;
            tileDetailsGroup.TabStop = false;
            tileDetailsGroup.Text = "Tile Details";
            //
            // tileResetButton
            //
            tileResetButton.Location = new Point(352, 370);
            tileResetButton.Name = "tileResetButton";
            tileResetButton.Size = new Size(110, 27);
            tileResetButton.TabIndex = 13;
            tileResetButton.Text = "Reset";
            tileResetButton.UseVisualStyleBackColor = true;
            tileResetButton.Click += tileResetButton_Click;
            //
            // tileSaveButton
            //
            tileSaveButton.Location = new Point(236, 370);
            tileSaveButton.Name = "tileSaveButton";
            tileSaveButton.Size = new Size(110, 27);
            tileSaveButton.TabIndex = 12;
            tileSaveButton.Text = "Save Changes";
            tileSaveButton.UseVisualStyleBackColor = true;
            tileSaveButton.Click += tileSaveButton_Click;
            //
            // tileCreateButton
            //
            tileCreateButton.Location = new Point(120, 370);
            tileCreateButton.Name = "tileCreateButton";
            tileCreateButton.Size = new Size(110, 27);
            tileCreateButton.TabIndex = 11;
            tileCreateButton.Text = "Create Tile";
            tileCreateButton.UseVisualStyleBackColor = true;
            tileCreateButton.Click += tileCreateButton_Click;
            //
            // tileNotesInput
            //
            tileNotesInput.Location = new Point(120, 181);
            tileNotesInput.Name = "tileNotesInput";
            tileNotesInput.Size = new Size(414, 168);
            tileNotesInput.TabIndex = 10;
            tileNotesInput.Text = "";
            //
            // tileNotesLabel
            //
            tileNotesLabel.AutoSize = true;
            tileNotesLabel.Location = new Point(12, 181);
            tileNotesLabel.Name = "tileNotesLabel";
            tileNotesLabel.Size = new Size(41, 15);
            tileNotesLabel.TabIndex = 9;
            tileNotesLabel.Text = "Notes:";
            //
            // tileMovementCostInput
            //
            tileMovementCostInput.Location = new Point(120, 146);
            tileMovementCostInput.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            tileMovementCostInput.Name = "tileMovementCostInput";
            tileMovementCostInput.Size = new Size(120, 23);
            tileMovementCostInput.TabIndex = 8;
            tileMovementCostInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // tileMovementCostLabel
            //
            tileMovementCostLabel.AutoSize = true;
            tileMovementCostLabel.Location = new Point(12, 150);
            tileMovementCostLabel.Name = "tileMovementCostLabel";
            tileMovementCostLabel.Size = new Size(93, 15);
            tileMovementCostLabel.TabIndex = 7;
            tileMovementCostLabel.Text = "Movement cost:";
            //
            // tileWalkableCheckbox
            //
            tileWalkableCheckbox.AutoSize = true;
            tileWalkableCheckbox.Location = new Point(120, 118);
            tileWalkableCheckbox.Name = "tileWalkableCheckbox";
            tileWalkableCheckbox.Size = new Size(76, 19);
            tileWalkableCheckbox.TabIndex = 6;
            tileWalkableCheckbox.Text = "Walkable";
            tileWalkableCheckbox.UseVisualStyleBackColor = true;
            //
            // tileTextureInput
            //
            tileTextureInput.Location = new Point(120, 87);
            tileTextureInput.Name = "tileTextureInput";
            tileTextureInput.Size = new Size(250, 23);
            tileTextureInput.TabIndex = 5;
            //
            // tileTextureLabel
            //
            tileTextureLabel.AutoSize = true;
            tileTextureLabel.Location = new Point(12, 90);
            tileTextureLabel.Name = "tileTextureLabel";
            tileTextureLabel.Size = new Size(48, 15);
            tileTextureLabel.TabIndex = 4;
            tileTextureLabel.Text = "Texture:";
            //
            // tileNameInput
            //
            tileNameInput.Location = new Point(120, 56);
            tileNameInput.Name = "tileNameInput";
            tileNameInput.Size = new Size(250, 23);
            tileNameInput.TabIndex = 3;
            //
            // tileNameLabel
            //
            tileNameLabel.AutoSize = true;
            tileNameLabel.Location = new Point(12, 59);
            tileNameLabel.Name = "tileNameLabel";
            tileNameLabel.Size = new Size(42, 15);
            tileNameLabel.TabIndex = 2;
            tileNameLabel.Text = "Name:";
            //
            // tileIdInput
            //
            tileIdInput.Location = new Point(120, 24);
            tileIdInput.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            tileIdInput.Name = "tileIdInput";
            tileIdInput.Size = new Size(120, 23);
            tileIdInput.TabIndex = 1;
            //
            // tileIdLabel
            //
            tileIdLabel.AutoSize = true;
            tileIdLabel.Location = new Point(12, 28);
            tileIdLabel.Name = "tileIdLabel";
            tileIdLabel.Size = new Size(22, 15);
            tileIdLabel.TabIndex = 0;
            tileIdLabel.Text = "ID:";
            //
            // tileListLabel
            //
            tileListLabel.AutoSize = true;
            tileListLabel.Location = new Point(582, 12);
            tileListLabel.Name = "tileListLabel";
            tileListLabel.Size = new Size(35, 15);
            tileListLabel.TabIndex = 1;
            tileListLabel.Text = "Tiles:";
            //
            // tileList
            //
            tileList.FormattingEnabled = true;
            tileList.ItemHeight = 15;
            tileList.Location = new Point(582, 30);
            tileList.Name = "tileList";
            tileList.Size = new Size(202, 364);
            tileList.TabIndex = 2;
            //
            // OverrideItemButton
            //
            OverrideItemButton.Location = new Point(684, 395);
            OverrideItemButton.Name = "OverrideItemButton";
            OverrideItemButton.Size = new Size(100, 24);
            OverrideItemButton.TabIndex = 10;
            OverrideItemButton.Text = "Override Item";
            OverrideItemButton.UseVisualStyleBackColor = true;
            OverrideItemButton.Click += OverrideItemButton_Click;
            OverrideItemButton.MouseHover += OverrideItemButton_MouseHover;
            // 
            // EditItemButton
            // 
            EditItemButton.Location = new Point(582, 395);
            EditItemButton.Name = "EditItemButton";
            EditItemButton.Size = new Size(100, 24);
            EditItemButton.TabIndex = 9;
            EditItemButton.Text = "Edit Item";
            EditItemButton.UseVisualStyleBackColor = true;
            EditItemButton.Click += EditItemButton_Click;
            // 
            // itemWindowTabControl
            // 
            itemWindowTabControl.Controls.Add(createItemPage);
            itemWindowTabControl.Controls.Add(itemEffectTab);
            itemWindowTabControl.Location = new Point(6, 6);
            itemWindowTabControl.Name = "itemWindowTabControl";
            itemWindowTabControl.SelectedIndex = 0;
            itemWindowTabControl.Size = new Size(570, 481);
            itemWindowTabControl.TabIndex = 4;
            // 
            // createItemPage
            // 
            createItemPage.Controls.Add(itemCheckGeneratedTooltip);
            createItemPage.Controls.Add(goldCostCounter);
            createItemPage.Controls.Add(goldCostLabel);
            createItemPage.Controls.Add(itemToCreate);
            createItemPage.Controls.Add(itemMaxCountSetter);
            createItemPage.Controls.Add(itemCountLabel);
            createItemPage.Controls.Add(createItemButton);
            createItemPage.Controls.Add(itemNameInput);
            createItemPage.Controls.Add(itemNameLabel);
            createItemPage.Location = new Point(4, 24);
            createItemPage.Name = "createItemPage";
            createItemPage.Padding = new Padding(3);
            createItemPage.Size = new Size(562, 453);
            createItemPage.TabIndex = 0;
            createItemPage.Text = "Create Item";
            createItemPage.UseVisualStyleBackColor = true;
            // 
            // itemCheckGeneratedTooltip
            // 
            itemCheckGeneratedTooltip.Location = new Point(485, 353);
            itemCheckGeneratedTooltip.Name = "itemCheckGeneratedTooltip";
            itemCheckGeneratedTooltip.Size = new Size(71, 27);
            itemCheckGeneratedTooltip.TabIndex = 9;
            itemCheckGeneratedTooltip.Text = "Tooltip";
            itemCheckGeneratedTooltip.UseVisualStyleBackColor = true;
            itemCheckGeneratedTooltip.Click += itemCheckGeneratedTooltip_Click;
            // 
            // goldCostCounter
            // 
            goldCostCounter.Location = new Point(458, 9);
            goldCostCounter.Name = "goldCostCounter";
            goldCostCounter.Size = new Size(65, 23);
            goldCostCounter.TabIndex = 8;
            goldCostCounter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // goldCostLabel
            // 
            goldCostLabel.AutoSize = true;
            goldCostLabel.Location = new Point(400, 11);
            goldCostLabel.Name = "goldCostLabel";
            goldCostLabel.Size = new Size(60, 15);
            goldCostLabel.TabIndex = 7;
            goldCostLabel.Text = "Gold cost:";
            // 
            // itemToCreate
            // 
            itemToCreate.Controls.Add(itemTypeTab);
            itemToCreate.Controls.Add(descriptionTab);
            itemToCreate.Location = new Point(6, 38);
            itemToCreate.Name = "itemToCreate";
            itemToCreate.SelectedIndex = 0;
            itemToCreate.Size = new Size(467, 380);
            itemToCreate.TabIndex = 6;
            // 
            // itemTypeTab
            // 
            itemTypeTab.Controls.Add(itemTypeTabControl);
            itemTypeTab.Controls.Add(itemQualitySelector);
            itemTypeTab.Controls.Add(itemTypeSelector);
            itemTypeTab.Location = new Point(4, 24);
            itemTypeTab.Name = "itemTypeTab";
            itemTypeTab.Padding = new Padding(3);
            itemTypeTab.Size = new Size(459, 352);
            itemTypeTab.TabIndex = 0;
            itemTypeTab.Text = "Item Type";
            itemTypeTab.UseVisualStyleBackColor = true;
            // 
            // itemTypeTabControl
            // 
            itemTypeTabControl.Appearance = TabAppearance.FlatButtons;
            itemTypeTabControl.Controls.Add(itemTypeNoneTab);
            itemTypeTabControl.Controls.Add(itemTypeBagTab);
            itemTypeTabControl.Controls.Add(itemTypeConsumableTab);
            itemTypeTabControl.Controls.Add(itemTypeEquipmentTab);
            itemTypeTabControl.Controls.Add(itemTypeWeaponTab);
            itemTypeTabControl.ItemSize = new Size(30, 20);
            itemTypeTabControl.Location = new Point(91, 0);
            itemTypeTabControl.Name = "itemTypeTabControl";
            itemTypeTabControl.SelectedIndex = 0;
            itemTypeTabControl.Size = new Size(366, 318);
            itemTypeTabControl.SizeMode = TabSizeMode.Fixed;
            itemTypeTabControl.TabIndex = 2;
            itemTypeTabControl.TabStop = false;
            // 
            // itemTypeNoneTab
            // 
            itemTypeNoneTab.BackColor = Color.Transparent;
            itemTypeNoneTab.Controls.Add(nonePageLabel);
            itemTypeNoneTab.Location = new Point(4, 24);
            itemTypeNoneTab.Name = "itemTypeNoneTab";
            itemTypeNoneTab.Padding = new Padding(3);
            itemTypeNoneTab.Size = new Size(358, 290);
            itemTypeNoneTab.TabIndex = 0;
            itemTypeNoneTab.Text = "tabPage1";
            // 
            // nonePageLabel
            // 
            nonePageLabel.AutoSize = true;
            nonePageLabel.Location = new Point(20, 20);
            nonePageLabel.Name = "nonePageLabel";
            nonePageLabel.Size = new Size(87, 15);
            nonePageLabel.TabIndex = 0;
            nonePageLabel.Text = "Nothing here :)";
            // 
            // itemTypeBagTab
            // 
            itemTypeBagTab.BackColor = Color.Transparent;
            itemTypeBagTab.Controls.Add(itemBagSizeLabel);
            itemTypeBagTab.Controls.Add(itemBagSizeSetter);
            itemTypeBagTab.Location = new Point(4, 24);
            itemTypeBagTab.Name = "itemTypeBagTab";
            itemTypeBagTab.Padding = new Padding(3);
            itemTypeBagTab.Size = new Size(358, 290);
            itemTypeBagTab.TabIndex = 1;
            itemTypeBagTab.Text = "tabPage3";
            // 
            // itemBagSizeLabel
            // 
            itemBagSizeLabel.AutoSize = true;
            itemBagSizeLabel.Location = new Point(19, 62);
            itemBagSizeLabel.Name = "itemBagSizeLabel";
            itemBagSizeLabel.Size = new Size(55, 15);
            itemBagSizeLabel.TabIndex = 1;
            itemBagSizeLabel.Text = "Bag size: ";
            // 
            // itemBagSizeSetter
            // 
            itemBagSizeSetter.Location = new Point(80, 60);
            itemBagSizeSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemBagSizeSetter.Name = "itemBagSizeSetter";
            itemBagSizeSetter.Size = new Size(80, 23);
            itemBagSizeSetter.TabIndex = 0;
            itemBagSizeSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // itemTypeConsumableTab
            // 
            itemTypeConsumableTab.BackColor = Color.Transparent;
            itemTypeConsumableTab.Controls.Add(itemConsumableTabController);
            itemTypeConsumableTab.Location = new Point(4, 24);
            itemTypeConsumableTab.Name = "itemTypeConsumableTab";
            itemTypeConsumableTab.Size = new Size(358, 290);
            itemTypeConsumableTab.TabIndex = 2;
            // 
            // itemConsumableTabController
            // 
            itemConsumableTabController.Controls.Add(itemConsumablePotionPage);
            itemConsumableTabController.Controls.Add(itemConsumableFoodPage);
            itemConsumableTabController.Dock = DockStyle.Fill;
            itemConsumableTabController.Location = new Point(0, 0);
            itemConsumableTabController.Name = "itemConsumableTabController";
            itemConsumableTabController.SelectedIndex = 0;
            itemConsumableTabController.Size = new Size(358, 290);
            itemConsumableTabController.TabIndex = 2;
            // 
            // itemConsumablePotionPage
            // 
            itemConsumablePotionPage.Controls.Add(itemPotionMaximumLabel);
            itemConsumablePotionPage.Controls.Add(itemPotionMinimumLabel);
            itemConsumablePotionPage.Controls.Add(itemPotionValueFlowLayout);
            itemConsumablePotionPage.Controls.Add(itemPotionTypeSetter);
            itemConsumablePotionPage.Location = new Point(4, 24);
            itemConsumablePotionPage.Name = "itemConsumablePotionPage";
            itemConsumablePotionPage.Padding = new Padding(3);
            itemConsumablePotionPage.Size = new Size(350, 262);
            itemConsumablePotionPage.TabIndex = 0;
            itemConsumablePotionPage.Text = "Potion";
            itemConsumablePotionPage.UseVisualStyleBackColor = true;
            // 
            // itemPotionMaximumLabel
            // 
            itemPotionMaximumLabel.AutoSize = true;
            itemPotionMaximumLabel.Location = new Point(222, 6);
            itemPotionMaximumLabel.Name = "itemPotionMaximumLabel";
            itemPotionMaximumLabel.Size = new Size(65, 15);
            itemPotionMaximumLabel.TabIndex = 5;
            itemPotionMaximumLabel.Text = "Maximum:";
            // 
            // itemPotionMinimumLabel
            // 
            itemPotionMinimumLabel.AutoSize = true;
            itemPotionMinimumLabel.Location = new Point(150, 6);
            itemPotionMinimumLabel.Name = "itemPotionMinimumLabel";
            itemPotionMinimumLabel.Size = new Size(63, 15);
            itemPotionMinimumLabel.TabIndex = 4;
            itemPotionMinimumLabel.Text = "Minimum:";
            // 
            // itemPotionValueFlowLayout
            // 
            itemPotionValueFlowLayout.Controls.Add(itemPotionHealthPanel);
            itemPotionValueFlowLayout.Controls.Add(itemPotionManaPanel);
            itemPotionValueFlowLayout.Controls.Add(itemPotionEnergyPanel);
            itemPotionValueFlowLayout.Location = new Point(91, 24);
            itemPotionValueFlowLayout.Name = "itemPotionValueFlowLayout";
            itemPotionValueFlowLayout.Size = new Size(214, 156);
            itemPotionValueFlowLayout.TabIndex = 2;
            // 
            // itemPotionHealthPanel
            // 
            itemPotionHealthPanel.AutoSize = true;
            itemPotionHealthPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            itemPotionHealthPanel.Controls.Add(itemPotionHealthLabel);
            itemPotionHealthPanel.Controls.Add(itemPotionHealthMaximumSetter);
            itemPotionHealthPanel.Controls.Add(itemPotionHealthMinimumSetter);
            itemPotionHealthPanel.Location = new Point(3, 3);
            itemPotionHealthPanel.Name = "itemPotionHealthPanel";
            itemPotionHealthPanel.Size = new Size(197, 29);
            itemPotionHealthPanel.TabIndex = 1;
            itemPotionHealthPanel.Visible = false;
            // 
            // itemPotionHealthLabel
            // 
            itemPotionHealthLabel.AutoSize = true;
            itemPotionHealthLabel.Location = new Point(3, 5);
            itemPotionHealthLabel.Name = "itemPotionHealthLabel";
            itemPotionHealthLabel.Size = new Size(45, 15);
            itemPotionHealthLabel.TabIndex = 1;
            itemPotionHealthLabel.Text = "Health:";
            // 
            // itemPotionHealthMaximumSetter
            // 
            itemPotionHealthMaximumSetter.Location = new Point(128, 3);
            itemPotionHealthMaximumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionHealthMaximumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionHealthMaximumSetter.Name = "itemPotionHealthMaximumSetter";
            itemPotionHealthMaximumSetter.Size = new Size(66, 23);
            itemPotionHealthMaximumSetter.TabIndex = 0;
            itemPotionHealthMaximumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionHealthMaximumSetter.ValueChanged += itemPotionHealthMaximumSetter_ValueChanged;
            // 
            // itemPotionHealthMinimumSetter
            // 
            itemPotionHealthMinimumSetter.Location = new Point(56, 3);
            itemPotionHealthMinimumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionHealthMinimumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionHealthMinimumSetter.Name = "itemPotionHealthMinimumSetter";
            itemPotionHealthMinimumSetter.Size = new Size(66, 23);
            itemPotionHealthMinimumSetter.TabIndex = 0;
            itemPotionHealthMinimumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionHealthMinimumSetter.ValueChanged += itemPotionHealthMinimumSetter_ValueChanged;
            // 
            // itemPotionManaPanel
            // 
            itemPotionManaPanel.AutoSize = true;
            itemPotionManaPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            itemPotionManaPanel.Controls.Add(itemPotionManaMaximumSetter);
            itemPotionManaPanel.Controls.Add(itemPotionManaLabel);
            itemPotionManaPanel.Controls.Add(itemPotionManaMinimumSetter);
            itemPotionManaPanel.Location = new Point(3, 38);
            itemPotionManaPanel.Name = "itemPotionManaPanel";
            itemPotionManaPanel.Size = new Size(197, 29);
            itemPotionManaPanel.TabIndex = 2;
            itemPotionManaPanel.Visible = false;
            // 
            // itemPotionManaMaximumSetter
            // 
            itemPotionManaMaximumSetter.Location = new Point(128, 3);
            itemPotionManaMaximumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionManaMaximumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionManaMaximumSetter.Name = "itemPotionManaMaximumSetter";
            itemPotionManaMaximumSetter.Size = new Size(66, 23);
            itemPotionManaMaximumSetter.TabIndex = 0;
            itemPotionManaMaximumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionManaMaximumSetter.ValueChanged += itemPotionManaMaximumSetter_ValueChanged;
            // 
            // itemPotionManaLabel
            // 
            itemPotionManaLabel.AutoSize = true;
            itemPotionManaLabel.Location = new Point(3, 5);
            itemPotionManaLabel.Name = "itemPotionManaLabel";
            itemPotionManaLabel.Size = new Size(40, 15);
            itemPotionManaLabel.TabIndex = 1;
            itemPotionManaLabel.Text = "Mana:";
            // 
            // itemPotionManaMinimumSetter
            // 
            itemPotionManaMinimumSetter.Location = new Point(56, 3);
            itemPotionManaMinimumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionManaMinimumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionManaMinimumSetter.Name = "itemPotionManaMinimumSetter";
            itemPotionManaMinimumSetter.Size = new Size(66, 23);
            itemPotionManaMinimumSetter.TabIndex = 0;
            itemPotionManaMinimumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionManaMinimumSetter.ValueChanged += itemPotionManaMinimumSetter_ValueChanged;
            // 
            // itemPotionEnergyPanel
            // 
            itemPotionEnergyPanel.AutoSize = true;
            itemPotionEnergyPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            itemPotionEnergyPanel.Controls.Add(itemPotionEnergyLabel);
            itemPotionEnergyPanel.Controls.Add(itemPotionEnergyMinimumSetter);
            itemPotionEnergyPanel.Controls.Add(itemPotionEnergyMaximumSetter);
            itemPotionEnergyPanel.Location = new Point(3, 73);
            itemPotionEnergyPanel.Name = "itemPotionEnergyPanel";
            itemPotionEnergyPanel.Size = new Size(197, 29);
            itemPotionEnergyPanel.TabIndex = 2;
            itemPotionEnergyPanel.Visible = false;
            // 
            // itemPotionEnergyLabel
            // 
            itemPotionEnergyLabel.AutoSize = true;
            itemPotionEnergyLabel.Location = new Point(3, 5);
            itemPotionEnergyLabel.Name = "itemPotionEnergyLabel";
            itemPotionEnergyLabel.Size = new Size(46, 15);
            itemPotionEnergyLabel.TabIndex = 1;
            itemPotionEnergyLabel.Text = "Energy:";
            // 
            // itemPotionEnergyMinimumSetter
            // 
            itemPotionEnergyMinimumSetter.Location = new Point(56, 3);
            itemPotionEnergyMinimumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionEnergyMinimumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionEnergyMinimumSetter.Name = "itemPotionEnergyMinimumSetter";
            itemPotionEnergyMinimumSetter.Size = new Size(66, 23);
            itemPotionEnergyMinimumSetter.TabIndex = 0;
            itemPotionEnergyMinimumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionEnergyMinimumSetter.ValueChanged += itemPotionEnergyMinimumSetter_ValueChanged;
            // 
            // itemPotionEnergyMaximumSetter
            // 
            itemPotionEnergyMaximumSetter.Location = new Point(128, 3);
            itemPotionEnergyMaximumSetter.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            itemPotionEnergyMaximumSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionEnergyMaximumSetter.Name = "itemPotionEnergyMaximumSetter";
            itemPotionEnergyMaximumSetter.Size = new Size(66, 23);
            itemPotionEnergyMaximumSetter.TabIndex = 0;
            itemPotionEnergyMaximumSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemPotionEnergyMaximumSetter.ValueChanged += itemPotionEnergyMaximumSetter_ValueChanged;
            // 
            // itemPotionTypeSetter
            // 
            itemPotionTypeSetter.CheckOnClick = true;
            itemPotionTypeSetter.FormattingEnabled = true;
            itemPotionTypeSetter.Items.AddRange(new object[] { "Health", "Mana", "Energy" });
            itemPotionTypeSetter.Location = new Point(3, 3);
            itemPotionTypeSetter.Name = "itemPotionTypeSetter";
            itemPotionTypeSetter.Size = new Size(77, 94);
            itemPotionTypeSetter.TabIndex = 0;
            itemPotionTypeSetter.ItemCheck += itemPotionTypeSetter_ItemCheck;
            // 
            // itemConsumableFoodPage
            // 
            itemConsumableFoodPage.Location = new Point(4, 24);
            itemConsumableFoodPage.Name = "itemConsumableFoodPage";
            itemConsumableFoodPage.Padding = new Padding(3);
            itemConsumableFoodPage.Size = new Size(350, 262);
            itemConsumableFoodPage.TabIndex = 1;
            itemConsumableFoodPage.Text = "Food";
            itemConsumableFoodPage.UseVisualStyleBackColor = true;
            // 
            // itemTypeEquipmentTab
            // 
            itemTypeEquipmentTab.BackColor = Color.Transparent;
            itemTypeEquipmentTab.Controls.Add(itemEquipmentMaterialSetter);
            itemTypeEquipmentTab.Controls.Add(itemEquipmentMaterialLabel);
            itemTypeEquipmentTab.Controls.Add(itemEquipmentTypeLabel);
            itemTypeEquipmentTab.Controls.Add(itemTypeEquipmentTypeSetter);
            itemTypeEquipmentTab.Controls.Add(itemStats);
            itemTypeEquipmentTab.Location = new Point(4, 24);
            itemTypeEquipmentTab.Name = "itemTypeEquipmentTab";
            itemTypeEquipmentTab.Size = new Size(358, 290);
            itemTypeEquipmentTab.TabIndex = 3;
            itemTypeEquipmentTab.Text = "tabPage1";
            // 
            // itemEquipmentMaterialSetter
            // 
            itemEquipmentMaterialSetter.DropDownStyle = ComboBoxStyle.DropDownList;
            itemEquipmentMaterialSetter.FormattingEnabled = true;
            itemEquipmentMaterialSetter.Items.AddRange(new object[] { "None", "Cloth", "Leather", "Mail", "Plate" });
            itemEquipmentMaterialSetter.Location = new Point(89, 221);
            itemEquipmentMaterialSetter.Name = "itemEquipmentMaterialSetter";
            itemEquipmentMaterialSetter.Size = new Size(118, 23);
            itemEquipmentMaterialSetter.TabIndex = 8;
            // 
            // itemEquipmentMaterialLabel
            // 
            itemEquipmentMaterialLabel.AutoSize = true;
            itemEquipmentMaterialLabel.Location = new Point(3, 224);
            itemEquipmentMaterialLabel.Name = "itemEquipmentMaterialLabel";
            itemEquipmentMaterialLabel.Size = new Size(53, 15);
            itemEquipmentMaterialLabel.TabIndex = 7;
            itemEquipmentMaterialLabel.Text = "Material:";
            // 
            // itemEquipmentTypeLabel
            // 
            itemEquipmentTypeLabel.AutoSize = true;
            itemEquipmentTypeLabel.Location = new Point(3, 250);
            itemEquipmentTypeLabel.Name = "itemEquipmentTypeLabel";
            itemEquipmentTypeLabel.Size = new Size(34, 15);
            itemEquipmentTypeLabel.TabIndex = 6;
            itemEquipmentTypeLabel.Text = "Type:";
            // 
            // itemTypeEquipmentTypeSetter
            // 
            itemTypeEquipmentTypeSetter.DropDownStyle = ComboBoxStyle.DropDownList;
            itemTypeEquipmentTypeSetter.FormattingEnabled = true;
            itemTypeEquipmentTypeSetter.Items.AddRange(new object[] { "Head", "Neck", "Shoulders", "Back", "Chest", "Wrist", "Hands", "Belt", "Legs", "Feet", "Finger", "Trinket" });
            itemTypeEquipmentTypeSetter.Location = new Point(89, 247);
            itemTypeEquipmentTypeSetter.Name = "itemTypeEquipmentTypeSetter";
            itemTypeEquipmentTypeSetter.Size = new Size(118, 23);
            itemTypeEquipmentTypeSetter.TabIndex = 3;
            // 
            // itemStats
            // 
            itemStats.Controls.Add(itemStatsArmorSetter);
            itemStats.Controls.Add(itemStatsArmorLabel);
            itemStats.Controls.Add(itemStatsSpiritLabel);
            itemStats.Controls.Add(itemStatsIntelligenceLabel);
            itemStats.Controls.Add(itemStatsStaminaLabel);
            itemStats.Controls.Add(itemStatsStrengthLabel);
            itemStats.Controls.Add(itemStatsAgilityLabel);
            itemStats.Controls.Add(itemStatsSpiritSetter);
            itemStats.Controls.Add(itemStatsIntelligenceSetter);
            itemStats.Controls.Add(itemStatsStaminaSetter);
            itemStats.Controls.Add(itemStatsStrengthSetter);
            itemStats.Controls.Add(itemStatsAgilitySetter);
            itemStats.Location = new Point(0, 0);
            itemStats.Name = "itemStats";
            itemStats.Size = new Size(149, 180);
            itemStats.TabIndex = 2;
            // 
            // itemStatsArmorSetter
            // 
            itemStatsArmorSetter.Location = new Point(75, 153);
            itemStatsArmorSetter.Name = "itemStatsArmorSetter";
            itemStatsArmorSetter.Size = new Size(72, 23);
            itemStatsArmorSetter.TabIndex = 5;
            // 
            // itemStatsArmorLabel
            // 
            itemStatsArmorLabel.AutoSize = true;
            itemStatsArmorLabel.Location = new Point(0, 155);
            itemStatsArmorLabel.Name = "itemStatsArmorLabel";
            itemStatsArmorLabel.Size = new Size(44, 15);
            itemStatsArmorLabel.TabIndex = 4;
            itemStatsArmorLabel.Text = "Armor:";
            // 
            // itemStatsSpiritLabel
            // 
            itemStatsSpiritLabel.AutoSize = true;
            itemStatsSpiritLabel.Location = new Point(0, 126);
            itemStatsSpiritLabel.Name = "itemStatsSpiritLabel";
            itemStatsSpiritLabel.Size = new Size(37, 15);
            itemStatsSpiritLabel.TabIndex = 3;
            itemStatsSpiritLabel.Text = "Spirit:";
            // 
            // itemStatsIntelligenceLabel
            // 
            itemStatsIntelligenceLabel.AutoSize = true;
            itemStatsIntelligenceLabel.Location = new Point(0, 97);
            itemStatsIntelligenceLabel.Name = "itemStatsIntelligenceLabel";
            itemStatsIntelligenceLabel.Size = new Size(71, 15);
            itemStatsIntelligenceLabel.TabIndex = 3;
            itemStatsIntelligenceLabel.Text = "Intelligence:";
            // 
            // itemStatsStaminaLabel
            // 
            itemStatsStaminaLabel.AutoSize = true;
            itemStatsStaminaLabel.Location = new Point(0, 68);
            itemStatsStaminaLabel.Name = "itemStatsStaminaLabel";
            itemStatsStaminaLabel.Size = new Size(53, 15);
            itemStatsStaminaLabel.TabIndex = 3;
            itemStatsStaminaLabel.Text = "Stamina:";
            // 
            // itemStatsStrengthLabel
            // 
            itemStatsStrengthLabel.AutoSize = true;
            itemStatsStrengthLabel.Location = new Point(0, 39);
            itemStatsStrengthLabel.Name = "itemStatsStrengthLabel";
            itemStatsStrengthLabel.Size = new Size(52, 15);
            itemStatsStrengthLabel.TabIndex = 3;
            itemStatsStrengthLabel.Text = "Strength";
            // 
            // itemStatsAgilityLabel
            // 
            itemStatsAgilityLabel.AutoSize = true;
            itemStatsAgilityLabel.Location = new Point(0, 10);
            itemStatsAgilityLabel.Name = "itemStatsAgilityLabel";
            itemStatsAgilityLabel.Size = new Size(44, 15);
            itemStatsAgilityLabel.TabIndex = 3;
            itemStatsAgilityLabel.Text = "Agility:";
            // 
            // itemStatsSpiritSetter
            // 
            itemStatsSpiritSetter.Location = new Point(75, 124);
            itemStatsSpiritSetter.Name = "itemStatsSpiritSetter";
            itemStatsSpiritSetter.Size = new Size(72, 23);
            itemStatsSpiritSetter.TabIndex = 2;
            // 
            // itemStatsIntelligenceSetter
            // 
            itemStatsIntelligenceSetter.Location = new Point(75, 95);
            itemStatsIntelligenceSetter.Name = "itemStatsIntelligenceSetter";
            itemStatsIntelligenceSetter.Size = new Size(72, 23);
            itemStatsIntelligenceSetter.TabIndex = 2;
            // 
            // itemStatsStaminaSetter
            // 
            itemStatsStaminaSetter.Location = new Point(75, 66);
            itemStatsStaminaSetter.Name = "itemStatsStaminaSetter";
            itemStatsStaminaSetter.Size = new Size(72, 23);
            itemStatsStaminaSetter.TabIndex = 2;
            // 
            // itemStatsStrengthSetter
            // 
            itemStatsStrengthSetter.Location = new Point(75, 37);
            itemStatsStrengthSetter.Name = "itemStatsStrengthSetter";
            itemStatsStrengthSetter.Size = new Size(72, 23);
            itemStatsStrengthSetter.TabIndex = 1;
            // 
            // itemStatsAgilitySetter
            // 
            itemStatsAgilitySetter.Location = new Point(75, 8);
            itemStatsAgilitySetter.Name = "itemStatsAgilitySetter";
            itemStatsAgilitySetter.Size = new Size(72, 23);
            itemStatsAgilitySetter.TabIndex = 0;
            // 
            // itemTypeWeaponTab
            // 
            itemTypeWeaponTab.BackColor = Color.Transparent;
            itemTypeWeaponTab.Controls.Add(itemDummyDpsChangingLabel);
            itemTypeWeaponTab.Controls.Add(itemDummyDpsLabel);
            itemTypeWeaponTab.Controls.Add(itemAttackSpeedLabel);
            itemTypeWeaponTab.Controls.Add(itemMaximumDamageLabel);
            itemTypeWeaponTab.Controls.Add(itemMinimumDamageLabel);
            itemTypeWeaponTab.Controls.Add(itemAttackSpeedSetter);
            itemTypeWeaponTab.Controls.Add(itemMaximumDamageSetter);
            itemTypeWeaponTab.Controls.Add(itemMinimumDamageSetter);
            itemTypeWeaponTab.Controls.Add(itemWeaponEQTypeLabel);
            itemTypeWeaponTab.Controls.Add(itemWeaponEQTypeSetter);
            itemTypeWeaponTab.Controls.Add(itemWeaponTypeSetter);
            itemTypeWeaponTab.Controls.Add(itemWeaponTypeLabel);
            itemTypeWeaponTab.Location = new Point(4, 24);
            itemTypeWeaponTab.Name = "itemTypeWeaponTab";
            itemTypeWeaponTab.Size = new Size(358, 290);
            itemTypeWeaponTab.TabIndex = 4;
            itemTypeWeaponTab.Text = "tabPage1";
            // 
            // itemDummyDpsChangingLabel
            // 
            itemDummyDpsChangingLabel.AutoSize = true;
            itemDummyDpsChangingLabel.Location = new Point(305, 224);
            itemDummyDpsChangingLabel.Name = "itemDummyDpsChangingLabel";
            itemDummyDpsChangingLabel.Size = new Size(13, 15);
            itemDummyDpsChangingLabel.TabIndex = 13;
            itemDummyDpsChangingLabel.Text = "1";
            // 
            // itemDummyDpsLabel
            // 
            itemDummyDpsLabel.AutoSize = true;
            itemDummyDpsLabel.Location = new Point(231, 224);
            itemDummyDpsLabel.Name = "itemDummyDpsLabel";
            itemDummyDpsLabel.Size = new Size(78, 15);
            itemDummyDpsLabel.TabIndex = 12;
            itemDummyDpsLabel.Text = "Dummy dps: ";
            // 
            // itemAttackSpeedLabel
            // 
            itemAttackSpeedLabel.AutoSize = true;
            itemAttackSpeedLabel.Location = new Point(247, 194);
            itemAttackSpeedLabel.Name = "itemAttackSpeedLabel";
            itemAttackSpeedLabel.Size = new Size(24, 15);
            itemAttackSpeedLabel.TabIndex = 11;
            itemAttackSpeedLabel.Text = "AS:";
            // 
            // itemMaximumDamageLabel
            // 
            itemMaximumDamageLabel.AutoSize = true;
            itemMaximumDamageLabel.Location = new Point(124, 194);
            itemMaximumDamageLabel.Name = "itemMaximumDamageLabel";
            itemMaximumDamageLabel.Size = new Size(33, 15);
            itemMaximumDamageLabel.TabIndex = 10;
            itemMaximumDamageLabel.Text = "Max:";
            // 
            // itemMinimumDamageLabel
            // 
            itemMinimumDamageLabel.AutoSize = true;
            itemMinimumDamageLabel.Location = new Point(3, 194);
            itemMinimumDamageLabel.Name = "itemMinimumDamageLabel";
            itemMinimumDamageLabel.Size = new Size(31, 15);
            itemMinimumDamageLabel.TabIndex = 9;
            itemMinimumDamageLabel.Text = "Min:";
            // 
            // itemAttackSpeedSetter
            // 
            itemAttackSpeedSetter.DecimalPlaces = 1;
            itemAttackSpeedSetter.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            itemAttackSpeedSetter.Location = new Point(277, 192);
            itemAttackSpeedSetter.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            itemAttackSpeedSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemAttackSpeedSetter.Name = "itemAttackSpeedSetter";
            itemAttackSpeedSetter.Size = new Size(37, 23);
            itemAttackSpeedSetter.TabIndex = 8;
            itemAttackSpeedSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemAttackSpeedSetter.ValueChanged += itemAttackSpeedSetter_ValueChanged;
            // 
            // itemMaximumDamageSetter
            // 
            itemMaximumDamageSetter.Location = new Point(163, 192);
            itemMaximumDamageSetter.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            itemMaximumDamageSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaximumDamageSetter.Name = "itemMaximumDamageSetter";
            itemMaximumDamageSetter.Size = new Size(78, 23);
            itemMaximumDamageSetter.TabIndex = 7;
            itemMaximumDamageSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaximumDamageSetter.ValueChanged += itemMaxDamageSetter_ValueChanged;
            // 
            // itemMinimumDamageSetter
            // 
            itemMinimumDamageSetter.Location = new Point(40, 192);
            itemMinimumDamageSetter.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            itemMinimumDamageSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemMinimumDamageSetter.Name = "itemMinimumDamageSetter";
            itemMinimumDamageSetter.Size = new Size(78, 23);
            itemMinimumDamageSetter.TabIndex = 6;
            itemMinimumDamageSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemMinimumDamageSetter.ValueChanged += itemMinimumDamageSetter_ValueChanged;
            // 
            // itemWeaponEQTypeLabel
            // 
            itemWeaponEQTypeLabel.AutoSize = true;
            itemWeaponEQTypeLabel.Location = new Point(3, 250);
            itemWeaponEQTypeLabel.Name = "itemWeaponEQTypeLabel";
            itemWeaponEQTypeLabel.Size = new Size(34, 15);
            itemWeaponEQTypeLabel.TabIndex = 5;
            itemWeaponEQTypeLabel.Text = "Type:";
            // 
            // itemWeaponEQTypeSetter
            // 
            itemWeaponEQTypeSetter.DropDownStyle = ComboBoxStyle.DropDownList;
            itemWeaponEQTypeSetter.FormattingEnabled = true;
            itemWeaponEQTypeSetter.Items.AddRange(new object[] { "One Handed", "Main Handed", "Off Handed", "Two Handed", "Ranged" });
            itemWeaponEQTypeSetter.Location = new Point(89, 247);
            itemWeaponEQTypeSetter.Name = "itemWeaponEQTypeSetter";
            itemWeaponEQTypeSetter.Size = new Size(129, 23);
            itemWeaponEQTypeSetter.TabIndex = 4;
            // 
            // itemWeaponTypeSetter
            // 
            itemWeaponTypeSetter.DropDownStyle = ComboBoxStyle.DropDownList;
            itemWeaponTypeSetter.FormattingEnabled = true;
            itemWeaponTypeSetter.Items.AddRange(new object[] { "Dagger", "Sword", "Two Handed Sword", "Axe", "Two Handed Axe", "Mace", "Two Handed Mace", "Fist", "Staff", "Bow", "Gun", "Thrown", "Wand", "Shield", "Holdable" });
            itemWeaponTypeSetter.Location = new Point(89, 221);
            itemWeaponTypeSetter.Name = "itemWeaponTypeSetter";
            itemWeaponTypeSetter.Size = new Size(129, 23);
            itemWeaponTypeSetter.TabIndex = 1;
            itemWeaponTypeSetter.SelectedIndexChanged += itemWeaponTypeSetter_SelectedIndexChanged;
            // 
            // itemWeaponTypeLabel
            // 
            itemWeaponTypeLabel.AutoSize = true;
            itemWeaponTypeLabel.Location = new Point(3, 224);
            itemWeaponTypeLabel.Name = "itemWeaponTypeLabel";
            itemWeaponTypeLabel.Size = new Size(80, 15);
            itemWeaponTypeLabel.TabIndex = 0;
            itemWeaponTypeLabel.Text = "Weapon type:";
            // 
            // itemQualitySelector
            // 
            itemQualitySelector.CheckOnClick = true;
            itemQualitySelector.FormattingEnabled = true;
            itemQualitySelector.Items.AddRange(new object[] { "Poor", "Common", "Uncommon", "Rare", "Epic", "Legendary" });
            itemQualitySelector.Location = new Point(0, 91);
            itemQualitySelector.Name = "itemQualitySelector";
            itemQualitySelector.Size = new Size(91, 112);
            itemQualitySelector.TabIndex = 1;
            // 
            // itemTypeSelector
            // 
            itemTypeSelector.CheckOnClick = true;
            itemTypeSelector.FormattingEnabled = true;
            itemTypeSelector.ImeMode = ImeMode.Off;
            itemTypeSelector.Items.AddRange(new object[] { "None", "Bag", "Consumable", "Equipment", "Weapon" });
            itemTypeSelector.Location = new Point(0, 0);
            itemTypeSelector.Name = "itemTypeSelector";
            itemTypeSelector.Size = new Size(91, 94);
            itemTypeSelector.TabIndex = 0;
            itemTypeSelector.UseTabStops = false;
            itemTypeSelector.ItemCheck += itemTypeSelector_ItemCheck;
            // 
            // descriptionTab
            // 
            descriptionTab.Controls.Add(itemDescriptionInput);
            descriptionTab.Location = new Point(4, 24);
            descriptionTab.Name = "descriptionTab";
            descriptionTab.Padding = new Padding(3);
            descriptionTab.Size = new Size(459, 352);
            descriptionTab.TabIndex = 1;
            descriptionTab.Text = "Description";
            descriptionTab.UseVisualStyleBackColor = true;
            // 
            // itemDescriptionInput
            // 
            itemDescriptionInput.Dock = DockStyle.Fill;
            itemDescriptionInput.Location = new Point(3, 3);
            itemDescriptionInput.Name = "itemDescriptionInput";
            itemDescriptionInput.Size = new Size(453, 346);
            itemDescriptionInput.TabIndex = 0;
            itemDescriptionInput.Text = "";
            // 
            // itemMaxCountSetter
            // 
            itemMaxCountSetter.Location = new Point(344, 9);
            itemMaxCountSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaxCountSetter.Name = "itemMaxCountSetter";
            itemMaxCountSetter.Size = new Size(41, 23);
            itemMaxCountSetter.TabIndex = 5;
            itemMaxCountSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // itemCountLabel
            // 
            itemCountLabel.AutoSize = true;
            itemCountLabel.Location = new Point(279, 11);
            itemCountLabel.Name = "itemCountLabel";
            itemCountLabel.Size = new Size(67, 15);
            itemCountLabel.TabIndex = 4;
            itemCountLabel.Text = "Max count:";
            // 
            // createItemButton
            // 
            createItemButton.Location = new Point(475, 394);
            createItemButton.Name = "createItemButton";
            createItemButton.Size = new Size(86, 24);
            createItemButton.TabIndex = 3;
            createItemButton.Text = "Create Item";
            createItemButton.UseVisualStyleBackColor = true;
            createItemButton.Click += createItemButton_Click;
            // 
            // itemEffectTab
            // 
            itemEffectTab.Controls.Add(label2);
            itemEffectTab.Controls.Add(label1);
            itemEffectTab.Controls.Add(listBox2);
            itemEffectTab.Controls.Add(listBox1);
            itemEffectTab.Location = new Point(4, 24);
            itemEffectTab.Name = "itemEffectTab";
            itemEffectTab.Padding = new Padding(3);
            itemEffectTab.Size = new Size(562, 453);
            itemEffectTab.TabIndex = 1;
            itemEffectTab.Text = "Item Effects";
            itemEffectTab.UseVisualStyleBackColor = true;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(306, 7);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 3;
            label2.Text = "Selected Item Effects:";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(18, 5);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 2;
            label1.Text = "All effects:";
            //
            // listBox2
            //
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(305, 30);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(231, 334);
            listBox2.TabIndex = 1;
            //
            // listBox1
            //
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(25, 30);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(231, 334);
            listBox1.TabIndex = 0;
            //
            // otherPage
            //
            otherPage.Location = new Point(4, 24);
            otherPage.Name = "otherPage";
            otherPage.Padding = new Padding(3);
            otherPage.Size = new Size(792, 483);
            otherPage.TabIndex = 2;
            otherPage.Text = "??";
            otherPage.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(968, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveDatapackToolStripMenuItem, loadDatapackToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveDatapackToolStripMenuItem
            // 
            saveDatapackToolStripMenuItem.Name = "saveDatapackToolStripMenuItem";
            saveDatapackToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveDatapackToolStripMenuItem.Size = new Size(190, 22);
            saveDatapackToolStripMenuItem.Text = "Save Datapack";
            saveDatapackToolStripMenuItem.Click += saveDatapackToolStripMenuItem_Click;
            // 
            // loadDatapackToolStripMenuItem
            // 
            loadDatapackToolStripMenuItem.Name = "loadDatapackToolStripMenuItem";
            loadDatapackToolStripMenuItem.Size = new Size(190, 22);
            loadDatapackToolStripMenuItem.Text = "Load Datapack";
            loadDatapackToolStripMenuItem.Click += loadDatapackToolStripMenuItem_Click;
            //
            // exitToolStripMenuItem
            //
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(190, 22);
            exitToolStripMenuItem.Text = "Exit";
            //
            // helpToolStripMenuItem
            //
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fileDownloadInstructionsToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            //
            // toolsToolStripMenuItem
            //
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { spriteEditorToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(48, 20);
            toolsToolStripMenuItem.Text = "Tools";
            //
            // spriteEditorToolStripMenuItem
            //
            spriteEditorToolStripMenuItem.Name = "spriteEditorToolStripMenuItem";
            spriteEditorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
            spriteEditorToolStripMenuItem.Size = new Size(239, 22);
            spriteEditorToolStripMenuItem.Text = "Open Sprite Editor";
            spriteEditorToolStripMenuItem.Click += spriteEditorToolStripMenuItem_Click;
            //
            // fileDownloadInstructionsToolStripMenuItem
            //
            fileDownloadInstructionsToolStripMenuItem.Name = "fileDownloadInstructionsToolStripMenuItem";
            fileDownloadInstructionsToolStripMenuItem.Size = new Size(208, 22);
            fileDownloadInstructionsToolStripMenuItem.Text = "File Download Instructions";
            fileDownloadInstructionsToolStripMenuItem.Click += fileDownloadInstructionsToolStripMenuItem_Click;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 539);
            Controls.Add(MainTab);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "SPHM Maker";
            ((System.ComponentModel.ISupportInitialize)lootDistributionChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)lootKillsCounter).EndInit();
            ((System.ComponentModel.ISupportInitialize)lootEntriesGrid).EndInit();
            lootTabPage.ResumeLayout(false);
            lootTabPage.PerformLayout();
            MainTab.ResumeLayout(false);
            ItemPageTab.ResumeLayout(false);
            ItemPageTab.PerformLayout();
            itemWindowTabControl.ResumeLayout(false);
            createItemPage.ResumeLayout(false);
            createItemPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)goldCostCounter).EndInit();
            itemToCreate.ResumeLayout(false);
            itemTypeTab.ResumeLayout(false);
            itemTypeTabControl.ResumeLayout(false);
            itemTypeNoneTab.ResumeLayout(false);
            itemTypeNoneTab.PerformLayout();
            itemTypeBagTab.ResumeLayout(false);
            itemTypeBagTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemBagSizeSetter).EndInit();
            itemTypeConsumableTab.ResumeLayout(false);
            itemConsumableTabController.ResumeLayout(false);
            itemConsumablePotionPage.ResumeLayout(false);
            itemConsumablePotionPage.PerformLayout();
            itemPotionValueFlowLayout.ResumeLayout(false);
            itemPotionValueFlowLayout.PerformLayout();
            itemPotionHealthPanel.ResumeLayout(false);
            itemPotionHealthPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionHealthMaximumSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionHealthMinimumSetter).EndInit();
            itemPotionManaPanel.ResumeLayout(false);
            itemPotionManaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionManaMaximumSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionManaMinimumSetter).EndInit();
            itemPotionEnergyPanel.ResumeLayout(false);
            itemPotionEnergyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemPotionEnergyMinimumSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemPotionEnergyMaximumSetter).EndInit();
            itemTypeEquipmentTab.ResumeLayout(false);
            itemTypeEquipmentTab.PerformLayout();
            itemStats.ResumeLayout(false);
            itemStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemStatsArmorSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsSpiritSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsIntelligenceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStaminaSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStrengthSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsAgilitySetter).EndInit();
            itemTypeWeaponTab.ResumeLayout(false);
            itemTypeWeaponTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemAttackSpeedSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemMaximumDamageSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemMinimumDamageSetter).EndInit();
            descriptionTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).EndInit();
            itemEffectTab.ResumeLayout(false);
            itemEffectTab.PerformLayout();
            tileDetailsGroup.ResumeLayout(false);
            tileDetailsGroup.PerformLayout();
            TilesPageTab.ResumeLayout(false);
            TilesPageTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tileMovementCostInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)tileIdInput).EndInit();
            lootTabPage.ResumeLayout(false);
            lootTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lootEntriesGrid).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox itemNameInput;
        private ListBox items;
        private Label itemNameLabel;
        private Label itemSelectBoxLabel;
        private TabControl MainTab;
        private TabPage ItemPageTab;
        private TabPage lootTabPage;
        private TabPage TilesPageTab;
        private TabPage otherPage;
        private GroupBox tileDetailsGroup;
        private Button tileResetButton;
        private Button tileSaveButton;
        private Button tileCreateButton;
        private RichTextBox tileNotesInput;
        private Label tileNotesLabel;
        private NumericUpDown tileMovementCostInput;
        private Label tileMovementCostLabel;
        private CheckBox tileWalkableCheckbox;
        private TextBox tileTextureInput;
        private Label tileTextureLabel;
        private TextBox tileNameInput;
        private Label tileNameLabel;
        private NumericUpDown tileIdInput;
        private Label tileIdLabel;
        private Label tileListLabel;
        private ListBox tileList;
        private TabControl itemWindowTabControl;
        private TabPage createItemPage;
        private TabPage itemEffectTab;
        private Button createItemButton;
        private NumericUpDown itemMaxCountSetter;
        private Label itemCountLabel;
        private TabControl itemToCreate;
        private TabPage itemTypeTab;
        private TabPage descriptionTab;
        private RichTextBox itemDescriptionInput;
        private ExtendedForm.ExtendedCheckedListBox itemTypeSelector;
        private NumericUpDown goldCostCounter;
        private Label goldCostLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveDatapackToolStripMenuItem;
        private ToolStripMenuItem loadDatapackToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem spriteEditorToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem fileDownloadInstructionsToolStripMenuItem;
        private Button EditItemButton;
        private Button OverrideItemButton;
        private ListBox lootTablesListBox;
        private Label lootTableIdLabel;
        private TextBox lootTableIdTextBox;
        private Button lootAddTableButton;
        private Button lootSaveTableButton;
        private Button lootDeleteTableButton;
        private DataGridView lootEntriesGrid;
        private Button lootAddEntryButton;
        private Button lootRemoveEntryButton;
        private Label lootKillsLabel;
        private NumericUpDown lootKillsCounter;
        private Chart lootDistributionChart;
        private Label lootChartHintLabel;
        private ToolTip toolTip1;
        private ExtendedForm.ExtendedCheckedListBox itemQualitySelector;
        private TabControl itemTypeTabControl;
        private TabPage itemTypeBagTab;
        public TabPage itemTypeNoneTab;
        private Label nonePageLabel;
        private NumericUpDown itemBagSizeSetter;
        private Label itemBagSizeLabel;
        private TabPage itemTypeConsumableTab;
        private TabPage itemTypeEquipmentTab;
        private TabPage itemTypeWeaponTab;
        private NumericUpDown itemStatsAgilitySetter;
        private Panel itemStats;
        private NumericUpDown itemStatsStrengthSetter;
        private Label itemStatsSpiritLabel;
        private Label itemStatsIntelligenceLabel;
        private Label itemStatsStaminaLabel;
        private Label itemStatsStrengthLabel;
        private Label itemStatsAgilityLabel;
        private NumericUpDown itemStatsSpiritSetter;
        private NumericUpDown itemStatsIntelligenceSetter;
        private NumericUpDown itemStatsStaminaSetter;
        private ComboBox itemTypeEquipmentTypeSetter;
        private NumericUpDown itemStatsArmorSetter;
        private Label itemStatsArmorLabel;
        private Label itemWeaponTypeLabel;
        private ComboBox itemWeaponTypeSetter;
        private ComboBox itemWeaponEQTypeSetter;
        private Label label2;
        private Label label1;
        private ListBox listBox2;
        private ListBox listBox1;
        private Label itemEquipmentTypeLabel;
        private Label itemWeaponEQTypeLabel;
        private Label itemMaximumDamageLabel;
        private Label itemMinimumDamageLabel;
        private NumericUpDown itemAttackSpeedSetter;
        private NumericUpDown itemMaximumDamageSetter;
        private NumericUpDown itemMinimumDamageSetter;
        private Label itemAttackSpeedLabel;
        private Label itemDummyDpsChangingLabel;
        private Label itemDummyDpsLabel;
        private CheckedListBox itemPotionTypeSetter;
        private Panel itemPotionHealthPanel;
        private TabControl itemConsumableTabController;
        private TabPage itemConsumablePotionPage;
        private NumericUpDown itemPotionHealthMaximumSetter;
        private TabPage itemConsumableFoodPage;
        private Label itemPotionHealthLabel;
        private FlowLayoutPanel itemPotionValueFlowLayout;
        private Panel itemPotionManaPanel;
        private Label itemPotionManaLabel;
        private NumericUpDown itemPotionManaMinimumSetter;
        private Panel itemPotionEnergyPanel;
        private Label itemPotionEnergyLabel;
        private NumericUpDown itemPotionEnergyMinimumSetter;
        private ComboBox itemEquipmentMaterialSetter;
        private Label itemEquipmentMaterialLabel;
        private NumericUpDown itemPotionHealthMinimumSetter;
        private NumericUpDown itemPotionEnergyMaximumSetter;
        private NumericUpDown itemPotionManaMaximumSetter;
        private Label itemPotionMaximumLabel;
        private Label itemPotionMinimumLabel;
        private Button itemCheckGeneratedTooltip;
    }
}
