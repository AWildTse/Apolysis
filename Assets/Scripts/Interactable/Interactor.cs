using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private float _rayDistance;
    [SerializeField] private List<InteractableItem> _interactableItems;
    [SerializeField] private InteractableItem _interactableItem;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private Collider[] _pickUpObjectColliders;
    private Camera _camera;
    private int _pickUpObjectLayerMask;

    private GameObject _target;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _rayDistance = 3f;
        _pickUpObjectLayerMask = 1 << 6;
    }

    private void Update()
    {
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

            if (!_interactableItems.Contains(_interactableItem))
            {
                _interactableItems.Add(_interactableItem);
            }
        }
    }

    public void RemoveFromListOutOfRange()
    {
        //this check whether or not the list is completely empty. It'll just clear out everything
        //should only really be called when there is one item left, but this will clean up any
        //missed _interactableItems
        if (PickUpCollidersListEmpty())
        {
            for (int i = 0; i < _interactableItems.Count; i++)
            {
                _interactableItems.RemoveAt(i);
            }
        }
    }
    public bool PickUpCollidersListEmpty()
    {
        if(_pickUpObjectColliders.Length > 0)
        {
            Debug.Log("We have some pick up colliders");
            return false;
        }
        else
        {
            Debug.Log("We don't have any pick up colliders");
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void RayCast()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider.tag == "PickUpObject")
            {
                Debug.Log("collider tag hit");
                foreach (var item in _interactableItems)
                {
                    Debug.Log("item name in _interactableItems list: " + item.name);
                }

                if (Input.GetButtonDown("PickUp"))
                {
                    //_interactableItem.Interact();
                }
            }
        }
    }    
}
