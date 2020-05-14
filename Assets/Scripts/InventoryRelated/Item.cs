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
        caillou,
        hache,
        allumette,
        hache_pierre,
        echelle,
        masse,

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
            case ItemType.caillou: return ItemAssets.Instance.caillouSprite;
            case ItemType.hache: return ItemAssets.Instance.hacheSprite;
            case ItemType.allumette: return ItemAssets.Instance.allumetteSprite;
            case ItemType.hache_pierre: return ItemAssets.Instance.hache_pierreSprite;
            case ItemType.echelle: return ItemAssets.Instance.echelleSprite;
            case ItemType.masse: return ItemAssets.Instance.masseSprite;
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
            case ItemType.caillou:
                return true;
            case ItemType.hache:
            case ItemType.hache_pierre:
            case ItemType.allumette:
            case ItemType.echelle:
            case ItemType.masse:
                return false;
        }
    }
}
