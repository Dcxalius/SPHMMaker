using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SPHMMaker.Items;
using SPHMMaker.Items.Effects;

namespace SPHMMaker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;
        readonly BindingList<EffectData> effectLibrary = new();
        readonly BindingList<EffectData> currentItemEffects = new();
        EffectData? editingEffect;


        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeEffectSystem();
            InitializeItems();
        }
        void InitializeEffectSystem()
        {
            effectTypeSelector.DataSource = Enum.GetValues(typeof(EffectData.EffectType));
            effectTargetSelector.DataSource = Enum.GetValues(typeof(EffectData.EffectTarget));

            effectLibraryListBox.DataSource = effectLibrary;
            itemEffectsListBox.DataSource = currentItemEffects;

            effectLibraryListBox.ClearSelected();
            if (effectTypeSelector.Items.Count > 0)
            {
                effectTypeSelector.SelectedIndex = 0;
            }

            if (effectTargetSelector.Items.Count > 0)
            {
                effectTargetSelector.SelectedIndex = 0;
            }

            ClearEffectEditor();
        }

        void ClearEffectEditor()
        {
            effectNameInput.Text = string.Empty;
            effectDescriptionInput.Text = string.Empty;
            editingEffect = null;
            ResetNumeric(effectMagnitudeInput, 0m);
            ResetNumeric(effectDurationInput, 0m);

            if (effectTypeSelector.Items.Count > 0)
            {
                effectTypeSelector.SelectedIndex = 0;
            }

            if (effectTargetSelector.Items.Count > 0)
            {
                effectTargetSelector.SelectedIndex = 0;
            }
        }

        void PopulateEffectEditor(EffectData effect)
        {
            effectNameInput.Text = effect.Name;
            effectDescriptionInput.Text = effect.Description;
            effectTypeSelector.SelectedItem = effect.Type;
            effectTargetSelector.SelectedItem = effect.Target;
            ResetNumeric(effectMagnitudeInput, (decimal)effect.Magnitude);
            ResetNumeric(effectDurationInput, (decimal)effect.Duration);
            editingEffect = effect;
        }

        static void ResetNumeric(NumericUpDown control, decimal value)
        {
            if (value < control.Minimum)
            {
                control.Value = control.Minimum;
                return;
            }

            if (value > control.Maximum)
            {
                control.Value = control.Maximum;
                return;
            }

            control.Value = value;
        }

        bool TryBuildEffectFromEditor(out EffectData effect, bool showErrors = true)
        {
            effect = null!;

            string name = effectNameInput.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                if (showErrors)
                {
                    MessageBox.Show("Effect name cannot be empty.", "Effects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return false;
            }

            if (effectTypeSelector.SelectedItem is not EffectData.EffectType type)
            {
                if (showErrors)
                {
                    MessageBox.Show("Select an effect type before saving.", "Effects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return false;
            }

            if (effectTargetSelector.SelectedItem is not EffectData.EffectTarget target)
            {
                if (showErrors)
                {
                    MessageBox.Show("Select an effect target before saving.", "Effects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return false;
            }

            effect = new EffectData(
                name,
                effectDescriptionInput.Text.Trim(),
                type,
                target,
                (float)effectMagnitudeInput.Value,
                (float)effectDurationInput.Value);

            return true;
        }

        void LoadEffectsFromItem(ItemData item)
        {
            currentItemEffects.Clear();

            foreach (EffectData effect in item.Effects)
            {
                currentItemEffects.Add(effect.Clone());
            }
        }

        IEnumerable<EffectData> CloneCurrentEffects()
        {
            return currentItemEffects.Select(effect => effect.Clone()).ToArray();
        }
        private void loadDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? path = GetDirectory();

            if (path is null)
            {
                return;
            }

            //TODO: Check access?
            //DirectoryInfo di = new DirectoryInfo(path);
            //di.GetAccessControl().GetAccessRules();

            string[] foldersThatShouldBeHere = ["Items"];

            string[] folders = Directory.GetDirectories(path);


            foreach (string folder in foldersThatShouldBeHere)
            {
                if (!folders.Contains(path + "\\" + folder))
                {
                    MessageBox.Show($"Error, {folder} is not found");
                    return;
                }
            }

            ItemManager.Load(path + "\\Items");
        }

        private void saveDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? s = GetDirectory();

            if (s is null)
            {
                MessageBox.Show("Save aborted.");
                return;
            }

            //ItemManager.Save(s);
        }

        string? GetDirectory()
        {
            var fbg = new FolderBrowserDialog()
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbg.ShowDialog() != DialogResult.OK)
                return null;

            return fbg.SelectedPath;
        }

        private void itemCheckGeneratedTooltip_Click(object sender, EventArgs e)
        {
            ItemData item = FoldDataIntoItem;
            string tooltip = itemNameInput.Text;
            tooltip += "\n";
            tooltip += itemDescriptionInput.Text;
            tooltip += "\n";

            switch (itemTypeSelector.GetSingleCheckedIndexName)
            {
                case "Bag":
                    tooltip += "Bag slots: " + itemBagSizeSetter.Value;
                    break;
                case "Consumable":
                    //TODO: Check what type and do stuff
                    tooltip += "Not finished yet xdd";
                    break;
                case "Equipment":
                    if(itemEquipmentMaterialSetter.Text != EquipmentData.MaterialType.None.ToString())
                    {
                        tooltip += itemEquipmentMaterialSetter.Text;
                        tooltip += "\n";
                    }
                    tooltip += ((EquipmentData)item).StatReport;
                    break;
                case "Weapon":
                    tooltip += ((WeaponData)item).StatReport;
                    tooltip += "\n";
                    tooltip += ((WeaponData)item).GetAttack;
                    break;
                case "None":
                    break;
                default:
                    throw new NotImplementedException();
            }


            MessageBox.Show(tooltip);
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

        void effectLibraryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (effectLibraryListBox.SelectedItem is EffectData effect)
            {
                PopulateEffectEditor(effect);
                return;
            }

            ClearEffectEditor();
        }

        void effectNewButton_Click(object sender, EventArgs e)
        {
            effectLibraryListBox.ClearSelected();
            ClearEffectEditor();
            effectNameInput.Focus();
        }

        void effectSaveButton_Click(object sender, EventArgs e)
        {
            if (!TryBuildEffectFromEditor(out EffectData effect))
            {
                return;
            }

            int duplicateIndex = FindEffectIndexByName(effect.Name, editingEffect);
            if (duplicateIndex >= 0)
            {
                MessageBox.Show("An effect with that name already exists.", "Effects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (editingEffect != null)
            {
                int index = effectLibrary.IndexOf(editingEffect);
                if (index >= 0)
                {
                    effectLibrary[index] = effect;
                    editingEffect = effectLibrary[index];
                    effectLibraryListBox.SelectedIndex = index;
                    return;
                }
            }

            effectLibrary.Add(effect);
            editingEffect = effect;
            effectLibraryListBox.SelectedItem = effect;
        }

        void effectDeleteButton_Click(object sender, EventArgs e)
        {
            if (effectLibraryListBox.SelectedItem is not EffectData effect)
            {
                return;
            }

            if (MessageBox.Show($"Delete effect '{effect.Name}'?", "Delete Effect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            effectLibrary.Remove(effect);

            for (int i = currentItemEffects.Count - 1; i >= 0; i--)
            {
                if (currentItemEffects[i].Equals(effect))
                {
                    currentItemEffects.RemoveAt(i);
                }
            }

            ClearEffectEditor();
        }

        void effectAddToItemButton_Click(object sender, EventArgs e)
        {
            EffectData? effectToAdd = null;

            if (effectLibraryListBox.SelectedItem is EffectData selectedEffect)
            {
                effectToAdd = selectedEffect.Clone();
            }
            else if (TryBuildEffectFromEditor(out EffectData effect, false))
            {
                effectToAdd = effect;
            }

            if (effectToAdd == null)
            {
                MessageBox.Show("Select an effect from the library or fill in the effect editor before adding it to the item.", "Effects", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            currentItemEffects.Add(effectToAdd);
            itemEffectsListBox.SelectedIndex = currentItemEffects.Count - 1;
        }

        void effectRemoveFromItemButton_Click(object sender, EventArgs e)
        {
            int index = itemEffectsListBox.SelectedIndex;
            if (index < 0 || index >= currentItemEffects.Count)
            {
                return;
            }

            currentItemEffects.RemoveAt(index);
        }

        int FindEffectIndexByName(string name, EffectData? ignored)
        {
            for (int i = 0; i < effectLibrary.Count; i++)
            {
                EffectData candidate = effectLibrary[i];
                if (candidate.Equals(ignored))
                {
                    continue;
                }

                if (string.Equals(candidate.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
