namespace SPHMMaker
{
    partial class NewCanvasDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            widthNumeric = new NumericUpDown();
            heightNumeric = new NumericUpDown();
            widthLabel = new Label();
            heightLabel = new Label();
            okButton = new Button();
            cancelButton = new Button();
            presetLabel = new Label();
            presetComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)widthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).BeginInit();
            SuspendLayout();
            // 
            // widthNumeric
            // 
            widthNumeric.Location = new Point(94, 16);
            widthNumeric.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            widthNumeric.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            widthNumeric.Name = "widthNumeric";
            widthNumeric.Size = new Size(120, 23);
            widthNumeric.TabIndex = 0;
            widthNumeric.Value = new decimal(new int[] { 256, 0, 0, 0 });
            widthNumeric.ValueChanged += widthNumeric_ValueChanged;
            // 
            // heightNumeric
            // 
            heightNumeric.Location = new Point(94, 54);
            heightNumeric.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            heightNumeric.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            heightNumeric.Name = "heightNumeric";
            heightNumeric.Size = new Size(120, 23);
            heightNumeric.TabIndex = 1;
            heightNumeric.Value = new decimal(new int[] { 256, 0, 0, 0 });
            heightNumeric.ValueChanged += heightNumeric_ValueChanged;
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(12, 18);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(39, 15);
            widthLabel.TabIndex = 2;
            widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Location = new Point(12, 56);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(43, 15);
            heightLabel.TabIndex = 3;
            heightLabel.Text = "Height";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(58, 137);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 27);
            okButton.TabIndex = 3;
            okButton.Text = "Create";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(139, 137);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 27);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // presetLabel
            // 
            presetLabel.AutoSize = true;
            presetLabel.Location = new Point(12, 94);
            presetLabel.Name = "presetLabel";
            presetLabel.Size = new Size(38, 15);
            presetLabel.TabIndex = 6;
            presetLabel.Text = "Preset";
            // 
            // presetComboBox
            // 
            presetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            presetComboBox.FormattingEnabled = true;
            presetComboBox.Items.AddRange(new object[] { "32 x 32", "64 x 64", "128 x 128", "256 x 256", "512 x 512", "1024 x 1024" });
            presetComboBox.Location = new Point(94, 91);
            presetComboBox.Name = "presetComboBox";
            presetComboBox.Size = new Size(120, 23);
            presetComboBox.TabIndex = 2;
            presetComboBox.SelectedIndexChanged += presetComboBox_SelectedIndexChanged;
            // 
            // NewCanvasDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(226, 176);
            Controls.Add(presetComboBox);
            Controls.Add(presetLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(heightLabel);
            Controls.Add(widthLabel);
            Controls.Add(heightNumeric);
            Controls.Add(widthNumeric);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewCanvasDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Canvas";
            ((System.ComponentModel.ISupportInitialize)widthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown widthNumeric;
        private NumericUpDown heightNumeric;
        private Label widthLabel;
        private Label heightLabel;
        private Button okButton;
        private Button cancelButton;
        private Label presetLabel;
        private ComboBox presetComboBox;
    }
}
