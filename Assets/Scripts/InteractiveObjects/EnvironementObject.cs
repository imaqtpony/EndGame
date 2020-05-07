using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryNS.Utils;

public class EnvironementObject : MonoBehaviour
{

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //ce script pour generer les items quand l'objet du world creve, et s'autodetruire
    protected void DropMaterialOnDeath(bool p_isBurned, float p_destroyTime, int p_amountItem, Item.ItemType p_itemType)
    {
        if (!p_isBurned)
        {
            //Item item = GenerateItem(transform.position, p_item.ItemType);
            Destroy(gameObject, p_destroyTime);
            Debug.Log("DETRUIS TOIIII");

            Item duplicateItem = new Item { itemType = p_itemType, amount = p_amountItem };
            ItemWorld.DropItem(gameObject.transform.position, duplicateItem);
        }
        else
        {
            Destroy(gameObject, p_destroyTime);

        }


    }



}
