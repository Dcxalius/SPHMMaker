using System.Windows.Forms;

namespace SPHMMaker
{
    public partial class NewCanvasDialog : Form
    {
        public int CanvasWidth => (int)widthNumeric.Value;
        public int CanvasHeight => (int)heightNumeric.Value;

        public NewCanvasDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void presetComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (presetComboBox.SelectedItem is string preset)
            {
                string[] parts = preset.Split('x', 'X');
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int width) && int.TryParse(parts[1].Trim(), out int height))
                {
                    widthNumeric.Value = Math.Clamp(width, (int)widthNumeric.Minimum, (int)widthNumeric.Maximum);
                    heightNumeric.Value = Math.Clamp(height, (int)heightNumeric.Minimum, (int)heightNumeric.Maximum);
                }
            }
        }

        private void widthNumeric_ValueChanged(object? sender, EventArgs e)
        {
            ValidateAspectPreset();
        }

        private void heightNumeric_ValueChanged(object? sender, EventArgs e)
        {
            ValidateAspectPreset();
        }

        private void ValidateAspectPreset()
        {
            string comboValue = $"{CanvasWidth} x {CanvasHeight}";
            int index = presetComboBox.FindStringExact(comboValue);
            if (index >= 0)
            {
                presetComboBox.SelectedIndex = index;
            }
            else
            {
                presetComboBox.SelectedIndex = -1;
            }
        }
    }
}
