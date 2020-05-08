using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        baton,
        tissu,
        mrcFer,
        hache,
        torche,

    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.baton: return ItemAssets.Instance.batonSprite;
            case ItemType.tissu: return ItemAssets.Instance.tissuSprite;
            case ItemType.mrcFer: return ItemAssets.Instance.mrcFerSprite;
            case ItemType.hache: return ItemAssets.Instance.hacheSprite;
            case ItemType.torche: return ItemAssets.Instance.torcheSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.baton:
            case ItemType.tissu:
            case ItemType.mrcFer:
                return true;
            case ItemType.hache:
            case ItemType.torche:
                return false;
        }
    }
}
