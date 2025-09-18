using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class BagData : ItemData
    {
        public int SlotCount { get => slotCount; }
        int slotCount;
        [JsonConstructor]
        public BagData(int id, string gfxName, string name, string description, int slotCount, int cost, ItemQuality quality) : base(id, gfxName, name, description, 1, ItemType.Bag, quality, cost)
        {
            this.slotCount = slotCount;
            Assert();
        }

        void Assert()
        {

            Debug.Assert(slotCount > 0);
        }
    }
}
