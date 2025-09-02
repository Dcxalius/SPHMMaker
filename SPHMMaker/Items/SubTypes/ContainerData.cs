﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class ContainerData : ItemData
    {
        public int SlotCount { get => slotCount; }
        int slotCount;
        [JsonConstructor]
        public ContainerData(int id, string gfxName, string name, string description, int slotCount, int cost, Item.Quality quality) : base(id, gfxName, name, description, 1, ItemType.Container, quality, cost)
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
