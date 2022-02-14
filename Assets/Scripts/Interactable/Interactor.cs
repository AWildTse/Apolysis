using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    #region Interactable Items and Collider Variables
    [SerializeField] private List<InteractableItem> _interactableItems;
    [Tooltip("The radius around the player that detects nearby _interactableItems")]
    [SerializeField] private float _radius = 3f;
    private InteractableItem _interactableItem;
    private Collider[] _pickUpObjectColliders;
    #endregion

    #region Raycasting And LayerMask Variables
    private float _rayDistance;
    private Camera _camera;
    private int _pickUpObjectLayerMask;
    #endregion

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _rayDistance = 3f;
        _pickUpObjectLayerMask = 1 << 6;
    }

    private void Update()
    {
        //Continuously update collision colliders within a radius around the user for pickupable objects
        _pickUpObjectColliders = Physics.OverlapSphere(transform.position, _radius, _pickUpObjectLayerMask);
        RayCast();
        CheckRadiusForPickUps();
        RemoveFromListOutOfRange();
    }

    public void CheckRadiusForPickUps()
    {
        foreach (var pickup in _pickUpObjectColliders)
        {
            GameObject go = pickup.gameObject;
            _interactableItem = go.GetComponent<InteractableItem>();

            //As long as our _interactableItems list doesn't already contain the item, we add it
            if (!_interactableItems.Contains(_interactableItem))
            {
                _interactableItems.Add(_interactableItem);
            }
        }
    }

    public void RemoveFromListOutOfRange()
    {
        //this checks whether or not the list is completely empty. It'll just clear out everything
        //should only really be called when there is one item left, but this will clean up any
        //missed _interactableItems
        if (PickUpCollidersListEmpty())
        {
            for (int i = 0; i < _interactableItems.Count; i++)
            {
                _interactableItems.RemoveAt(i);
            }
        }
        //As long as there is at least one _interactableItem nearby, we cycle through list
        //_interactableItems. 
        else if(_interactableItems.Count > 0)
        {
            for (int i = 0; i < _interactableItems.Count; i++)
            {
                int count = 0;
                _interactableItem = _interactableItems[i];
                //We cycle through the colliders list
                foreach (var pickup in _pickUpObjectColliders)
                {
                    //check if the collider list's first item's name is equal to the
                    // _interactableItem name
                    if (pickup.name != _interactableItem.name)
                    {
                        //If they're not, we add to the count                        
                        count++;
                    }
                    //If the count equals _pickUpObjectColliders list length, we know
                    //the _interactableItem is no longer within the radius
                    //so we remove it from the list of _interactableItems
                    if(count == _pickUpObjectColliders.Length)
                    {
                        _interactableItems.Remove(_interactableItem);
                    }
                }
            }
        }
    }
    public bool PickUpCollidersListEmpty()
    {
        if(_pickUpObjectColliders.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RayCast()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider.tag == "PickUpObject")
            {
                if (Input.GetButtonDown("PickUp"))
                {
                    _interactableItem.Interact();
                }
            }
        }
    }    
}
