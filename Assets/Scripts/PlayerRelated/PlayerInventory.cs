using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;
using TMPro;

/// <summary>
/// manages the invotory when the player harvests an item in the world
/// </summary>
public class PlayerInventory : MonoBehaviour
{

    private Inventory inventory;

    public IntVar m_inventorySpace;

    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] TextMeshProUGUI m_amounItemsInventory;

    private void Awake()
    {
        uiInventory.SetInventory(inventory);

    }

    /// <summary>
    /// position of the player
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (Inventory.itemList.Count < m_inventorySpace.Value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            m_amounItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count }/{m_inventorySpace.Value}";

            if (itemWorld != null)
            {
                //add the item at the ressources list 
                if (collider.gameObject.tag != "Tools")
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
                //or the tool list
                else if (collider.gameObject.tag == "Tools")
                {
                    inventory.AddTools(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }

            }

        }

    }

}
