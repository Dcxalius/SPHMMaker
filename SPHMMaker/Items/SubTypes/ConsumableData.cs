using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class ConsumableData : ItemData
    {
        public enum ConsumableType
        {
            NONE,
            Heal,
            Mana,
            Energy,
            Food,
            Drink
        }

        public float ValueOfType(ConsumableType aType) => value[Array.IndexOf(type, aType)];
        public bool IsOfType(ConsumableType aType) => type.Contains(aType);
        [JsonIgnore]
        public ConsumableType[] Consumable { get => type; }
        ConsumableType[] type;
        [JsonIgnore]
        public float[] Value { get => value; }
        float[] value;


        [JsonConstructor]
        public ConsumableData(int id, string gfxName, string name, string description, int maxStack, ConsumableType[] type, ItemQuality quality, int cost, float[] value) : base(id, gfxName, name, description, maxStack, ItemType.Consumable, quality, cost)
        {
            this.type = type;
            this.value = value;
            Assert();
        }

        void Assert()
        {
            ConsumableType testType = ConsumableType.NONE;
            for (int i = 0; i < type.Length; i++)
            {
                //This assert should be unneccecary with the one bellow //Debug.Assert(type.Where(x => x == type[i]).Count() == 1, "Duplicate ConsumeTypes found");
                Debug.Assert(testType < type[i], "Unsorted or duplicate types");
                testType = type[i];
            }



            Debug.Assert(type.Length == value.Length, "Missing values");
            Debug.Assert(type.Length != 0, "Type not set.");
            Debug.Assert(value.Length != 0, "Value not set.");

        }
    }
}
