using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Apolysis.ItemSystem;

namespace Apolysis.InventorySystem
{
    public class PlayerInventory : Inventory
    {
        //Actual Inventory
        [SerializeReference] public List<Item> InventoryList;
        [SerializeField] public List<int> QuantityList;
        [SerializeField] public int MaxInventory = 5;

        //Placeholder to get Count and location of multiple stacks
        [SerializeField] public List<int> Positions;

        private void Awake()
        {

        }

        public override void AddToInventory(Item item)
        {
            if (InventoryMaxedSlotsFull())
            {
                Debug.Log("All slots and stacks are FULL. Cannot add item");
            }
            if (item.IsStackable)
            {

                for (int i = 0; i < InventoryList.Count; i++)
                {
                    if (InventoryList[i] == item)
                    {
                        Positions.Add(i);
                    }
                }
                if (Positions.Count == 0)
                {
                    AddItem(item);
                }
                else
                {
                    int count = 0;
                    for (int i = 0; i < Positions.Count; i++)
                    {
                        if (count == Positions.Count)
                        {
                            AddItem(item);
                        }
                        if (QuantityList[Positions[i]] == item.MaxStackAmount)
                        {
                            count++;
                            if (count == Positions.Count)
                            {
                                if (!InventoryMaxedSlotsFull())
                                {
                                    AddItem(item);
                                }
                                else
                                {
                                    Debug.Log("Can't add item to inventory. MAXED SLOTS AND STACKS");
                                    break;
                                }
                            }
                            continue;
                        }
                        else
                        {
                            QuantityList[Positions[i]] = QuantityList[Positions[i]] + 1;
                            break;
                        }
                    }
                    Positions.Clear();
                }
            }
            else
            {
                if (!InventorySlotsFull())
                {
                    AddItem(item);
                }
                else
                {
                    Debug.Log("Can't add item to inventory. MAXED SLOTS");
                }
            }
        }

        public override void RemoveFromInventory()
        {
            //if exists, remove from stack
            //if last, remove from inventory
        }

        public void AddItem(Item item)
        {
            InventoryList.Add(item);
            QuantityList.Add(1);
        }

        public bool InventorySlotsFull()
        {
            if (InventoryList.Count == MaxInventory)
                return true;
            else
                return false;
        }

        public bool InventoryMaxedSlotsFull()
        {
            int fullSlots = 0;
            for (int i = 0; i < InventoryList.Count; i++)
            {
                if (InventoryList[i].IsStackable)
                {
                    if (QuantityList[i] == InventoryList[i].MaxStackAmount)
                    {
                        fullSlots++;
                    }
                }
                else
                {
                    fullSlots++;
                }
            }
            if (fullSlots == MaxInventory)
            {
                Debug.Log("fullSlots: " + fullSlots);
                Debug.Log("MaxInventory: " + MaxInventory);
                return true;
            }
            return false;
        }
    }
}
