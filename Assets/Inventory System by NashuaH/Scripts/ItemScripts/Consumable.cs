using UnityEngine;
using otherItem;

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Weapon")]
public class Consumable : Item
{
    public consumableType typeOfConsumable;
 
    public int HPRecover;

    public override void Use()
    {
        base.Use();

        //Consumble Action
      

    
         Inventory.instance.RemoveItem(this, 1);
    }

    public enum consumableType { Potion, Food }
   
}
