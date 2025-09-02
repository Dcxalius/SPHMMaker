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

            AllocConsole();

            items.DataSource = ItemManager.Items;
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
    }
}
