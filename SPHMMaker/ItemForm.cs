using SPHMMaker.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker
{
    public partial class Form1
    {
        public void InitializeItems()
        {
            ItemManager.SetListBox(items);
            itemTypeTabControl.ItemSize = new Size(0, 1);
            itemTypeEquipmentTypeSetter.SelectedIndex = 0;
        }

        private ItemData FoldDataIntoItem
        {
            get
            {
                int id = items.Items.Count;
                string name = itemNameInput.Text;
                string gfxName = itemNameInput.Text;
                string description = descriptionInput.Text;
                int maxStack = (int)itemMaxCountSetter.Value;
                ItemData.ItemQuality itemQuality = Enum.Parse<ItemData.ItemQuality>(itemQualitySelector.Items[itemQualitySelector.GetSingleCheckedIndex.Value].ToString());
                int[] stats = [(int)itemStatsAgilitySetter.Value, (int)itemStatsStrengthSetter.Value, (int)itemStatsStaminaSetter.Value, (int)itemStatsIntelligenceSetter.Value, (int)itemStatsSpiritSetter.Value];
                int cost = (int)goldCostCounter.Value;

                {
                    string s = string.Empty;
                    if (!ItemManager.FreeIdCheck(id)) s += "Duplicated Id found, you've fucked something up majorly and item creation won't work until you fixed this. \n";
                    //Check for existing ID to be safe?
                    if (!(name.Length > 0)) s += "Name cannot be empty. \n";
                    //if (!(gfxName.Length > 0)) MessageBox.Show("GfxName cannot be empty");
                    if (!(description.Length > 0)) s += "Description cannot be empty.\n";
                    if (s != string.Empty)
                    {
                        MessageBox.Show("Error in created item found, exiting without creating.");
                        return null;
                    }
                }

                ItemData item;

                switch (itemTypeSelector.Items[itemTypeSelector.GetSingleCheckedIndex.Value].ToString())
                {
                    case nameof(ItemData.ItemType.None):
                        item = new ItemData(id, name, gfxName, description, maxStack, ItemData.ItemType.None, itemQuality, cost);
                        break;
                    case nameof(ItemData.ItemType.Bag):
                        item = new BagData(id, gfxName, name, description, (int)bagSizeSetter.Value, cost, itemQuality);
                        break;
                    case nameof(ItemData.ItemType.Consumable):
                        item = new ConsumableData(id, gfxName, name, description, maxStack, ConsumableData.ConsumableType.NONE, itemQuality, cost);
                        break;
                    case nameof(ItemData.ItemType.Equipment):
                        item = new EquipmentData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(itemTypeEquipmentTypeSetter.Text), (int)itemStatsArmorSetter.Value, stats, itemQuality, cost, EquipmentData.GearType.Count);
                        break;
                    case nameof(ItemData.ItemType.Weapon):
                        item = new WeaponData(id, gfxName, name, description, EquipmentData.EQType.Count, (int)itemStatsArmorSetter.Value, stats, 0, 0, 0, itemQuality, WeaponData.Weapon.None, cost);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                return item;
            }
        }

        private void createItemButton_Click(object sender, EventArgs e)
        {
            ItemData item = FoldDataIntoItem;

            if (item != null) ItemManager.CreateItem(item);
        }


        private void OverrideItemButton_Click(object sender, EventArgs e)
        {
            ItemData item = FoldDataIntoItem;
            item.ID = items.SelectedIndex;

            if (item != null) ItemManager.OverrideItem(items.SelectedIndex, item);
        }

        private void OverrideItemButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(OverrideItemButton, "This removes and replaces the selected item with the created item.");
        }

        private void itemTypeSelector_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked) return;
            if (sender is not ExtendedForm.ExtendedCheckedListBox list) throw new ArgumentNullException();
            switch (list.Items[e.Index])
            {
                case nameof(ItemData.ItemType.None):
                    itemTypeTabControl.SelectedTab = itemTypeNoneTab;
                    break;
                case nameof(ItemData.ItemType.Bag):
                    itemTypeTabControl.SelectedTab = itemTypeBagTab;
                    break;
                case nameof(ItemData.ItemType.Consumable):
                    itemTypeTabControl.SelectedTab = itemTypeConsumableTab;
                    break;
                case nameof(ItemData.ItemType.Equipment):
                    itemTypeTabControl.SelectedTab = itemTypeEquipmentTab;
                    itemTypeEquipmentTab.Controls.Add(itemStats);
                    break;
                case nameof(ItemData.ItemType.Weapon):
                    itemTypeTabControl.SelectedTab = itemTypeWeaponTab;
                    itemTypeWeaponTab.Controls.Add(itemStats);
                    break;
                default:
                    break;
            }
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

        private void items_MouseDoubleClick(object sender, MouseEventArgs e) => SetItemTo(items.IndexFromPoint(e.Location));
        private void EditItemButton_Click(object sender, EventArgs e) => SetItemTo(items.SelectedIndex);
        private void SetItemTo(int aIndex)
        {
            //TODO: Check if anything has been changed and ask the user if they want to discard the changes 

            if (aIndex != ListBox.NoMatches)
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

                if (item is EquipmentData equipmentData)
                {
                    itemStatsAgilitySetter.Value = equipmentData.Agility;
                    itemStatsStrengthSetter.Value = equipmentData.Strength;
                    itemStatsStaminaSetter.Value = equipmentData.Stamina;
                    itemStatsIntelligenceSetter.Value = equipmentData.Intelligence;
                    itemStatsSpiritSetter.Value = equipmentData.Spirit;
                    itemStatsArmorSetter.Value = equipmentData.Armor;
                }

                if (item is EquipmentData equipmentOnlyData && item is not WeaponData)
                {
                    itemTypeEquipmentTypeSetter.SelectedIndex = itemTypeEquipmentTypeSetter.Items.IndexOf(equipmentOnlyData.Slot.ToString());

                }

                //MessageBox.Show(index.ToString());
            }
        }
    }
}
