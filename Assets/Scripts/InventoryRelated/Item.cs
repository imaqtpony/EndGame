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
        gros_caillou,
        poudre,

        hache,
        allumette,
        hache_pierre,
        echelle,
        plan_echelle,

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
            case ItemType.plan_echelle: return ItemAssets.Instance.plan_echelleSprite;
            case ItemType.gros_caillou: return ItemAssets.Instance.gros_caillouSprite;
            case ItemType.poudre: return ItemAssets.Instance.poudreSprite;
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
            case ItemType.gros_caillou:
            case ItemType.poudre:
                return true;
            case ItemType.hache:
            case ItemType.hache_pierre:
            case ItemType.allumette:
            case ItemType.echelle:
            case ItemType.plan_echelle:
                return false;
        }
    }
}
