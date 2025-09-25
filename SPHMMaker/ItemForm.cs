using SPHMMaker.Items;
using SPHMMaker.Items.SubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SPHMMaker.Items.SubTypes.PotionData;

namespace SPHMMaker
{
    public partial class Form1
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement) //TODO: Move this
        {
            return sWhitespace.Replace(input, replacement);
        }

        private ItemData FoldDataIntoItem
        {
            get
            {
                int id = items.Items.Count;
                string name = itemNameInput.Text;
                string gfxName = itemNameInput.Text;
                string description = itemDescriptionInput.Text;
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
                        item = new ItemData(id, name, gfxName, description, maxStack, itemQuality, cost);
                        break;
                    case nameof(ItemData.ItemType.Bag):
                        item = new BagData(id, gfxName, name, description, (int)itemBagSizeSetter.Value, cost, itemQuality);
                        break;
                    case nameof(ItemData.ItemType.Consumable): //TODO
                        PotionData.PotionType[] potionTypes = itemPotionTypeSetter.CheckedIndices.Cast<PotionData.PotionType>().ToArray();
                        float[] minVal = GetPotionData(potionTypes.Length, "Minimum");
                        float[] maxVal = GetPotionData(potionTypes.Length, "Maximum");

                        item = new PotionData(id, gfxName, name, description, maxStack, potionTypes, itemQuality, cost, minVal, maxVal);
                        break;
                    case nameof(ItemData.ItemType.Equipment):
                        item = new EquipmentData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(itemTypeEquipmentTypeSetter.Text), (int)itemStatsArmorSetter.Value, stats, itemQuality, cost, Enum.Parse<EquipmentData.MaterialType>(itemEquipmentMaterialSetter.Text));
                        break;
                    case nameof(ItemData.ItemType.Weapon):
                        if (itemWeaponTypeSetter.SelectedIndex < Math.Log2((double)WeaponData.Weapon.Shield) )
                            item = new WeaponData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(ReplaceWhitespace(itemWeaponEQTypeSetter.Text, "")), (int)itemStatsArmorSetter.Value, stats, (int)itemMinimumDamageSetter.Value, (int)itemMaximumDamageSetter.Value, (float)itemAttackSpeedSetter.Value, itemQuality, Enum.Parse<WeaponData.Weapon>(ReplaceWhitespace(itemWeaponTypeSetter.Text, "")), cost);
                        else
                            item = new WeaponData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(ReplaceWhitespace(itemWeaponEQTypeSetter.Text, "")), (int)itemStatsArmorSetter.Value, stats, 0, 0, 0, itemQuality, Enum.Parse<WeaponData.Weapon>(ReplaceWhitespace(itemWeaponTypeSetter.Text, "")), cost);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                return item;
            }
        }


        public void InitializeItems()
        {
            ItemManager.SetListBox(items);
            itemTypeTabControl.ItemSize = new Size(0, 1);
            itemTypeEquipmentTypeSetter.SelectedIndex = 0;
            itemWeaponEQTypeSetter.SelectedIndex = 0;
            itemWeaponTypeSetter.SelectedIndex = 0;
            itemEquipmentMaterialSetter.SelectedIndex = 0;
        }


        float[] GetPotionData(int aLength, string aSearch)
        {
            float[] values = new float[aLength];

            for (int i = 0; i < values.Length; i++)
            {
                Control.ControlCollection c = itemPotionValueFlowLayout.Controls[i].Controls;
                for (int j = 0; j < c.Count; j++)
                {
                    if (c[j].Name.Contains(aSearch))
                    {
                        values[i] = (float)((NumericUpDown)c[j]).Value;
                        continue;
                    }
                }
            }

            return values;
        }

        void SetPotionData(PotionData.PotionType[] aPotionTypes, float[] aMinValues, float[] aMaxValues)
        {
            for (int i = 0; i < aPotionTypes.Length; i++)
            {
                Control.ControlCollection c = itemPotionValueFlowLayout.Controls[(int)aPotionTypes[i]].Controls;
                for (int j = 0; j < c.Count; j++)
                {
                    if (c[j].Name.Contains("Minimum"))
                    {
                        ((NumericUpDown)c[j]).Value = (decimal)aMinValues[i];
                        continue;
                    }
                    if (c[j].Name.Contains("Maximum"))
                    {
                        ((NumericUpDown)c[j]).Value = (decimal)aMaxValues[i];
                        continue;
                    }
                }
            }
        }



        private void itemPotionHealthMinimumSetter_ValueChanged(object sender, EventArgs e) => itemPotionHealthMaximumSetter.Minimum = ((NumericUpDown)sender).Value;
        private void itemPotionHealthMaximumSetter_ValueChanged(object sender, EventArgs e) => itemPotionHealthMinimumSetter.Maximum = ((NumericUpDown)sender).Value;
        private void itemPotionManaMinimumSetter_ValueChanged(object sender, EventArgs e) => itemPotionManaMaximumSetter.Minimum = ((NumericUpDown)sender).Value;
        private void itemPotionManaMaximumSetter_ValueChanged(object sender, EventArgs e) => itemPotionManaMinimumSetter.Maximum = ((NumericUpDown)sender).Value;
        private void itemPotionEnergyMinimumSetter_ValueChanged(object sender, EventArgs e) => itemPotionEnergyMaximumSetter.Minimum = ((NumericUpDown)sender).Value;
        private void itemPotionEnergyMaximumSetter_ValueChanged(object sender, EventArgs e) => itemPotionEnergyMinimumSetter.Maximum = ((NumericUpDown)sender).Value;


        private void itemPotionTypeSetter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //for (int i = 0; i < itemPotionValueFlowLayout.Controls.Count; i++)
            //{
            //    if (itemPotionTypeSetter.GetItemChecked(i) == true)
            //    {
            //        itemPotionValueFlowLayout.Controls[i].Visible = true;
            //    }
            //    else
            //    {
            //        itemPotionValueFlowLayout.Controls[i].Visible = false;
            //    }
            //}

            if (e.NewValue == CheckState.Unchecked) itemPotionValueFlowLayout.Controls[e.Index].Visible = false;
            else itemPotionValueFlowLayout.Controls[e.Index].Visible = true;


        }

        private void itemMinimumDamageSetter_ValueChanged(object sender, EventArgs e) => RecalcDummyDps();
        private void itemAttackSpeedSetter_ValueChanged(object sender, EventArgs e) => RecalcDummyDps();
        private void itemMaxDamageSetter_ValueChanged(object sender, EventArgs e) => RecalcDummyDps();
        private void RecalcDummyDps()
        {
            float avgHit = (float)(itemMinimumDamageSetter.Value + itemMaximumDamageSetter.Value) / 2;
            itemDummyDpsChangingLabel.Text = (avgHit / (float)itemAttackSpeedSetter.Value).ToString();
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

                int itemTypeIndex = itemTypeSelector.FindString(item.TypeName);
                if (itemTypeIndex == -1) itemTypeSelector.SetItemChecked(0, true);
                else itemTypeSelector.SetItemChecked(itemTypeIndex, true);

                int itemQualityIndex = itemQualitySelector.FindString(item.Quality.ToString());
                if (itemQualityIndex == -1) throw new Exception();
                else itemQualitySelector.SetItemChecked(itemQualityIndex, true);

                itemDescriptionInput.Text = item.Description;

                if (item is BagData bagData)
                {
                    itemBagSizeSetter.Value = bagData.SlotCount;
                    return;
                }

                if (item is EquipmentData equipmentData)
                {
                    itemStatsAgilitySetter.Value = equipmentData.Agility;
                    itemStatsStrengthSetter.Value = equipmentData.Strength;
                    itemStatsStaminaSetter.Value = equipmentData.Stamina;
                    itemStatsIntelligenceSetter.Value = equipmentData.Intelligence;
                    itemStatsSpiritSetter.Value = equipmentData.Spirit;
                    itemStatsArmorSetter.Value = equipmentData.Armor;

                    if (item is EquipmentData equipmentOnlyData && item is not WeaponData)
                    {
                        itemTypeEquipmentTypeSetter.SelectedIndex = itemTypeEquipmentTypeSetter.Items.IndexOf(equipmentOnlyData.Slot.ToString());
                        itemEquipmentMaterialSetter.SelectedIndex = itemEquipmentMaterialSetter.Items.IndexOf(equipmentOnlyData.Material.ToString());

                    }

                    if (item is WeaponData weaponData)
                    {
                        var v = itemWeaponEQTypeSetter.Items;

                        for (int i = 0; i < v.Count; i++)
                        {
                            string s = ReplaceWhitespace(v[i].ToString(), "");

                            if (s == weaponData.Hand.ToString())
                            {
                                itemWeaponEQTypeSetter.SelectedIndex = i;
                                break;
                            }
                        }

                        v = itemWeaponTypeSetter.Items;
                        for (int i = 0; i < v.Count; i++)
                        {
                            string s = ReplaceWhitespace(v[i].ToString(), "");

                            if (s == weaponData.WeaponType.ToString())
                            {
                                itemWeaponTypeSetter.SelectedIndex = i;
                                break;
                            }
                        }

                        if (weaponData.WeaponType < WeaponData.Weapon.Shield)
                        {
                            WeaponData.Attack attack = weaponData.GetAttack;

                            itemMinimumDamageSetter.Value = attack.MinAttackDamage;
                            itemMaximumDamageSetter.Value = attack.MaxAttackDamage;
                            itemAttackSpeedSetter.Value = (decimal)attack.AttackSpeed;
                        }
                        return;
                    }
                }

                if (item is ConsumableData consumableData)
                {
                    if (consumableData is PotionData potionData)
                    {
                        IEnumerable<int> checkedI = itemPotionTypeSetter.CheckedIndices.Cast<int>();

                        checkedI = checkedI.Where(x => !potionData.Type.Contains((PotionData.PotionType)x));

                        for (int i = checkedI.Count() - 1; i >= 0; i--)
                        {
                            itemPotionTypeSetter.SetItemChecked(checkedI.ElementAt(i), false);
                        }

                        IEnumerable<PotionData.PotionType> iToSet = potionData.Type.Where(x => itemPotionTypeSetter.CheckedIndices.Contains((int)x) == false);

                        for (int i = 0; i < potionData.Type.Length; i++)
                        {
                            itemPotionTypeSetter.SetItemChecked((int)potionData.Type[i], true);
                            
                        }

                        SetPotionData(potionData.Type, potionData.Value, potionData.MaxValue);
                    }
                    

                    return;
                }

                //MessageBox.Show(index.ToString());
            }
        }

        private void itemWeaponTypeSetter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemWeaponTypeSetter.SelectedIndex >= itemWeaponTypeSetter.Items.IndexOf("Shield"))
            {
                itemMinimumDamageLabel.Visible = false;
                itemMinimumDamageSetter.Visible = false;
                itemMaximumDamageLabel.Visible = false;
                itemMaximumDamageSetter.Visible = false;
                itemAttackSpeedLabel.Visible = false;
                itemAttackSpeedSetter.Visible = false;
                itemDummyDpsChangingLabel.Visible = false;
                itemDummyDpsLabel.Visible = false;
            }
            else
            {
                itemMinimumDamageLabel.Visible = true;
                itemMinimumDamageSetter.Visible = true;
                itemMaximumDamageLabel.Visible = true;
                itemMaximumDamageSetter.Visible = true;
                itemAttackSpeedLabel.Visible = true;
                itemAttackSpeedSetter.Visible = true;
                itemDummyDpsChangingLabel.Visible = true;
                itemDummyDpsLabel.Visible = true;
            }

        }
    }
}
