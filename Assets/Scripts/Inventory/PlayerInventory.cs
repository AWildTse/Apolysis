using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.ItemSystem;

namespace Apolysis.InventorySystem
{
    public class PlayerInventory : Inventory
    {
        [SerializeReference] public List<Item> Inventory;

        private void Awake()
        {

        }

        public override void AddToInventory(Item item)
        {
            Debug.Log("In AddToInventory");

            //if exists, add to stack
            //else, add to inventory
        }
        public override void RemoveFromInventory()
        {
            //if exists, remove from stack
            //if last, remove from inventory
        }
    }
}
