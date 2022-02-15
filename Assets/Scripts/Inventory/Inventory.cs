using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : IInventory
{
    public abstract void AddToInventory();
    public abstract void RemoveFromInventory();
}
