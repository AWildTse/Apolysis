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
            
        }
        public override void RemoveFromInventory()
        {
            //if exists, remove from stack
            //if last, remove from inventory
        }
    }
}
