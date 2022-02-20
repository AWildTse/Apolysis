using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.Interfaces;
using Apolysis.ItemSystem;

namespace Apolysis.InventorySystem
{
    public abstract class Inventory : MonoBehaviour, IInventory
    {
        public abstract void AddToInventory(Item item);
        public abstract void RemoveFromInventory();
    }
}
