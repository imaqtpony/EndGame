using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Item1,
        Item2,
        Item3,
        Item4,

    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.item1Sprite;
            case ItemType.Item2: return ItemAssets.Instance.item2Sprite;
            case ItemType.Item3: return ItemAssets.Instance.item3Sprite;
            case ItemType.Item4: return ItemAssets.Instance.item4Sprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1:
            case ItemType.Item2:
            case ItemType.Item3:
                return true;
            case ItemType.Item4:
                return false;
        }
    }
}
