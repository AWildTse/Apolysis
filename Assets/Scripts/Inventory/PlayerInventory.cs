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
        
        //Placeholder to get Count and location of multiple stacks
        [SerializeField] public List<int> Positions;

        private void Awake()
        {

        }
        public void test(Item item)
        {
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
                    InventoryList.Add(item);
                    QuantityList.Add(1);
                }
                else
                {
                    int count = 0;
                    for (int i = 0; i < Positions.Count; i++)
                    {
                        if (count == Positions.Count)
                        {
                            InventoryList.Add(item);
                            QuantityList.Add(1);
                        }
                        if (QuantityList[Positions[i]] == item.MaxStackAmount)
                        {
                            count++;
                            if (count == Positions.Count)
                            {
                                InventoryList.Add(item);
                                QuantityList.Add(1);
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
                InventoryList.Add(item);
                QuantityList.Add(1);
            }
        }
        public override void AddToInventory(Item item)
        {
            Debug.Log("In AddToInventory");
            if (item.IsStackable)
            {
                Debug.Log("Item is stackable!");
                if (InventoryList.Contains(item)) //for(int i = 0; i < InventoryList.Count; i++
                {
                    Debug.Log("Item is already in inventory. Adding to existing item.");
                    if (QuantityList[InventoryList.IndexOf(item)] != item.MaxStackAmount)
                    {
                        QuantityList[InventoryList.IndexOf(item)] = QuantityList[InventoryList.IndexOf(item)] + 1;
                    }
                    else
                    {
                        if (InventoryList.Count < 9) //temp max storage
                        {
                            Debug.Log("Making a new item as long as there is still room in the inventory, current count is: " + InventoryList.Count);
                            InventoryList.Add(item);
                            QuantityList.Add(1); //temp how many to add
                        }
                        else
                        {
                            Debug.Log("You're out of inventory space! Current count is: " + InventoryList.Count);
                        }
                    }
                }
                else
                {

                    if (InventoryList.Count < 9) //temp max storage
                    {
                        Debug.Log("Making a new item as long as there is still room in the inventory, current count is: " + InventoryList.Count);
                        InventoryList.Add(item);
                        QuantityList.Add(1); //temp how many to add
                    }
                    else
                    {
                        Debug.Log("You're out of inventory space! Current count is: " + InventoryList.Count);
                    }

                }

            }
            else
            {
                for (int i = 0; i <= 1; i++) //temp how many to add
                {
                    if (InventoryList.Count < 9) //temp max storage
                    {
                        Debug.Log("Item is not stackable. Adding in as long as there is room in inventory!");
                        InventoryList.Add(item);
                        QuantityList.Add(1);
                    }
                    else
                    {
                        Debug.Log("You're out of inventory space (in not stackable)! Current count is: " + InventoryList.Count);
                    }

                }

            }
            //if exists, add to stack
            //else, add to inventory
        }
        public override void RemoveFromInventory()
        {
            //if exists, remove from stack
            //if last, remove from inventory
        }
    }
}
