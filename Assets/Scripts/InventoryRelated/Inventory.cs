using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GD2Lib;

public class Inventory
{
    public static List<Item> itemList;
    public static List<Item> toolsList;

    private Action<Item.ItemType> useItemAction;

    public event EventHandler OnItemListChanged;
    public event EventHandler OnToolsListChanged;

    public static int m_amountBaton;
    public static int m_amountTissu;
    public static int m_amountMrcFer;
    public static int m_amountCaillou;
    public static int m_amountGros_caillou;
    public static int m_amountPoudre;

    public Inventory(Action<Item.ItemType> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        toolsList = new List<Item>();

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

    public void AddTools(Item item)
    {

        toolsList.Add(item);

        OnToolsListChanged?.Invoke(this, EventArgs.Empty);
        AmountItems(item);
    }

    public void AmountItems(Item item)
    {
        foreach (Item inventoryItem in itemList)
        {
            switch (inventoryItem.itemType)
            {
                default:
                case Item.ItemType.baton:
                    m_amountBaton = inventoryItem.amount;
                    
                    break;
                case Item.ItemType.tissu:
                    m_amountTissu = inventoryItem.amount;

                    break;
                case Item.ItemType.mrcFer:
                    m_amountMrcFer = inventoryItem.amount;

                    break;
                case Item.ItemType.caillou:
                    m_amountCaillou = inventoryItem.amount;
                    break;
                case Item.ItemType.gros_caillou:
                    m_amountGros_caillou = inventoryItem.amount;
                    break;
                case Item.ItemType.poudre:
                    m_amountPoudre = inventoryItem.amount;
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

        AmountItems(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void RemoveTools(Item item)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in toolsList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount -= item.amount;
                itemInInventory = inventoryItem;

            }
        }
        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            toolsList.Remove(itemInInventory);

        }

        AmountItems(item);
        OnToolsListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void RemoveAllItems()
    {
        
        itemList.Clear();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        OnToolsListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void RemoveAllTools()
    {

        toolsList.Clear();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        OnToolsListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void UseItem(Item.ItemType item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public List<Item> GetToolsList()
    {
        return toolsList;
    }
}
