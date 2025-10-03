using SPHMMaker.Items;
using SPHMMaker.Items.SubTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static SPHMMaker.Items.SubTypes.PotionData;

namespace SPHMMaker
{
    public partial class MainForm
    {
        const int ItemImageSize = 48;
        const int ItemHorizontalPadding = 8;
        const int ItemVerticalPadding = 4;
        static readonly string[] SupportedImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };
        static readonly IReadOnlyDictionary<ItemData.ItemQuality, Color> ItemQualityColors = new Dictionary<ItemData.ItemQuality, Color>
        {
            { ItemData.ItemQuality.Poor, Color.DimGray },
            { ItemData.ItemQuality.Common, Color.WhiteSmoke },
            { ItemData.ItemQuality.Uncommon, Color.MediumSeaGreen },
            { ItemData.ItemQuality.Rare, Color.RoyalBlue },
            { ItemData.ItemQuality.Epic, Color.MediumPurple },
            { ItemData.ItemQuality.Legendary, Color.Orange }
        };

        readonly Dictionary<string, Image> itemImageCache = new();
        readonly Image defaultItemImage;
        bool imagesDisposed;
        int editingItem = -1;

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement) //TODO: Move this
        {
            return sWhitespace.Replace(input, replacement);
        }

        private ItemData? FoldDataIntoItem()
        {
            int id = items.Items.Count;
            string name = itemNameInput.Text;
            string gfxName = itemNameInput.Text;
            string description = itemDescriptionInput.Text;
            int maxStack = (int)itemMaxCountSetter.Value;
            ItemData.ItemQuality itemQuality = Enum.Parse<ItemData.ItemQuality>(itemQualitySelector.Items[itemQualitySelector.GetSingleCheckedIndex.Value].ToString());
            int[] stats =
            {
                (int)itemStatsAgilitySetter.Value,
                (int)itemStatsStrengthSetter.Value,
                (int)itemStatsStaminaSetter.Value,
                (int)itemStatsIntelligenceSetter.Value,
                (int)itemStatsSpiritSetter.Value
            };
            int cost = (int)goldCostCounter.Value;

            string validationErrors = string.Empty;
            if (!ItemManager.FreeIdCheck(id))
            {
                validationErrors += "Duplicated Id found, you've fucked something up majorly and item creation won't work until you fixed this. \n";
            }

            if (name.Length == 0)
            {
                validationErrors += "Name cannot be empty. \n";
            }

            if (description.Length == 0)
            {
                validationErrors += "Description cannot be empty.\n";
            }

            if (validationErrors != string.Empty)
            {
                MessageBox.Show("Error in created item found, exiting without creating.");
                return null;
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
                    if (itemWeaponTypeSetter.SelectedIndex < Math.Log2((double)WeaponData.Weapon.Shield))
                    {
                        item = new WeaponData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(ReplaceWhitespace(itemWeaponEQTypeSetter.Text, "")), (int)itemStatsArmorSetter.Value, stats, (int)itemMinimumDamageSetter.Value, (int)itemMaximumDamageSetter.Value, (float)itemAttackSpeedSetter.Value, itemQuality, Enum.Parse<WeaponData.Weapon>(ReplaceWhitespace(itemWeaponTypeSetter.Text, "")), cost);
                    }
                    else
                    {
                        item = new WeaponData(id, gfxName, name, description, Enum.Parse<EquipmentData.EQType>(ReplaceWhitespace(itemWeaponEQTypeSetter.Text, "")), (int)itemStatsArmorSetter.Value, stats, 0, 0, 0, itemQuality, Enum.Parse<WeaponData.Weapon>(ReplaceWhitespace(itemWeaponTypeSetter.Text, "")), cost);
                    }

                    break;

                default:
                    throw new NotImplementedException();
            }

            return item;
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


        private void itemCheckGeneratedTooltip_Click(object sender, EventArgs e)
        {
            ItemData? item = FoldDataIntoItem();
            if (item is null)
            {
                return;
            }

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
                    tooltip += "Not finished yet xdd";
                    break;
                case "Equipment":
                    if (itemEquipmentMaterialSetter.Text != EquipmentData.MaterialType.None.ToString())
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
            ItemData? item = FoldDataIntoItem();

            if (item != null)
            {
                ItemManager.CreateItem(item);
            }
        }


        private void OverrideItemButton_Click(object sender, EventArgs e)
        {
            ItemData? item = FoldDataIntoItem();
            if (item == null)
            {
                return;
            }

            item.ID = items.SelectedIndex;

            ItemManager.OverrideItem(items.SelectedIndex, item);
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
            int previousEditingItem = editingItem;

            if (!CanDiscardChanges(aIndex))
            {
                if (previousEditingItem >= 0 && previousEditingItem < items.Items.Count)
                {
                    items.SelectedIndex = previousEditingItem;
                }
                return;
            }

            if (aIndex != ListBox.NoMatches)
            {
                editingItem = aIndex;
                ItemData item = ItemManager.GetItemById(aIndex);
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

        private bool CanDiscardChanges(int newIndex)
        {
            if (editingItem == -1 || editingItem == newIndex)
            {
                return true;
            }

            ItemData originalItem;
            try
            {
                originalItem = ItemManager.GetItemById(editingItem);
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }
            catch (NullReferenceException)
            {
                return true;
            }

            if (!HasUnsavedChanges(originalItem))
            {
                return true;
            }

            var result = MessageBox.Show(
                "You have unsaved changes. Do you want to discard them?",
                "Discard changes",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        private bool HasUnsavedChanges(ItemData original)
        {
            if (original == null)
            {
                return false;
            }

            if (!string.Equals(itemNameInput.Text, original.Name, StringComparison.Ordinal))
            {
                return true;
            }

            if (!string.Equals(itemDescriptionInput.Text, original.Description, StringComparison.Ordinal))
            {
                return true;
            }

            if ((int)itemMaxCountSetter.Value != original.MaxStack)
            {
                return true;
            }

            if ((int)goldCostCounter.Value != original.Cost)
            {
                return true;
            }

            string currentQuality = GetCheckedItemText(itemQualitySelector);
            if (!string.Equals(currentQuality, original.Quality.ToString(), StringComparison.Ordinal))
            {
                return true;
            }

            string expectedTypeName = itemTypeSelector.FindString(original.TypeName) == -1
                ? nameof(ItemData.ItemType.None)
                : original.TypeName;

            string currentType = GetCheckedItemText(itemTypeSelector);
            if (!string.Equals(currentType, expectedTypeName, StringComparison.Ordinal))
            {
                return true;
            }

            if (original is BagData bag)
            {
                if ((int)itemBagSizeSetter.Value != bag.SlotCount)
                {
                    return true;
                }
            }

            if (original is EquipmentData equipment)
            {
                if ((int)itemStatsAgilitySetter.Value != equipment.Agility
                    || (int)itemStatsStrengthSetter.Value != equipment.Strength
                    || (int)itemStatsStaminaSetter.Value != equipment.Stamina
                    || (int)itemStatsIntelligenceSetter.Value != equipment.Intelligence
                    || (int)itemStatsSpiritSetter.Value != equipment.Spirit
                    || (int)itemStatsArmorSetter.Value != equipment.Armor)
                {
                    return true;
                }

                if (original is WeaponData weapon)
                {
                    if (!string.Equals(ReplaceWhitespace(itemWeaponEQTypeSetter.Text, string.Empty), weapon.Hand.ToString(), StringComparison.Ordinal))
                    {
                        return true;
                    }

                    if (!string.Equals(ReplaceWhitespace(itemWeaponTypeSetter.Text, string.Empty), weapon.WeaponType.ToString(), StringComparison.Ordinal))
                    {
                        return true;
                    }

                    if (weapon.WeaponType < WeaponData.Weapon.Shield)
                    {
                        WeaponData.Attack attack = weapon.GetAttack;

                        if ((int)itemMinimumDamageSetter.Value != attack.MinAttackDamage
                            || (int)itemMaximumDamageSetter.Value != attack.MaxAttackDamage
                            || itemAttackSpeedSetter.Value != (decimal)attack.AttackSpeed)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (!string.Equals(itemTypeEquipmentTypeSetter.Text, equipment.Slot.ToString(), StringComparison.Ordinal))
                    {
                        return true;
                    }

                    if (!string.Equals(itemEquipmentMaterialSetter.Text, equipment.Material.ToString(), StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }

            if (original is PotionData potion)
            {
                int[] currentTypes = itemPotionTypeSetter.CheckedIndices.Cast<int>().OrderBy(x => x).ToArray();
                int[] originalTypes = potion.Type.Select(x => (int)x).OrderBy(x => x).ToArray();

                if (!currentTypes.SequenceEqual(originalTypes))
                {
                    return true;
                }

                for (int i = 0; i < potion.Type.Length; i++)
                {
                    int potionTypeIndex = (int)potion.Type[i];
                    decimal minValue = GetPotionControlValue(potionTypeIndex, "Minimum");
                    decimal maxValue = GetPotionControlValue(potionTypeIndex, "Maximum");

                    if (minValue != (decimal)potion.Value[i] || maxValue != (decimal)potion.MaxValue[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static string GetCheckedItemText(ExtendedForm.ExtendedCheckedListBox list)
        {
            int? index = list.GetSingleCheckedIndex;
            if (!index.HasValue)
            {
                return string.Empty;
            }

            return list.Items[index.Value]?.ToString() ?? string.Empty;
        }

        private decimal GetPotionControlValue(int typeIndex, string namePart)
        {
            if (typeIndex < 0 || typeIndex >= itemPotionValueFlowLayout.Controls.Count)
            {
                return 0;
            }

            Control.ControlCollection controls = itemPotionValueFlowLayout.Controls[typeIndex].Controls;
            foreach (Control control in controls)
            {
                if (control.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                {
                    return ((NumericUpDown)control).Value;
                }
            }

            return 0;
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
