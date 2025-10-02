using System.Drawing;
using System.Windows.Forms;

namespace SPHMMaker
{
    internal class SpriteSizeDialog : Form
    {
        private readonly NumericUpDown widthInput;
        private readonly NumericUpDown heightInput;
        private readonly Button okButton;
        private readonly Button cancelButton;

        public int SpriteWidth => (int)widthInput.Value;
        public int SpriteHeight => (int)heightInput.Value;

        public SpriteSizeDialog(int currentWidth, int currentHeight)
        {
            Text = "Sprite Size";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(260, 140);

            var widthLabel = new Label
            {
                Text = "Width:",
                Location = new Point(12, 15),
                AutoSize = true
            };

            widthInput = new NumericUpDown
            {
                Location = new Point(120, 12),
                Minimum = 1,
                Maximum = 1024,
                Value = currentWidth,
                Width = 120
            };

            var heightLabel = new Label
            {
                Text = "Height:",
                Location = new Point(12, 50),
                AutoSize = true
            };

            heightInput = new NumericUpDown
            {
                Location = new Point(120, 48),
                Minimum = 1,
                Maximum = 1024,
                Value = currentHeight,
                Width = 120
            };

            okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(84, 96),
                Width = 75
            };

            cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(165, 96),
                Width = 75
            };

            Controls.Add(widthLabel);
            Controls.Add(widthInput);
            Controls.Add(heightLabel);
            Controls.Add(heightInput);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            AcceptButton = okButton;
            CancelButton = cancelButton;
        }
    }
}
