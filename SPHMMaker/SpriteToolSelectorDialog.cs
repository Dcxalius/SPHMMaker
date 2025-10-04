using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPHMMaker
{
    internal class SpriteToolSelectorDialog : Form
    {
        private readonly ListBox toolListBox;
        private readonly Button okButton;
        private readonly Button cancelButton;
        private readonly SpriteEditorForm.SpriteTool defaultTool;

        public SpriteEditorForm.SpriteTool SelectedTool
        {
            get
            {
                if (toolListBox.SelectedItem is SpriteEditorForm.SpriteTool tool)
                {
                    return tool;
                }

                return defaultTool;
            }
        }

        public SpriteToolSelectorDialog(SpriteEditorForm.SpriteTool currentTool)
        {
            defaultTool = currentTool;

            Text = "Select Tool";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(260, 220);

            var titleLabel = new Label
            {
                Text = "Choose a tool:",
                Location = new Point(12, 12),
                AutoSize = true
            };

            toolListBox = new ListBox
            {
                Location = new Point(12, 36),
                Size = new Size(236, 130),
                IntegralHeight = false,
                BorderStyle = BorderStyle.FixedSingle
            };
            toolListBox.Items.AddRange(Enum.GetValues(typeof(SpriteEditorForm.SpriteTool)).Cast<object>().ToArray());
            toolListBox.SelectedItem = currentTool;
            toolListBox.DoubleClick += (_, _) =>
            {
                if (toolListBox.SelectedItem != null)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(92, 180),
                Width = 75
            };

            cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(173, 180),
                Width = 75
            };

            Controls.Add(titleLabel);
            Controls.Add(toolListBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            AcceptButton = okButton;
            CancelButton = cancelButton;
        }
    }
}
