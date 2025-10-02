using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SPHMMaker.Items.Effects;

namespace SPHMMaker.Items
{
    internal class WeaponData : EquipmentData
    {
        public enum HandRequirement
        {
            OneHanded,
            MainHanded,
            OffHanded,
            TwoHanded,
            Ranged
        }

        [Flags]
        public enum Weapon
        {
            None,
            Dagger = 1,
            Sword = 2,
            TwoHandedSword = 4,
            Axe = 8,
            TwoHandedAxe = 16,
            Mace = 32,
            TwoHandedMace = 64,
            Fist = 128,
            Staff = 256,
            Bow = 512,
            Gun = 1024,
            Thrown = 2048,
            Wand = 4096,
            Shield = 8192,
            Holdable = 16384 //TODO: I dunnu what to call this xdd
        }

        //public override ItemPairReport StatReport
        //{
        //    get
        //    {
        //        if (statReport != null) return statReport;
        //        ItemPairReport report = base.StatReport;
        //        report.AddLine("Attack", attack.ToString());


        //        return base.StatReport;
        //    }
        //}

        public Attack GetAttack => attack;
        Attack attack;

        public Weapon WeaponType => weaponType;
        Weapon weaponType;

        public HandRequirement Hand => (HandRequirement)(Slot - EQType.OneHanded);


        public WeaponData(int id, string gfxName, string name, string description, EQType slot, int armor, int[] baseStats, int minAttackDamage, int maxAttackDamage, float attackSpeed, ItemQuality quality, Weapon weaponType, int cost, IEnumerable<EffectData>? effects = null) : base(id, gfxName, name, description, slot, armor, baseStats, quality, cost, MaterialType.None, effects)
        {
            if (minAttackDamage != 0 || maxAttackDamage != 0)
            {
                attack = new Attack(minAttackDamage, maxAttackDamage, attackSpeed);
            }
            this.weaponType = weaponType;
            Debug.Assert(weaponType != Weapon.None);
        }
        
        public struct Attack
        {
            public int MinAttackDamage;
            public int MaxAttackDamage;
            public float AttackSpeed;

            public Attack(int aMin, int aMax, float aAttackSpeed)
            {
                MinAttackDamage = aMin;
                MaxAttackDamage = aMax;
                AttackSpeed = aAttackSpeed;
            }

            public override string ToString()
            {
                if (MinAttackDamage == 0 || MaxAttackDamage == 0) return "";

                return $"{MinAttackDamage} to {MaxAttackDamage} damage every {AttackSpeed} seconds \n" +
                    $"for {((float)(MinAttackDamage + MaxAttackDamage) / 2) / AttackSpeed} dps";
            }
        }
    }

}
