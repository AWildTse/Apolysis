using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apolysis.ItemSystem.Ammunitions;

namespace Apolysis.ItemSystem.Weapons
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Inventory/Weapon/Ranged Weapon")]
    public class RangedWeapon : Weapon
    {
        public float Damage;
        public int AmmoCapacity;
        public Ammo AmmoType;
        public float FireRate;
        public float Velocity;
        public float BulletDrop;
        public float Recoil;
        public float NoiseLevel;
        public bool HoldToFire;

        public override void PickUp()
        {

        }
        public override void Use()
        {

        }
        public override void Equip()
        {

        }
        public override void Unequip()
        {

        }
    }
}