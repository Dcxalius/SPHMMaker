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
        [JsonIgnore]
        public float[] Value { get => value; }
        protected float[] value;


        [JsonConstructor]
        public ConsumableData(int id, string gfxName, string name, string description, int maxStack, ItemQuality quality, int cost, float[] value) : base(id, gfxName, name, description, maxStack, quality, cost)
        {
            this.value = value;
            Assert();
        }

        void Assert()
        {
            
            Debug.Assert(value.Length != 0, "Value not set.");

        }
    }
}
