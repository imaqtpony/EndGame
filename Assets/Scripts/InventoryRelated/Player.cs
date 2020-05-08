using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;
using TMPro;

public class Player : MonoBehaviour
{

    private Inventory inventory;

    private LifePlayer m_lifePlayer;

    public IntVar m_inventorySpace;

    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] DropItemZone m_dropItemZone;

    [SerializeField] TextMeshProUGUI m_amounItemsInventory;

    // Start is called before the first frame update
    private void Awake()
    {
        m_lifePlayer = GetComponent<LifePlayer>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    
    private void OnTriggerEnter(Collider collider)
    {


        if (Inventory.itemList.Count < m_inventorySpace.Value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

            if (itemWorld != null)
            {
                if(collider.gameObject.tag != "Tools")
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                    Debug.Log("RAMASSE RESSOURCES");
                }
                else if (collider.gameObject.tag == "Tools")
                {
                    inventory.AddTools(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                    Debug.Log("RAMASSE OUTILS");
                }

            }
            m_amounItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count }/{m_inventorySpace.Value}";

        }

    }

    private void UseItem(Item.ItemType p_itemType)
    {
        switch (p_itemType)
        {
            case Item.ItemType.circle:
                m_lifePlayer.HealingFunc();

                break;
            case Item.ItemType.square:
                Debug.Log("Carré utilisé");

                break;
            case Item.ItemType.triangle:
                Debug.Log("Triangle utilisé");

                break;
            case Item.ItemType.losange:
                Debug.Log("Losange utilisé");

                break;

        }
        inventory.RemoveItem(new Item { itemType = p_itemType, amount = 1 });

    }
}
