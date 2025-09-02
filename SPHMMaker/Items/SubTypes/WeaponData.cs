using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SPHMMaker.Items
{
    internal class WeaponData : EquipmentData
    {
        public enum HandRequirement
        {
            OneHand,
            TwoHand,
            MainHand,
            OffHand,
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

        public Attack GetAttack => attack;
        Attack attack;

        int minAttackDamage;
        int maxAttackDamage;
        float attackSpeed;

        public Weapon WeaponType => weaponType;
        Weapon weaponType;


        public WeaponData(int id, string gfxName, string name, string description, EQType slot, int armor, int[] baseStats, int minAttackDamage, int maxAttackDamage, float attackSpeed, Item.Quality quality, Weapon weaponType, int cost) : base(id, gfxName, name, description, slot, ItemType.Weapon, armor, baseStats, quality, cost, GearType.None)
        {
            attack = new Attack(minAttackDamage, maxAttackDamage, attackSpeed);
            this.weaponType = weaponType;
            Debug.Assert(weaponType != Weapon.None);
        }
        
        public struct Attack
        {
            int minAttackDamage;
            int maxAttackDamage;
            float attackspeed;

            public Attack(int aMin, int aMax, float aAttackSpeed)
            {
                minAttackDamage = aMin;
                maxAttackDamage = aMax;
                attackspeed = aAttackSpeed;
            }
        }
    }

}
