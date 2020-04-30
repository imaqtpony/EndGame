using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;

public class Player : MonoBehaviour
{

    private Inventory inventory;

    private LifePlayer m_lifePlayer;

    public IntVar m_inventorySpace;

    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] DropItemZone m_dropItemZone;

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
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
        }
        
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.circle:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.circle, amount = 1 });
                m_lifePlayer.HealingFunc();

                break;
            case Item.ItemType.square:
                Debug.Log("Carré utilisé");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.square, amount = 1 });
                break;
            case Item.ItemType.triangle:
                Debug.Log("Triangle utilisé");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.triangle, amount = 1 });
                break;

        }
    }
}
