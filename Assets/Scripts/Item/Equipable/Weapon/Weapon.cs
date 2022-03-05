using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.Interfaces;

namespace Apolysis.ItemSystem.Weapons
{
    public abstract class Weapon : Item, IEquipable
    {
        public abstract void Equip();
        public abstract void Unequip();
    }
}