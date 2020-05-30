//Last Edited : 30/05 

using UnityEngine;
using InventoryNS.Utils;

public class EnvironementObject : MonoBehaviour
{

    public static bool m_hasDropObject;

    /// <summary>
    /// Handle the death of an environment object
    /// </summary>
    /// <param name="p_isBurned"> is it on fire </param>
    /// <param name="p_destroyTime"> how much time before destroying the object </param>
    /// <param name="p_amountItem"> amount to drop </param>
    /// <param name="p_itemType"> what is the loot </param>
    protected void DropMaterialOnDeath(bool p_isBurned, float p_destroyTime, int p_amountItem, Item.ItemType p_itemType)
    {
        if (!MovePlayer.m_stopSwipe)
        {
            if (!p_isBurned)
            {
                Destroy(gameObject, p_destroyTime);

                int randNumb = Random.Range(0, 2);

                //50% chance to drop something
                if(randNumb == 1)
                {
                    Item duplicateItem = new Item { itemType = p_itemType, amount = p_amountItem };
                    ItemWorld.DropItem(gameObject.transform.position, duplicateItem);
                }
                
            }
            else
            {
                Destroy(gameObject, p_destroyTime);
                
            }
        }
        
    }

    /// <summary>
    /// When a crate is opened
    /// </summary>
    /// <param name="p_attachedObject"> the associated object </param>
    /// <param name="p_itemType"> and item </param>
    protected void DropMaterialOnDeathCrate(GameObject p_attachedObject, Item.ItemType p_itemType)
    {
        if (!m_hasDropObject)
        {
            int randNumb = Random.Range(0, 3);
            if (randNumb == 0)
            {
                Item duplicateItem = new Item { itemType = p_itemType, amount = 1 };
                ItemWorld.DropItem(gameObject.transform.position, duplicateItem);
                Debug.Log("DROP PAS LEVIER");
            }
            else
            {
                Instantiate(p_attachedObject, new Vector3(transform.position.x, 0.01f, transform.position.z), Quaternion.Euler(-90, 0f, 0f));
                m_hasDropObject = true;
                Debug.Log("DROP LEVIER");

            }

        }
        Destroy(gameObject, .5f);

    }



}
