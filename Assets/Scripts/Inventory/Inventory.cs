using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.Interfaces;
using Apolysis.ItemSystem;

namespace Apolysis.InventorySystem
{
    public abstract class Inventory : MonoBehaviour, IInventory
    {
        //Actual Inventory
        [SerializeReference] public List<Item> InventoryList;
        [SerializeField] public List<int> QuantityList;
        [SerializeField] public int MaxInventory = 5;

        //Placeholder to get Count and location of multiple stacks
        [SerializeField] public List<int> Positions;

        public abstract void AddToInventory(Item item);
        public abstract void RemoveFromInventory();
    }
}
