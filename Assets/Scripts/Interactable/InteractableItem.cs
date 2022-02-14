using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    //field for inventory

    private Item _item;

    private void Start()
    {
        _item = gameObject.GetComponent<Item>();
    }

    public override void Interact()
    {
        Debug.Log("Inside Interact for InteractableItem");
        //Destroy(gameObject);
    }
}
