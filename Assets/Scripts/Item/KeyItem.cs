using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.Interfaces;

namespace Apolysis.ItemSystem
{
    public abstract class KeyItem : IItem
    {
        public string KeyItemName { get; protected set; }
        public string KeyItemDescription { get; protected set; }

        public Sprite KeyItemIcon;

        public abstract void PickUp();
        public abstract void Use();
    }
}