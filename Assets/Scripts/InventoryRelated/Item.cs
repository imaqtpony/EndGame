using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        circle,
        square,
        triangle,
        losange,
        squarangle,

    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.circle: return ItemAssets.Instance.circleSprite;
            case ItemType.square: return ItemAssets.Instance.squareSprite;
            case ItemType.triangle: return ItemAssets.Instance.triangleSprite;
            case ItemType.losange: return ItemAssets.Instance.losangeSprite;
            case ItemType.squarangle: return ItemAssets.Instance.squarangleSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.circle:
            case ItemType.square:
            case ItemType.triangle:
                return true;
            case ItemType.losange:
            case ItemType.squarangle:
                return false;
        }
    }
}
