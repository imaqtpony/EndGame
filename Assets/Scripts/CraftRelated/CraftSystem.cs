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

    private Inventory inventory;

    public List<Transform> m_craftSlotList;

    public static Item.ItemType m_itemType_1;
    public static Item.ItemType m_itemType_2;

    public bool m_craftActive;

    [SerializeField] CanvasGroup m_craftButton;

    public void CheckCraftSlot()
    {

        if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "mrcFer")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.hacheSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.hache;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "poudre")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.allumetteSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.allumette;
            m_craftButton.alpha = 1f;

        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "caillou")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.hache_pierreSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.hache_pierre;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && BluePrintObjects.m_ladderBluePrintDiscovered)
        {
            m_itemResult.sprite = ItemAssets.Instance.echelleSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.echelle;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "allumette")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.decraftAllumetteSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.tissu;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "hache")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.decraftHacheFerSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.mrcFer;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "hache_pierre")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.decraftHachePierreSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.baton;
            m_itemType_2 = Item.ItemType.caillou;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "echelle")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.batonSprite;
            m_craftActive = true;
            m_itemType_1 = Item.ItemType.baton;
            m_craftButton.alpha = 1f;
        }
        else
        {
            NotEnoughItemToCraft();
        }
        

    }


    public void NotEnoughItemToCraft()
    {
        m_itemResult.sprite = mask;
        m_craftActive = false;
        m_craftButton.alpha = .5f;

    }

}
