using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    [SerializeReference] private List<Item> _inventory;
    public override void AddToInventory()
    {
        //if exists, add to stack
        //else, add to inventory
    }
    public override void RemoveFromInventory()
    {
        //if exists, remove from stack
        //if last, remove from inventory
    }
}
