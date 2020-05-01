﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public static List<Item> itemList;

    private Action<Item.ItemType> useItemAction;

    public event EventHandler OnItemListChanged;

    public static int m_amountCircle;
    public static int m_amountSquare;
    public static int m_amountTriangle;

    public Inventory(Action<Item.ItemType> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);

            }
        }
        else
        {
            itemList.Add(item);

        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        AmountItems(item);
    }

    public void AmountItems(Item item)
    {
        foreach (Item inventoryItem in itemList)
        {
            switch (inventoryItem.itemType)
            {

                case Item.ItemType.circle:
                    m_amountCircle = inventoryItem.amount;
                    
                    break;
                case Item.ItemType.square:
                    m_amountSquare = inventoryItem.amount;

                    break;
                case Item.ItemType.triangle:
                    m_amountTriangle = inventoryItem.amount;

                    break;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount -= item.amount;
                itemInInventory = inventoryItem;

            }
        }
        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            itemList.Remove(itemInInventory);

        }

        /*else
        {
            itemList.Remove(item);

        }*/
        AmountItems(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveAllItems()
    {
        
        itemList.Clear();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void UseItem(Item.ItemType item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
