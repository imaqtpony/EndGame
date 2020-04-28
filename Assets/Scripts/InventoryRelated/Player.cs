using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Inventory inventory;

    [SerializeField] private LifePlayer m_lifePlayer;

    public SerInt m_inventorySpace;

    [SerializeField] private UI_Inventory uiInventory;
    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        Debug.Log(m_inventorySpace.m_value);

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        if(Inventory.itemList.Count <= m_inventorySpace.m_value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            Debug.Log(Inventory.itemList.Count);

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
            case Item.ItemType.Item1:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item1, amount = 1 });
                m_lifePlayer.HealingFunc();

                break;
            case Item.ItemType.Item2:
                Debug.Log("Carré utilisé");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item2, amount = 1 });
                break;
            case Item.ItemType.Item3:
                Debug.Log("Triangle utilisé");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item3, amount = 1 });
                break;

        }
    }
}
