using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SPHMMaker.Items.Effects
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class EffectData : IEquatable<EffectData>
    {
        public enum EffectType
        {
            Buff,
            Debuff,
            Heal,
            Damage,
            Utility
        }

        public enum EffectTarget
        {
            Self,
            Ally,
            Enemy,
            Area
        }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public EffectType Type { get; private set; }

        [JsonProperty]
        public EffectTarget Target { get; private set; }

        [JsonProperty]
        public float Magnitude { get; private set; }

        [JsonProperty]
        public float Duration { get; private set; }

        [JsonConstructor]
        public EffectData(string name, string description, EffectType type, EffectTarget target, float magnitude, float duration)
        {
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            Type = type;
            Target = target;
            Magnitude = magnitude;
            Duration = duration;
        }

        public EffectData Clone()
        {
            return new EffectData(Name, Description, Type, Target, Magnitude, Duration);
        }

        public bool Equals(EffectData? other)
        {
            if (other is null)
            {
                return false;
            }

            return string.Equals(Name, other.Name, StringComparison.Ordinal)
                && string.Equals(Description, other.Description, StringComparison.Ordinal)
                && Type == other.Type
                && Target == other.Target
                && Magnitude.Equals(other.Magnitude)
                && Duration.Equals(other.Duration);
        }

        public override bool Equals(object? obj) => Equals(obj as EffectData);

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Type, Target, Magnitude, Duration);
        }

        public override string ToString()
        {
            return $"{Name} ({Type} → {Target})";
        }
    }
}
