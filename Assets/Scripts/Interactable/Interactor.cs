using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.InteractableSystem;

namespace Apolysis.InteractorSystem
{
    public class Interactor : MonoBehaviour
    {
        #region Interactable Items and Collider Variables
        [SerializeField] private List<Interactable> _interactables;
        [Tooltip("The radius around the player that detects nearby _interactableItems")]
        [SerializeField] private float _radius = 3f;
        [SerializeField] Interactable _interactable;
        private Collider[] _interactableCollider;
        #endregion

        #region Raycasting And LayerMask Variables
        private float _rayDistance;
        private Camera _camera;
        private int _interactableLayerMask;
        #endregion

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
            _rayDistance = 3f;
            _interactableLayerMask = 1 << 6;
        }

        private void Update()
        {
            FindAllNearbyInteractables();
            RayCast();
        }

        public void FindAllNearbyInteractables()
        {
            _interactableCollider = Physics.OverlapSphere(transform.position, _radius, _interactableLayerMask);
            AddInteractables();
            RemoveOutOfRangeInteractables();
        }

        public void AddInteractables()
        {
            foreach (var interactable in _interactableCollider)
            {
                GameObject go = interactable.gameObject;
                _interactable = go.GetComponent<InteractableItem>();

                //As long as our _interactableItems list doesn't already contain the item, we add it
                if (!_interactables.Contains(_interactable))
                {
                    _interactables.Add(_interactable);
                }
            }
        }

        public void RemoveOutOfRangeInteractables()
        {
            //this checks whether or not the list is completely empty. It'll just clear out everything
            //should only really be called when there is one item left, but this will clean up any
            //missed _interactableItems
            if (InteractableColliderIsEmpty())
            {
                for (int i = 0; i < _interactables.Count; i++)
                {
                    _interactables.RemoveAt(i);
                }
            }
            //As long as there is at least one _interactableItem nearby, we cycle through list
            //_interactableItems. 
            else if (_interactables.Count > 0)
            {
                for (int i = 0; i < _interactables.Count; i++)
                {
                    int count = 0;
                    _interactable = _interactables[i];
                    //We cycle through the colliders list
                    foreach (var interactable in _interactableCollider)
                    {
                        //check if the collider list's first item's name is equal to the
                        //_interactableItem name
                        if (interactable.name != _interactable.name)
                        {
                            //If they're not, we add to the count                        
                            count++;
                        }
                        //If the count equals _pickUpObjectColliders list length, we know
                        //the _interactableItem is no longer within the radius
                        //so we remove it from the list of _interactableItems
                        if (count == _interactableCollider.Length)
                        {
                            _interactables.Remove(_interactable);
                        }
                    }
                }
            }
        }
        public bool InteractableColliderIsEmpty()
        {
            if (_interactableCollider.Length > 0)
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
            GameObject interactable;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //If we hit something raycasting
            if (Physics.Raycast(ray, out hit, _rayDistance))
            {
                //We check if it's an InteractableObject (If we add LayerMask to Physics.raycast
                //we might be able to skip this check)
                if (hit.collider.tag == "InteractableObject")
                {
                    interactable = hit.collider.gameObject;
                    for (int i = 0; i < _interactables.Count; i++)
                    {
                        //check nearby interactables to see if any of them match
                        if (interactable.name == _interactables[i].name)
                        {
                            //We use that object in the list to Interact() if they do match
                            if (Input.GetButtonDown("Interact"))
                            {
                                _interactables[i].Interact();
                            }
                        }
                    }
                }
            }
        }
    }
}