using System.Runtime.InteropServices;
using SPHMMaker.Items;

namespace SPHMMaker
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();

            ItemType.SetItemChecked(0, true);

            AllocConsole();

            items.DataSource = ItemManager.ItemNames;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void items_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void items_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = items.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                MessageBox.Show(index.ToString());
            }
        }

        private void createItemButton_Click(object sender, EventArgs e)
        {
            //Item item = new Item(itemNameInput.Text, itemMaxCountSetter.Value)
            //string name = ;
        }

        private void itemMaxCountSetter_ValueChanged(object sender, EventArgs e)
        {
            if (itemMaxCountSetter.Value > 0) return;
            itemMaxCountSetter.Value = 1;
        }

        private void ItemType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox.CheckedIndexCollection n = ItemType.CheckedIndices;
            if (n.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = CheckState.Checked;
                return;
            }


            int[] indexes = new int[n.Count];

            for (int i = 0; i < n.Count; i++)
            {
                indexes[i] = n[i];
            }

            //ItemType.SetItemChecked()
            foreach (int i in indexes)
            {
                if (i == e.Index) continue;

                ItemType.ItemCheck -= ItemType_ItemCheck;
                ItemType.SetItemCheckState(i, CheckState.Unchecked);
                ItemType.ItemCheck += ItemType_ItemCheck;
            }
        }

        private void OverrideItemButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(OverrideItemButton, "This removes and replaces the select item with the created item.");
        }
    }
}
