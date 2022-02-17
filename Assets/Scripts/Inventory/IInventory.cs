using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public void AddToInventory(Item item);
    public void RemoveFromInventory();
}
