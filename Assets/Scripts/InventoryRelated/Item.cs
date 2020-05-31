using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script which manages all the items in the game
/// </summary>
[Serializable]
public class Item
{
    /// <summary>
    /// all the items types
    /// </summary>
    public enum ItemType
    {
        //ressources
        baton,
        tissu,
        mrcFer,
        caillou,
        poudre,

        //tools
        hache,
        allumette,
        hache_pierre,
        echelle,

        //blueprint
        plan_echelle,

    }

    public ItemType itemType;
    public int amount;

    /// <summary>
    /// we associate the sprite according to the item type
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.baton: return ItemAssets.Instance.m_batonSprite;
            case ItemType.tissu: return ItemAssets.Instance.m_tissuSprite;
            case ItemType.mrcFer: return ItemAssets.Instance.m_mrcFerSprite;
            case ItemType.caillou: return ItemAssets.Instance.m_caillouSprite;
            case ItemType.hache: return ItemAssets.Instance.m_hacheSprite;
            case ItemType.allumette: return ItemAssets.Instance.m_allumetteSprite;
            case ItemType.hache_pierre: return ItemAssets.Instance.m_hache_pierreSprite;
            case ItemType.echelle: return ItemAssets.Instance.m_echelleSprite;
            case ItemType.plan_echelle: return ItemAssets.Instance.m_plan_echelleSprite;
            case ItemType.poudre: return ItemAssets.Instance.m_poudreSprite;
        }
    }

    /// <summary>
    /// we set them stackable or not
    /// </summary>
    /// <returns></returns>
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.baton:
            case ItemType.tissu:
            case ItemType.mrcFer:
            case ItemType.caillou:
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
