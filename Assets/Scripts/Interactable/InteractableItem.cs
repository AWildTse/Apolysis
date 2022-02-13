using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    //field for inventory

    private Item _item;

    private void Start()
    {
        _item = GetComponent<Item>();
    }

    public override void Interact()
    {
        Debug.Log("Picking up" + _item.Name);
        Destroy(gameObject);
    }
}