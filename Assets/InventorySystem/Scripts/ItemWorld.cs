using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        Debug.Log("SPAWWWWWWN");

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
        
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemworld = SpawnItemWorld(dropPosition + randomDir * 3f, item);
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


    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            uiText.SetText(item.amount.ToString());
        }
        else
        {
            uiText.SetText("");

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
