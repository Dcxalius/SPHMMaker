using System.Runtime.InteropServices;
using System.Windows.Forms;
using SPHMMaker.Items;

namespace SPHMMaker
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;


        public Form1()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
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
    }
}
