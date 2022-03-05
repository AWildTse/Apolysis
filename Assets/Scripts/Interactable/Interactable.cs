using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apolysis.Interfaces;

namespace Apolysis.InteractableSystem
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public abstract void Interact();
    }
}