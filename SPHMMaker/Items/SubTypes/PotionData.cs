using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items.SubTypes
{
    internal class PotionData : ConsumableData
    {

        public enum PotionType
        {
            Heal,
            Mana,
            Energy
        }

        public override string TypeName => "Consumable";

        public new PotionType[] Type { get => type; }
        PotionType[] type;

        public float[] MaxValue => maxValue;
        float[] maxValue;

        public float ValueOfType(PotionType aType) => value[Array.IndexOf(type, aType)];
        public bool IsOfType(PotionType aType) => type.Contains(aType);

        public PotionData(int id, string gfxName, string name, string description, int maxStack, PotionType[] type, ItemQuality quality, int cost, float[] value, float[] maxValue) : base(id, gfxName, name, description, maxStack, quality, cost, value)
        {
            this.type = type ?? Array.Empty<PotionType>();
            this.maxValue = maxValue ?? Array.Empty<float>();

            Assert();
        }

        void Assert()
        {
            Debug.Assert(type.Length != 0, "Type not set.");
            Debug.Assert(type.Length == value.Length && value.Length == maxValue.Length, "Missing values");
            PotionType? testType = null;
            for (int i = 0; i < type.Length; i++)
            {
                //This assert should be unneccecary with the one bellow //Debug.Assert(type.Where(x => x == type[i]).Count() == 1, "Duplicate ConsumeTypes found");
                if (testType.HasValue) Debug.Assert(testType.Value < type[i], "Unsorted or duplicate types");
                testType = type[i];
                Debug.Assert(value[i] <= maxValue[i], "MaxValue was less than value");
            }
        }
    }
}
