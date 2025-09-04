using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class ItemStats
    {
        [JsonProperty]
        int[] PrimaryStats { get; }

        public ItemStats(int[] aStats) 
        {
            PrimaryStats = aStats;
        }
    }

    public enum PrimaryStats
    {
        Agility, 
        Strength,
        Stamina,
        Intelligence,
        Spirit
    }
}
