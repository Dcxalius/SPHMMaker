﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPHMMaker;

namespace SPHMMaker.Items
{
    internal class ItemData : IComparable<ItemData>
    {
        public enum ItemQuality
        {
            Trash,
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }
        public enum ItemType
        {
            NotSet,
            Container,
            Trash,
            Consumable,
            Equipment,
            Weapon
        }


        public int ID => id;
        int id;
        public string Name => name;
        string name;
        public string Description => description;
        string description;

        public int MaxStack => maxStack;
        int maxStack;

        public GfxPath GfxPath => gfx;
        GfxPath gfx;

        public ItemQuality Quality => quality;
        ItemQuality quality;

        public ItemType Type => itemType;
        ItemType itemType;

        public int Cost => cost;
        int cost;

        [JsonConstructor]
        public ItemData(int id, string gfxName, string name, string description, int maxStack, ItemType itemType, ItemQuality quality, int cost)
        {
            this.id = id;
            gfx = new GfxPath(GfxType.Item, gfxName);
            this.name = name;
            this.description = description;
            this.maxStack = maxStack;
            this.itemType = itemType;
            this.quality = quality;
            this.cost = cost;
            Assert();
        }

        void Assert()
        {
            Debug.Assert(name != null && description != null && maxStack > 0 && itemType != ItemType.NotSet, "Itemdata not properly set.");
        }

        public int CompareTo(ItemData? other)
        {
            if (other == null) return -1;
            if (other.id > id) return -1;
            if (other.id < id) return 1;
            return 0;
        }
    }
}
