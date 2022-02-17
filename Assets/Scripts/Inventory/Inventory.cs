using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour, IInventory
{
    public abstract void AddToInventory(Item item);
    public abstract void RemoveFromInventory();
}
