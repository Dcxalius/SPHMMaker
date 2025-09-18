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

            ItemManager.SetListBox(items);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void items_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = items.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                editingItem = items.SelectedIndex;
                ItemData item = ItemManager.GetItemById(items.SelectedIndex);
                goldCostCounter.Value = item.Cost;
                itemMaxCountSetter.Value = item.MaxStack;
                itemNameInput.Text = item.Name;

                int itemTypeIndex = itemTypeSelector.FindString(item.Type.ToString());
                if (itemTypeIndex == -1) itemTypeSelector.SetItemChecked(0, true);
                else itemTypeSelector.SetItemChecked(itemTypeIndex, true);

                int itemQualityIndex = itemQualitySelector.FindString(item.Quality.ToString());
                if (itemQualityIndex == -1) throw new Exception();
                else itemQualitySelector.SetItemChecked(itemTypeIndex, true);

                descriptionInput.Text = item.Description;

                //MessageBox.Show(index.ToString());
            }
        }

        private void createItemButton_Click(object sender, EventArgs e)
        {
            ItemData item;
            string checkedType = itemTypeSelector.CheckedItems.Cast<string>().First();

            int id = items.Items.Count;
            string name = itemNameInput.Text;
            string gfxName = itemNameInput.Text;
            string description = descriptionInput.Text;
            int maxStack = (int)itemMaxCountSetter.Value;
            string checkRarity = itemQualitySelector.CheckedItems.Cast<string>().First();
            ItemData.ItemQuality itemQuality = Enum.Parse<ItemData.ItemQuality>(checkRarity);
            int cost = (int)goldCostCounter.Value;

            {
                bool err = false;
                //Check for existing ID to be safe?
                if (!(name.Length > 0))
                {
                    err = true;
                    MessageBox.Show("Name cannot be empty.");
                }
                //if (!(gfxName.Length > 0)) MessageBox.Show("GfxName cannot be empty");
                if (!(description.Length > 0))
                {
                    err = true;
                    MessageBox.Show("Description cannot be empty.");
                }

                if (maxStack < 0 || maxStack > int.MaxValue)
                {
                    err = true;
                    MessageBox.Show("Invalid Max Stack");
                }

                if (cost < 0)
                {
                    err = true;
                    MessageBox.Show("Invalid cost set.");
                }
                if (err)
                {
                    MessageBox.Show("Error in created item found, exiting without creating.");
                    return;
                }
            }

            string i = ItemData.ItemType.None.ToString();
            switch (checkedType)
            {
                case nameof(ItemData.ItemType.None):
                    item = new ItemData(id, name, gfxName, description, maxStack, ItemData.ItemType.None, itemQuality, cost);
                    break;
                case nameof(ItemData.ItemType.Bag):
                    item = new BagData(id, gfxName, name, description, 0 /* fix */ , cost, itemQuality);
                    break;
                case nameof(ItemData.ItemType.Consumable):
                    item = new ConsumableData(id, gfxName, name, description, maxStack, ConsumableData.ConsumableType.NONE, itemQuality, cost);
                    break;
                case nameof(ItemData.ItemType.Equipment):
                    item = new EquipmentData(id, gfxName, name, description, EquipmentData.EQType.Count, 0, [0, 0, 0, 0, 0], itemQuality, cost, EquipmentData.GearType.Count);
                    break;
                case nameof(ItemData.ItemType.Weapon):
                    item = new WeaponData(id, gfxName, name, description, EquipmentData.EQType.Count, 0, [0, 0, 0, 0, 0], 0, 0, 0, itemQuality, WeaponData.Weapon.None, cost);
                    break;

                default:
                    throw new NotImplementedException();
            }

            ItemManager.CreateItem(item);
        }

        private void itemMaxCountSetter_ValueChanged(object sender, EventArgs e)
        {
            if (itemMaxCountSetter.Value > 0) return;
            itemMaxCountSetter.Value = 1;
        }

        private void OverrideItemButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(OverrideItemButton, "This removes and replaces the select item with the created item.");
        }

        private void itemTypeSelector_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            switch (e.Item.Name)
            {
                case nameof(ItemData.ItemType.None):
                    itemTypeChangingTab.SelectedTab = itemTypeNoneTab;
                    break;
                case nameof(ItemData.ItemType.Bag):
                    itemTypeChangingTab.SelectedTab = itemTypeBagTab;

                    break;
                default:
                    break;
            }
        }
    }
}
