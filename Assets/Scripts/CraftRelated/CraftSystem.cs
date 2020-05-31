using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using InventoryNS.Utils;
using System.Security.Cryptography;

public class CraftSystem : MonoBehaviour
{
    [Header("Item results sprites")]
    [SerializeField] Image m_itemResult;
    [SerializeField] Sprite mask;

    [Header("The craft slots")]
    public List<Transform> m_craftSlotList;

    [Header("Sprites of the craft button")]
    [SerializeField] Sprite m_craftSprite;
    [SerializeField] Sprite m_uncraftSprite;
    [SerializeField] Image m_craftButtonSprite;
    [SerializeField] CanvasGroup m_craftButton;


    [Header("The items that we will get after the craft/uncraft")]
    public static Item.ItemType m_itemType_1;
    public static Item.ItemType m_itemType_2;

    [Header("Check if the items make a combination")]
    public bool m_craftActive;

    /// <summary>
    /// Check the actual items of the craft slot and check the combination
    /// </summary>
    public void CheckCraftSlot()
    {
        #region CRAFT SECTION

        //if in the slots there is a baton and a peace of iron 
        if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "mrcFer")) != null))
        {
            //=> display the axe sprite in the item slot result
            //we get the instance of the sprite
            m_itemResult.sprite = ItemAssets.Instance.m_hacheSprite;

            //we can craft now
            m_craftActive = true;

            //we confirm that it will be an axe
            m_itemType_1 = Item.ItemType.hache;

            //the craft button is in highlight to show the player the possibility to craft
            m_craftButton.alpha = 1f;

            //the craft button sprite is now a hammer
            m_craftButtonSprite.sprite = m_craftSprite;

        }

        //same thing for all crafts
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "poudre")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.m_allumetteSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.allumette;
            m_craftButton.alpha = 1f;
            m_craftButtonSprite.sprite = m_craftSprite;

        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "caillou")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.m_hache_pierreSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.hache_pierre;
            m_craftButton.alpha = 1f;
            m_craftButtonSprite.sprite = m_craftSprite;


        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && BluePrintObjects.m_ladderBluePrintDiscovered)
        {
            m_itemResult.sprite = ItemAssets.Instance.m_echelleSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.echelle;
            m_craftButton.alpha = 1f;
            m_craftButtonSprite.sprite = m_craftSprite;


        }
        #endregion

        #region UNCRAFT SECTION

        //for the uncraft, the principle is the same but we check the tool on the slot and give the ressources
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "allumette")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.m_decraftAllumetteSprite;
            m_craftActive = true;

            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.poudre;

            m_craftButton.alpha = 1f;

            //the craft button sprite is now the recycling logo
            m_craftButtonSprite.sprite = m_uncraftSprite;
        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "hache")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.m_decraftHacheFerSprite;
            m_craftActive = true;

            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.mrcFer;

            m_craftButton.alpha = 1f;
            m_craftButtonSprite.sprite = m_uncraftSprite;

        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "hache_pierre")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.m_decraftHachePierreSprite;
            m_craftActive = true;

            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.caillou;

            m_craftButton.alpha = 1f;
            m_craftButtonSprite.sprite = m_uncraftSprite;

        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "echelle")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.m_batonSprite;
            m_craftActive = true;

            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.plan_echelle;

            m_craftButtonSprite.sprite = m_uncraftSprite;
            m_craftButton.alpha = 1f;
        }
        #endregion

        else
        {
            NotEnoughItemToCraft();
        }


    }

    /// <summary>
    /// If there is no combination
    /// we reset the item slot result visual
    /// </summary>
    public void NotEnoughItemToCraft()
    {
        m_itemResult.sprite = mask;
        m_craftActive = false;
        m_craftButton.alpha = .5f;
        m_craftButtonSprite.sprite = m_craftSprite;

    }

}
