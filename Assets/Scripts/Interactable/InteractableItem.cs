using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.InventorySystem;
using Apolysis.ItemSystem;

namespace Apolysis.InteractableSystem
{
    public class InteractableItem : Interactable
    {
        //field for inventory
        [SerializeReference] private Item _item;
        [SerializeField] private GameObject _inventoryManager;
        private PlayerInventory _playerInventory;

        private void Start()
        {
            _inventoryManager = GameObject.FindWithTag("InventoryManager");
            _playerInventory = _inventoryManager.GetComponent<PlayerInventory>();
        }
        public override void Interact()
        {
            Debug.Log("Inside Interact for InteractableItem");
            Debug.Log("_item.Name: " + _item.Name + " _item.Description: " + _item.Description);
            _playerInventory.AddToInventory(_item);
            //put this item into inventory here. This is "pick up"
            //Destroy(gameObject);
        }
    }
}
