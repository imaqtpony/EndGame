using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Inventory inventory;

    [SerializeField] private UI_Inventory uiInventory;
    // Start is called before the first frame update
    private void Awake()
    {
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
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Item1:
                Debug.Log("t'as utilisé un cercle pov con");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item1, amount = 1 });
                break;
            case Item.ItemType.Item2:
                Debug.Log("c'est un carré vert ca je crois");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item2, amount = 1 });
                break;
            case Item.ItemType.Item3:
                Debug.Log("et ca le triangle non ?");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item3, amount = 1 });
                break;

        }
    }
}
