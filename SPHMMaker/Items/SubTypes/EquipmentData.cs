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
            OneHanded,
            MainHanded,
            OffHanded,
            TwoHanded,
            Ranged,
            Count
        }
        public enum MaterialType
        {
            Cloth,
            Leather,
            Mail,
            Plate,
            Count,
            None
        }

        [JsonIgnore]
        public virtual ItemPairReport StatReport
        {
            get
            {
                if (statReport != null) return statReport;
                ItemPairReport report = new ItemPairReport();
                for (int i = 0; i < baseStats.Length; i++)
                {
                    if (baseStats[i] != 0)
                    {
                        report.AddLine(((PrimaryStats)i).ToString(), baseStats[i]);
                    }
                }
                if (armor != 0) report.AddLine("Armor", armor);

                statReport = report;
                return report;
            }
        }
        protected ItemPairReport statReport;

        [JsonIgnore]
        public int Agility => baseStats[0];
        [JsonIgnore]
        public int Strength => baseStats[1];
        [JsonIgnore]
        public int Stamina => baseStats[2];
        [JsonIgnore]
        public int Intelligence => baseStats[3];
        [JsonIgnore]
        public int Spirit => baseStats[4];

        public int[] BaseStats => baseStats;
        int[] baseStats;

        public int Armor => armor;
        int armor;

        public MaterialType Material => material;
        MaterialType material;

        public EQType Slot { get => slot; } //TODO: Find better name
        EQType slot;

        [JsonConstructor]
        public EquipmentData(int id, string gfxName, string name, string description, EQType slot, int armor, int[] baseStats, ItemQuality quality, int cost, MaterialType material) : base(id, gfxName, name, description, 1, quality, cost)
        {
            this.slot = slot;
            //this.baseStats = new EquipmentStats(baseStats);
            //DEBUG
            this.baseStats = baseStats;
            this.material = material;
            this.armor = armor;

        }
    }
}
