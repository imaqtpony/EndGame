using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventoryNS.Utils;
using System;

[Serializable]
public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.Euler(90f, 0f, 0f));

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
        
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemworld = SpawnItemWorld(dropPosition + randomDir, item);
        itemworld.GetComponent<Rigidbody>().AddForce(randomDir * 3f, ForceMode.Impulse);
        return itemworld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro uiText;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiText = transform.Find("AmountDroppedItems").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        if(item.itemType == Item.ItemType.hache || 
            item.itemType == Item.ItemType.allumette || 
            item.itemType == Item.ItemType.hache_pierre)
        {
            gameObject.tag = "Tools";
        }
        if(item.itemType == Item.ItemType.echelle)
        {
            gameObject.tag = "SecondaryObject";

        }

    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            //uiText.SetText(item.amount.ToString());
            uiText.SetText("");

        }
        else
        {
            uiText.SetText("");

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
