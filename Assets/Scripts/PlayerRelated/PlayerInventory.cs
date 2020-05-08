﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    private Inventory inventory;

    private LifePlayer m_lifePlayer;

    public IntVar m_inventorySpace;

    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] TextMeshProUGUI m_amounItemsInventory;

    // Start is called before the first frame update
    private void Awake()
    {
        m_lifePlayer = GetComponent<LifePlayer>();
        inventory = new Inventory(UseItem);
        //uiInventory.SetPlayer(this);
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
            m_amounItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count }/{m_inventorySpace.Value}";

            if (itemWorld != null)
            {
                if (collider.gameObject.tag != "Tools")
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

        }

    }

    private void UseItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.baton:
                m_lifePlayer.HealingFunc();

                break;
            case Item.ItemType.tissu:
                Debug.Log("Carré utilisé");

                break;
            case Item.ItemType.mrcFer:
                Debug.Log("Triangle utilisé");

                break;
            case Item.ItemType.hache:
                Debug.Log("Losange utilisé");
                break;

        }
        inventory.RemoveItem(new Item { itemType = itemType, amount = 1 });

    }
}
