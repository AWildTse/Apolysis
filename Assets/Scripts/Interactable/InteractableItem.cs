using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    //field for inventory

    public Item item;

    private void Start()
    {
        //initialize getComponent to item
    }

    public override void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up" + item.Name);
        Destroy(gameObject);
    }
}
