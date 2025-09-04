using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class EquipmentData : ItemData
    {
        public enum EQType //TODO: Find better name
        {
            Head,
            Neck,
            Shoulders,
            Back,
            Chest,
            Wrist,
            Hands,
            Belt,
            Legs,
            Feet,
            Finger,
            Trinket,
            OneHander,
            MainHander,
            OffHander,
            TwoHander,
            Ranged,
            Count
        }

        public enum GearType
        {
            Cloth,
            Leather,
            Mail,
            Plate,
            Count,
            None
        }

        //[JsonIgnore]
        //public PairReport StatReport
        //{
        //    get
        //    {
        //        PairReport report = new PairReport();

        //        if (baseStats.TotalArmor != 0) report.AddLine("Armor", baseStats.TotalArmor);
        //        baseStats.AppendToExistingReport(ref report);

        //        return report;
        //    }
        //}

        //public EquipmentStats BaseStats => baseStats;
        //EquipmentStats baseStats;

        public GearType Material => material;
        GearType material;

        public EQType Slot { get => slot; } //TODO: Find better name
        EQType slot;

        public EquipmentData(int id, string gfxName, string name, string description, EQType slot, ItemType itemType, int armor, int[] baseStats, ItemQuality quality, int cost, GearType material) : base(id, gfxName, name, description, 1, itemType, quality, cost)
        {
            this.slot = slot;
            //this.baseStats = new EquipmentStats(baseStats);
            //DEBUG
            //this.baseStats = new EquipmentStats(baseStats, armor);
            this.material = material;

        }

        [JsonConstructor]
        public EquipmentData(int id, string gfxName, string name, string description, EQType slot, int armor, int[] baseStats, ItemQuality quality, int cost, GearType material) : this(id, gfxName, name, description, slot, ItemType.Equipment, armor, baseStats, quality, cost, material) { }
    }
}
