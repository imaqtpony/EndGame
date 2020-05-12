using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryNS.Utils;

public class EnvironementObject : MonoBehaviour
{

    public static bool m_hasDropObject;

    //ce script pour generer les items quand l'objet du world creve, et s'autodetruire
    protected void DropMaterialOnDeath(bool p_isBurned, float p_destroyTime, int p_amountItem, Item.ItemType p_itemType)
    {
        if (!MovePlayer.m_stopSwipe)
        {
            if (!p_isBurned)
            {
                //Item item = GenerateItem(transform.position, p_item.ItemType);
                Destroy(gameObject, p_destroyTime);

                Item duplicateItem = new Item { itemType = p_itemType, amount = p_amountItem };
                ItemWorld.DropItem(gameObject.transform.position, duplicateItem);
            }
            else
            {
                Destroy(gameObject, p_destroyTime);
                
            }
        }
        
    }

    protected void DropMaterialOnDeathCrate(GameObject p_attachedObject)
    {
        if (!m_hasDropObject)
        {
            Instantiate(p_attachedObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            m_hasDropObject = true;
        }
        Destroy(gameObject);

    }



}
