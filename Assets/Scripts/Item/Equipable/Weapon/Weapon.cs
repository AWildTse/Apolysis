using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.Interfaces;

namespace Apolysis.ItemSystem.Weapons
{
    public abstract class Weapon : Item, IEquipable
    {
        public abstract override void PickUp();
        public abstract override void Use();
        public abstract void Equip();
        public abstract void Unequip();
    }
}