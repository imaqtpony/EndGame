using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventoryNS.Utils;
using System;

[Serializable]
public class ItemWorld : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject Cube;
    [SerializeField] GameObject Triangle;
    [SerializeField] GameObject Losange;


    GameObject myItem;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
        
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemworld = SpawnItemWorld(dropPosition + randomDir * 2f, item);
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

        switch (item.itemType)
        {
            case Item.ItemType.circle:
                var mySphere = Instantiate(Sphere, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                mySphere.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.square:
                var mySquare = Instantiate(Cube, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                mySquare.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.triangle:
                var myTriangle = Instantiate(Triangle, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                myTriangle.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.losange:
                var myLosange = Instantiate(Losange, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                myLosange.transform.parent = gameObject.transform;
                break;


        }
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
