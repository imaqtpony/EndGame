﻿using System.Collections;
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
    [SerializeField] GameObject Squarangle;

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
            case Item.ItemType.baton:
                var mySphere = Instantiate(Sphere, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                mySphere.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.tissu:
                var mySquare = Instantiate(Cube, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                mySquare.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.mrcFer:
                var myTriangle = Instantiate(Triangle, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                myTriangle.transform.parent = gameObject.transform;
                break;

            case Item.ItemType.hache:
                var myLosange = Instantiate(Losange, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                myLosange.transform.parent = gameObject.transform;
                gameObject.tag = "Tools";
                break;

            case Item.ItemType.torche:
                var mySquarangle = Instantiate(Squarangle, new Vector3(gameObject.transform.position.x,
                                            gameObject.transform.position.y,
                                            gameObject.transform.position.z), Quaternion.identity);

                mySquarangle.transform.parent = gameObject.transform;
                gameObject.tag = "Tools";
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
