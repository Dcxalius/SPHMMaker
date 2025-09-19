using SPHMMaker.Items;

namespace SPHMMaker
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            itemNameInput = new TextBox();
            items = new ListBox();
            itemNameLabel = new Label();
            itemSelectBoxLabel = new Label();
            MainTab = new TabControl();
            ItemPageTab = new TabPage();
            OverrideItemButton = new Button();
            EditItemButton = new Button();
            itemCreatorModifier = new TabControl();
            createItemPage = new TabPage();
            goldCostCounter = new NumericUpDown();
            goldCostLabel = new Label();
            itemToCreate = new TabControl();
            itemTypeTab = new TabPage();
            itemTypeTabControl = new TabControl();
            itemTypeNoneTab = new TabPage();
            nonePageLabel = new Label();
            itemTypeBagTab = new TabPage();
            bagSizeLabel = new Label();
            bagSizeSetter = new NumericUpDown();
            itemTypeConsumableTab = new TabPage();
            itemTypeEquipmentTab = new TabPage();
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
            itemQualitySelector = new ExtendedForm.ExtendedCheckedListBox();
            itemTypeSelector = new ExtendedForm.ExtendedCheckedListBox();
            descriptionTab = new TabPage();
            descriptionInput = new RichTextBox();
            itemMaxCountSetter = new NumericUpDown();
            itemCountLabel = new Label();
            createItemButton = new Button();
            modItemPage = new TabPage();
            otherPage = new TabPage();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveDatapackToolStripMenuItem = new ToolStripMenuItem();
            loadDatapackToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            MainTab.SuspendLayout();
            ItemPageTab.SuspendLayout();
            itemCreatorModifier.SuspendLayout();
            createItemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)goldCostCounter).BeginInit();
            itemToCreate.SuspendLayout();
            itemTypeTab.SuspendLayout();
            itemTypeTabControl.SuspendLayout();
            itemTypeNoneTab.SuspendLayout();
            itemTypeBagTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bagSizeSetter).BeginInit();
            itemTypeEquipmentTab.SuspendLayout();
            itemStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemStatsArmorSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsSpiritSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsIntelligenceSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStaminaSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStrengthSetter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsAgilitySetter).BeginInit();
            descriptionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).BeginInit();
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
            items.FormattingEnabled = true;
            items.ItemHeight = 15;
            items.Location = new Point(582, 30);
            items.Name = "items";
            items.Size = new Size(202, 364);
            items.TabIndex = 1;
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
            MainTab.Controls.Add(otherPage);
            MainTab.Location = new Point(0, 27);
            MainTab.Name = "MainTab";
            MainTab.SelectedIndex = 0;
            MainTab.Size = new Size(800, 450);
            MainTab.TabIndex = 4;
            // 
            // ItemPageTab
            // 
            ItemPageTab.Controls.Add(OverrideItemButton);
            ItemPageTab.Controls.Add(EditItemButton);
            ItemPageTab.Controls.Add(itemCreatorModifier);
            ItemPageTab.Controls.Add(itemSelectBoxLabel);
            ItemPageTab.Controls.Add(items);
            ItemPageTab.Location = new Point(4, 24);
            ItemPageTab.Name = "ItemPageTab";
            ItemPageTab.Padding = new Padding(3);
            ItemPageTab.Size = new Size(792, 422);
            ItemPageTab.TabIndex = 0;
            ItemPageTab.Text = "Items";
            ItemPageTab.UseVisualStyleBackColor = true;
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
            // itemCreatorModifier
            // 
            itemCreatorModifier.Controls.Add(createItemPage);
            itemCreatorModifier.Controls.Add(modItemPage);
            itemCreatorModifier.Location = new Point(6, 6);
            itemCreatorModifier.Name = "itemCreatorModifier";
            itemCreatorModifier.SelectedIndex = 0;
            itemCreatorModifier.Size = new Size(570, 416);
            itemCreatorModifier.TabIndex = 4;
            // 
            // createItemPage
            // 
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
            createItemPage.Size = new Size(562, 388);
            createItemPage.TabIndex = 0;
            createItemPage.Text = "Create Item";
            createItemPage.UseVisualStyleBackColor = true;
            // 
            // goldCostCounter
            // 
            goldCostCounter.Location = new Point(513, 38);
            goldCostCounter.Name = "goldCostCounter";
            goldCostCounter.Size = new Size(41, 23);
            goldCostCounter.TabIndex = 8;
            goldCostCounter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // goldCostLabel
            // 
            goldCostLabel.AutoSize = true;
            goldCostLabel.Location = new Point(455, 40);
            goldCostLabel.Name = "goldCostLabel";
            goldCostLabel.Size = new Size(60, 15);
            goldCostLabel.TabIndex = 7;
            goldCostLabel.Text = "Gold cost:";
            // 
            // itemToCreate
            // 
            itemToCreate.Controls.Add(itemTypeTab);
            itemToCreate.Controls.Add(descriptionTab);
            itemToCreate.Location = new Point(6, 40);
            itemToCreate.Name = "itemToCreate";
            itemToCreate.SelectedIndex = 0;
            itemToCreate.Size = new Size(458, 342);
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
            itemTypeTab.Size = new Size(450, 314);
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
            itemTypeTabControl.Location = new Point(99, 6);
            itemTypeTabControl.Name = "itemTypeTabControl";
            itemTypeTabControl.SelectedIndex = 0;
            itemTypeTabControl.Size = new Size(345, 305);
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
            itemTypeNoneTab.Size = new Size(337, 277);
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
            itemTypeBagTab.Controls.Add(bagSizeLabel);
            itemTypeBagTab.Controls.Add(bagSizeSetter);
            itemTypeBagTab.Location = new Point(4, 24);
            itemTypeBagTab.Name = "itemTypeBagTab";
            itemTypeBagTab.Padding = new Padding(3);
            itemTypeBagTab.Size = new Size(337, 277);
            itemTypeBagTab.TabIndex = 1;
            itemTypeBagTab.Text = "tabPage3";
            // 
            // bagSizeLabel
            // 
            bagSizeLabel.AutoSize = true;
            bagSizeLabel.Location = new Point(19, 62);
            bagSizeLabel.Name = "bagSizeLabel";
            bagSizeLabel.Size = new Size(55, 15);
            bagSizeLabel.TabIndex = 1;
            bagSizeLabel.Text = "Bag size: ";
            // 
            // bagSizeSetter
            // 
            bagSizeSetter.Location = new Point(80, 60);
            bagSizeSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            bagSizeSetter.Name = "bagSizeSetter";
            bagSizeSetter.Size = new Size(80, 23);
            bagSizeSetter.TabIndex = 0;
            bagSizeSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // itemTypeConsumableTab
            // 
            itemTypeConsumableTab.BackColor = Color.Transparent;
            itemTypeConsumableTab.Location = new Point(4, 24);
            itemTypeConsumableTab.Name = "itemTypeConsumableTab";
            itemTypeConsumableTab.Size = new Size(337, 277);
            itemTypeConsumableTab.TabIndex = 2;
            // 
            // itemTypeEquipmentTab
            // 
            itemTypeEquipmentTab.BackColor = Color.Transparent;
            itemTypeEquipmentTab.Controls.Add(itemTypeEquipmentTypeSetter);
            itemTypeEquipmentTab.Controls.Add(itemStats);
            itemTypeEquipmentTab.Location = new Point(4, 24);
            itemTypeEquipmentTab.Name = "itemTypeEquipmentTab";
            itemTypeEquipmentTab.Size = new Size(337, 277);
            itemTypeEquipmentTab.TabIndex = 3;
            itemTypeEquipmentTab.Text = "tabPage1";
            // 
            // itemTypeEquipmentTypeSetter
            // 
            itemTypeEquipmentTypeSetter.DropDownStyle = ComboBoxStyle.DropDownList;
            itemTypeEquipmentTypeSetter.FormattingEnabled = true;
            itemTypeEquipmentTypeSetter.Items.AddRange(new object[] { "Head", "Neck", "Shoulders", "Back", "Chest", "Wrist", "Hands", "Belt", "Legs", "Feet", "Finger", "Trinket" });
            itemTypeEquipmentTypeSetter.Location = new Point(13, 186);
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
            itemStats.Size = new Size(161, 180);
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
            itemTypeWeaponTab.Location = new Point(4, 24);
            itemTypeWeaponTab.Name = "itemTypeWeaponTab";
            itemTypeWeaponTab.Size = new Size(337, 277);
            itemTypeWeaponTab.TabIndex = 4;
            itemTypeWeaponTab.Text = "tabPage1";
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
            descriptionTab.Controls.Add(descriptionInput);
            descriptionTab.Location = new Point(4, 24);
            descriptionTab.Name = "descriptionTab";
            descriptionTab.Padding = new Padding(3);
            descriptionTab.Size = new Size(450, 314);
            descriptionTab.TabIndex = 1;
            descriptionTab.Text = "Description";
            descriptionTab.UseVisualStyleBackColor = true;
            // 
            // descriptionInput
            // 
            descriptionInput.Location = new Point(7, 6);
            descriptionInput.Name = "descriptionInput";
            descriptionInput.Size = new Size(348, 272);
            descriptionInput.TabIndex = 0;
            descriptionInput.Text = "";
            // 
            // itemMaxCountSetter
            // 
            itemMaxCountSetter.Location = new Point(513, 9);
            itemMaxCountSetter.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaxCountSetter.Name = "itemMaxCountSetter";
            itemMaxCountSetter.Size = new Size(41, 23);
            itemMaxCountSetter.TabIndex = 5;
            itemMaxCountSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // itemCountLabel
            // 
            itemCountLabel.AutoSize = true;
            itemCountLabel.Location = new Point(448, 11);
            itemCountLabel.Name = "itemCountLabel";
            itemCountLabel.Size = new Size(67, 15);
            itemCountLabel.TabIndex = 4;
            itemCountLabel.Text = "Max count:";
            // 
            // createItemButton
            // 
            createItemButton.Location = new Point(470, 358);
            createItemButton.Name = "createItemButton";
            createItemButton.Size = new Size(86, 24);
            createItemButton.TabIndex = 3;
            createItemButton.Text = "Create Item";
            createItemButton.UseVisualStyleBackColor = true;
            createItemButton.Click += createItemButton_Click;
            // 
            // modItemPage
            // 
            modItemPage.Location = new Point(4, 24);
            modItemPage.Name = "modItemPage";
            modItemPage.Padding = new Padding(3);
            modItemPage.Size = new Size(562, 388);
            modItemPage.TabIndex = 1;
            modItemPage.Text = "Modify Item";
            modItemPage.UseVisualStyleBackColor = true;
            // 
            // otherPage
            // 
            otherPage.Location = new Point(4, 24);
            otherPage.Name = "otherPage";
            otherPage.Padding = new Padding(3);
            otherPage.Size = new Size(792, 422);
            otherPage.TabIndex = 1;
            otherPage.Text = "??";
            otherPage.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 539);
            Controls.Add(MainTab);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            MainTab.ResumeLayout(false);
            ItemPageTab.ResumeLayout(false);
            ItemPageTab.PerformLayout();
            itemCreatorModifier.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)bagSizeSetter).EndInit();
            itemTypeEquipmentTab.ResumeLayout(false);
            itemStats.ResumeLayout(false);
            itemStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemStatsArmorSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsSpiritSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsIntelligenceSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStaminaSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsStrengthSetter).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemStatsAgilitySetter).EndInit();
            descriptionTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).EndInit();
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
        private TabPage otherPage;
        private TabControl itemCreatorModifier;
        private TabPage createItemPage;
        private TabPage modItemPage;
        private Button createItemButton;
        private NumericUpDown itemMaxCountSetter;
        private Label itemCountLabel;
        private TabControl itemToCreate;
        private TabPage itemTypeTab;
        private TabPage descriptionTab;
        private RichTextBox descriptionInput;
        private ExtendedForm.ExtendedCheckedListBox itemTypeSelector;
        private NumericUpDown goldCostCounter;
        private Label goldCostLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveDatapackToolStripMenuItem;
        private ToolStripMenuItem loadDatapackToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button EditItemButton;
        private Button OverrideItemButton;
        private ToolTip toolTip1;
        private ExtendedForm.ExtendedCheckedListBox itemQualitySelector;
        private TabControl itemTypeTabControl;
        private TabPage itemTypeBagTab;
        public TabPage itemTypeNoneTab;
        private Label nonePageLabel;
        private NumericUpDown bagSizeSetter;
        private Label bagSizeLabel;
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
    }
}
