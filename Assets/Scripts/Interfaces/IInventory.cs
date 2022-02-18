using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.ItemSystem;

namespace Apolysis.Interfaces
{
    public interface IInventory
    {
        public void AddToInventory(Item item);
        public void RemoveFromInventory();
    }
}