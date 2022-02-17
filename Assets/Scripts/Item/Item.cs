using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject, IItem
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public bool IsStackable = false;
    public int StackAmount;

    public abstract void PickUp();
    public abstract void Use();
}
