using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPHMMaker;
using SPHMMaker.Items.Effects;

namespace SPHMMaker.Items
{
    internal class ItemData : IComparable<ItemData>
    {
        public enum ItemQuality
        {
            Poor,
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }
        public enum ItemType
        {
            None,
            Bag,
            Consumable,
            Equipment,
            Weapon
        }


        [JsonIgnore]
        public virtual string TypeName => GetType().Name.Substring(0, GetType().Name.Length - "Data".Length);

        public int ID
        {
            get => id;
            set => id = value;
        }

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

        public IReadOnlyList<EffectData> Effects => effects;
        readonly List<EffectData> effects;

        public int Cost => cost;
        int cost;

        [JsonConstructor]
        public ItemData(int id, string gfxName, string name, string description, int maxStack, ItemQuality quality, int cost, IEnumerable<EffectData>? effects = null)
        {
            this.id = id;
            gfx = new GfxPath(GfxType.Item, gfxName);
            this.name = name;
            this.description = description;
            this.maxStack = maxStack;
            this.quality = quality;
            this.cost = cost;
            this.effects = SanitizeEffects(effects);
        }

        static List<EffectData> SanitizeEffects(IEnumerable<EffectData>? source)
        {
            if (source == null)
            {
                return new List<EffectData>();
            }

            return source
                .Where(effect => effect != null)
                .Select(effect => effect!.Clone())
                .ToList();
        }

        public void SetEffects(IEnumerable<EffectData> updatedEffects)
        {
            effects.Clear();
            effects.AddRange(SanitizeEffects(updatedEffects));
        }

        public int CompareTo(ItemData? other)
        {
            if (other == null) return -1;
            if (other.id > id) return -1;
            if (other.id < id) return 1;
            return 0;
        }

        public override string ToString() => name;
    }
}
