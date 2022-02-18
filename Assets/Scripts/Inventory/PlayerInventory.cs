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
        //What we're using to search add related items
        [SerializeReference] public List<Item> MultipleItemsList;
        [SerializeField] public List<int> MultipleQuantitiesList;
        //position of related items relative to InventoryList
        [SerializeField] public List<int> Positions;

        private void Awake()
        {

        }
        public void test(Item item)
        {
            //item is stackable
            if (item.IsStackable)
            {
                
                //check if item is in inventory list
                for (int i = 0; i < InventoryList.Count; i++)
                {
                    if (InventoryList[i] == item)
                    {
                        MultipleItemsList.Add(item); //store items that you have multiples of
                        MultipleQuantitiesList.Add(QuantityList[i]); //store item positions
                        Positions.Add(i); //store position in inventorylist
                        Debug.Log("We have found matches. Adding " + item + " to MultipleItemsList. Currently Quantity is: " + QuantityList[i] + ". This item is stored in position: " + i);
                    }
                }
                //if item was not in inventory
                if(MultipleItemsList.Count == 0)
                {
                    Debug.Log("No Match found. Adding " + item + " to list");
                    InventoryList.Add(item);
                    QuantityList.Add(1); //temp how many to add
                }
                //item was in inventory. Cycle through and check if any stacks are full
                else
                {
                    //check if single item is maxed
                    if(MultipleItemsList.Count == 1)
                    {
                        Debug.Log("MultipleItemsList is == 1");
                        //stack is FULL.
                        if(MultipleQuantitiesList[0] == item.MaxStackAmount)
                        {
                            //add to the inventory and quantity lists 
                            Debug.Log("Comparing MultipleQuantitiesList to Max Item stack, the stack is full, adding new item");
                            InventoryList.Add(item);
                            QuantityList.Add(1);

                            //clear all the extra lists for next time
                            MultipleItemsList.Clear();
                            MultipleQuantitiesList.Clear();
                            Positions.Clear();
                        }
                        //stack is not full.
                        else
                        {
                            //increment by one
                            Debug.Log("Stack is not full. Adding quantity.");
                            QuantityList[Positions[0]] = QuantityList[Positions[0]] + 1;

                            //clear all the extra lists for next time
                            MultipleItemsList.Clear();
                            MultipleQuantitiesList.Clear();
                            Positions.Clear();
                        }
                    }
                    //There's more than one
                    else
                    {
                        //if the number of counts == MultipleItemsList.Count, then all items are full and we must instead add a new stack.
                        int count = 0;
                        for(int i = 0; i < MultipleItemsList.Count; i++)
                        {
                            if (count == MultipleItemsList.Count)
                            {
                                Debug.Log("All stacks inside inventory are now full.");
                                InventoryList.Add(item);
                                QuantityList.Add(1);
                            }
                            //stack is FULL.
                            if (MultipleQuantitiesList[i] == item.MaxStackAmount)
                            {
                                Debug.Log("There's more than one stack of this item. We are continuing loop until we find the stack that is not full");
                                
                                //we want to continue because current stack is full and we know from
                                //our conditionals that there will be more items to look at.
                                count++;
                                Debug.Log("count is " + count + " after adding");

                                if (count == MultipleItemsList.Count)
                                {
                                    Debug.Log("All stacks inside inventory are now full.");
                                    InventoryList.Add(item);
                                    QuantityList.Add(1);
                                }
                                continue;
                            }
                            else
                            {
                                Debug.Log("We found the stack that is not full. Adding");
                                //we want to add only one item and not any stacks that have less than
                                //stack amount stacked. break out of the for loop.
                                QuantityList[Positions[i]] = QuantityList[Positions[i]] + 1;
                                break;
                            }
                        }
                        //clear all the extra lists for next time
                        MultipleItemsList.Clear();
                        MultipleQuantitiesList.Clear();
                        Positions.Clear();
                    }
                }
            }
            //item is not stackable
            else { }
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
