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
            EditItemButton = new Button();
            itemCreatorModifier = new TabControl();
            createItemPage = new TabPage();
            numericUpDown1 = new NumericUpDown();
            GoldCostLabel = new Label();
            tabControl2 = new TabControl();
            itemTypeTab = new TabPage();
            ItemType = new CheckedListBox();
            descriptionTab = new TabPage();
            descriptionInput = new RichTextBox();
            itemMaxCountSetter = new NumericUpDown();
            itemCountLabel = new Label();
            createItemButton = new Button();
            modItemPage = new TabPage();
            tabPage2 = new TabPage();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveDatapackToolStripMenuItem = new ToolStripMenuItem();
            loadDatapackToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            OverrideItemButton = new Button();
            toolTip1 = new ToolTip(components);
            MainTab.SuspendLayout();
            ItemPageTab.SuspendLayout();
            itemCreatorModifier.SuspendLayout();
            createItemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabControl2.SuspendLayout();
            itemTypeTab.SuspendLayout();
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
            itemNameInput.TextChanged += textBox1_TextChanged;
            // 
            // items
            // 
            items.FormattingEnabled = true;
            items.ItemHeight = 15;
            items.Location = new Point(582, 30);
            items.Name = "items";
            items.Size = new Size(202, 364);
            items.TabIndex = 1;
            items.SelectedIndexChanged += items_SelectedIndexChanged;
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
            MainTab.Controls.Add(tabPage2);
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
            // EditItemButton
            // 
            EditItemButton.Location = new Point(582, 395);
            EditItemButton.Name = "EditItemButton";
            EditItemButton.Size = new Size(100, 24);
            EditItemButton.TabIndex = 9;
            EditItemButton.Text = "Edit Item";
            EditItemButton.UseVisualStyleBackColor = true;
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
            createItemPage.Controls.Add(numericUpDown1);
            createItemPage.Controls.Add(GoldCostLabel);
            createItemPage.Controls.Add(tabControl2);
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
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(434, 62);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(41, 23);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // GoldCostLabel
            // 
            GoldCostLabel.AutoSize = true;
            GoldCostLabel.Location = new Point(376, 64);
            GoldCostLabel.Name = "GoldCostLabel";
            GoldCostLabel.Size = new Size(60, 15);
            GoldCostLabel.TabIndex = 7;
            GoldCostLabel.Text = "Gold cost:";
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(itemTypeTab);
            tabControl2.Controls.Add(descriptionTab);
            tabControl2.Location = new Point(6, 40);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(364, 314);
            tabControl2.TabIndex = 6;
            // 
            // itemTypeTab
            // 
            itemTypeTab.Controls.Add(ItemType);
            itemTypeTab.Location = new Point(4, 24);
            itemTypeTab.Name = "itemTypeTab";
            itemTypeTab.Padding = new Padding(3);
            itemTypeTab.Size = new Size(356, 286);
            itemTypeTab.TabIndex = 0;
            itemTypeTab.Text = "Item Type";
            itemTypeTab.UseVisualStyleBackColor = true;
            // 
            // ItemType
            // 
            ItemType.CheckOnClick = true;
            ItemType.FormattingEnabled = true;
            ItemType.ImeMode = ImeMode.Off;
            ItemType.Items.AddRange(new object[] { "None", "Bag", "Consumable", "Equipment", "Weapon" });
            ItemType.Location = new Point(0, 0);
            ItemType.Name = "ItemType";
            ItemType.Size = new Size(91, 94);
            ItemType.TabIndex = 0;
            ItemType.UseTabStops = false;
            ItemType.ItemCheck += ItemType_ItemCheck;
            // 
            // descriptionTab
            // 
            descriptionTab.Controls.Add(descriptionInput);
            descriptionTab.Location = new Point(4, 24);
            descriptionTab.Name = "descriptionTab";
            descriptionTab.Padding = new Padding(3);
            descriptionTab.Size = new Size(356, 286);
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
            itemMaxCountSetter.Location = new Point(434, 33);
            itemMaxCountSetter.Name = "itemMaxCountSetter";
            itemMaxCountSetter.Size = new Size(41, 23);
            itemMaxCountSetter.TabIndex = 5;
            itemMaxCountSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaxCountSetter.ValueChanged += itemMaxCountSetter_ValueChanged;
            // 
            // itemCountLabel
            // 
            itemCountLabel.AutoSize = true;
            itemCountLabel.Location = new Point(369, 35);
            itemCountLabel.Name = "itemCountLabel";
            itemCountLabel.Size = new Size(67, 15);
            itemCountLabel.TabIndex = 4;
            itemCountLabel.Text = "Max count:";
            // 
            // createItemButton
            // 
            createItemButton.Location = new Point(386, 330);
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
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
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
            saveDatapackToolStripMenuItem.Size = new Size(152, 22);
            saveDatapackToolStripMenuItem.Text = "Save Datapack";
            // 
            // loadDatapackToolStripMenuItem
            // 
            loadDatapackToolStripMenuItem.Name = "loadDatapackToolStripMenuItem";
            loadDatapackToolStripMenuItem.Size = new Size(152, 22);
            loadDatapackToolStripMenuItem.Text = "Load Datapack";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(152, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // OverrideItemButton
            // 
            OverrideItemButton.Location = new Point(684, 395);
            OverrideItemButton.Name = "OverrideItemButton";
            OverrideItemButton.Size = new Size(100, 24);
            OverrideItemButton.TabIndex = 10;
            OverrideItemButton.Text = "Override Item";
            OverrideItemButton.UseVisualStyleBackColor = true;
            OverrideItemButton.MouseHover += OverrideItemButton_MouseHover;
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
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabControl2.ResumeLayout(false);
            itemTypeTab.ResumeLayout(false);
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
        private TabPage tabPage2;
        private TabControl itemCreatorModifier;
        private TabPage createItemPage;
        private TabPage modItemPage;
        private Button createItemButton;
        private NumericUpDown itemMaxCountSetter;
        private Label itemCountLabel;
        private TabControl tabControl2;
        private TabPage itemTypeTab;
        private TabPage descriptionTab;
        private RichTextBox descriptionInput;
        private CheckedListBox ItemType;
        private NumericUpDown numericUpDown1;
        private Label GoldCostLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveDatapackToolStripMenuItem;
        private ToolStripMenuItem loadDatapackToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button EditItemButton;
        private Button OverrideItemButton;
        private ToolTip toolTip1;
    }
}
