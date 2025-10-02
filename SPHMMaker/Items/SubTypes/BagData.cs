using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPHMMaker.Items.Effects;

namespace SPHMMaker.Items
{
    internal class BagData : ItemData
    {
        public int SlotCount { get => slotCount; }
        int slotCount;
        [JsonConstructor]
        public BagData(int id, string gfxName, string name, string description, int slotCount, int cost, ItemQuality quality, IEnumerable<EffectData>? effects = null) : base(id, gfxName, name, description, 1, quality, cost, effects)
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
