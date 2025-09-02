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
            itemNameInput = new TextBox();
            items = new ListBox();
            itemNameLabel = new Label();
            itemSelectBoxLabel = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            itemCreatorModifier = new TabControl();
            createItemPage = new TabPage();
            itemMaxCountSetter = new NumericUpDown();
            itemCountLabel = new Label();
            createItemButton = new Button();
            modItemPage = new TabPage();
            tabPage2 = new TabPage();
            tabControl2 = new TabControl();
            itemTypeTab = new TabPage();
            descriptionTab = new TabPage();
            descriptionInput = new RichTextBox();
            checkedListBox1 = new CheckedListBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            itemCreatorModifier.SuspendLayout();
            createItemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).BeginInit();
            tabControl2.SuspendLayout();
            itemTypeTab.SuspendLayout();
            descriptionTab.SuspendLayout();
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
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(itemCreatorModifier);
            tabPage1.Controls.Add(itemSelectBoxLabel);
            tabPage1.Controls.Add(items);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // itemCreatorModifier
            // 
            itemCreatorModifier.Controls.Add(createItemPage);
            itemCreatorModifier.Controls.Add(modItemPage);
            itemCreatorModifier.Location = new Point(6, 6);
            itemCreatorModifier.Name = "itemCreatorModifier";
            itemCreatorModifier.SelectedIndex = 0;
            itemCreatorModifier.Size = new Size(483, 388);
            itemCreatorModifier.TabIndex = 4;
            // 
            // createItemPage
            // 
            createItemPage.Controls.Add(tabControl2);
            createItemPage.Controls.Add(itemMaxCountSetter);
            createItemPage.Controls.Add(itemCountLabel);
            createItemPage.Controls.Add(createItemButton);
            createItemPage.Controls.Add(itemNameInput);
            createItemPage.Controls.Add(itemNameLabel);
            createItemPage.Location = new Point(4, 24);
            createItemPage.Name = "createItemPage";
            createItemPage.Padding = new Padding(3);
            createItemPage.Size = new Size(475, 360);
            createItemPage.TabIndex = 0;
            createItemPage.Text = "Create Item";
            createItemPage.UseVisualStyleBackColor = true;
            // 
            // itemMaxCountSetter
            // 
            itemMaxCountSetter.Location = new Point(352, 8);
            itemMaxCountSetter.Name = "itemMaxCountSetter";
            itemMaxCountSetter.Size = new Size(41, 23);
            itemMaxCountSetter.TabIndex = 5;
            itemMaxCountSetter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            itemMaxCountSetter.ValueChanged += itemMaxCountSetter_ValueChanged;
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
            modItemPage.Size = new Size(475, 360);
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
            // tabControl2
            // 
            tabControl2.Controls.Add(itemTypeTab);
            tabControl2.Controls.Add(descriptionTab);
            tabControl2.Location = new Point(6, 40);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(368, 314);
            tabControl2.TabIndex = 6;
            // 
            // itemTypeTab
            // 
            itemTypeTab.Controls.Add(checkedListBox1);
            itemTypeTab.Location = new Point(4, 24);
            itemTypeTab.Name = "itemTypeTab";
            itemTypeTab.Padding = new Padding(3);
            itemTypeTab.Size = new Size(360, 286);
            itemTypeTab.TabIndex = 0;
            itemTypeTab.Text = "Item Type";
            itemTypeTab.UseVisualStyleBackColor = true;
            // 
            // descriptionTab
            // 
            descriptionTab.Controls.Add(descriptionInput);
            descriptionTab.Location = new Point(4, 24);
            descriptionTab.Name = "descriptionTab";
            descriptionTab.Padding = new Padding(3);
            descriptionTab.Size = new Size(360, 286);
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
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "None", "Bag", "Consumable", "Equipment", "Weapon" });
            checkedListBox1.Location = new Point(7, 9);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(91, 94);
            checkedListBox1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            itemCreatorModifier.ResumeLayout(false);
            createItemPage.ResumeLayout(false);
            createItemPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemMaxCountSetter).EndInit();
            tabControl2.ResumeLayout(false);
            itemTypeTab.ResumeLayout(false);
            descriptionTab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox itemNameInput;
        private ListBox items;
        private Label itemNameLabel;
        private Label itemSelectBoxLabel;
        private TabControl tabControl1;
        private TabPage tabPage1;
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
        private CheckedListBox checkedListBox1;
    }
}
