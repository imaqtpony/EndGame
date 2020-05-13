using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using InventoryNS.Utils;

public class CraftSystem : MonoBehaviour
{
    [Header("Item results sprites")]
    [SerializeField] Image m_itemResult;
    [SerializeField] Sprite mask;

    private Inventory inventory;

    public List<Transform> m_craftSlotList;

    public static Item.ItemType m_itemType;

    public bool m_craftActive;

    [SerializeField] CanvasGroup m_craftButton;

    public void CheckCraftSlot()
    {
        if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null)
        {
            m_itemResult.sprite = ItemAssets.Instance.echelleSprite;
            m_craftActive = true;
            m_itemType = Item.ItemType.echelle;
            m_craftButton.alpha = 1f;
            Debug.Log("echelle");
        }
        if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "mrcFer")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.hacheSprite;
            m_craftActive = true;
            m_itemType = Item.ItemType.hache;
            m_craftButton.alpha = 1f;
        }
        else if (m_craftSlotList.Find(w => string.Equals(w.name, "tissu")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.allumetteSprite;
            m_craftActive = true;
            m_itemType = Item.ItemType.allumette;
            m_craftButton.alpha = 1f;

        }

        else if (m_craftSlotList.Find(w => string.Equals(w.name, "baton")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "caillou")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.hache_pierreSprite;
            m_craftActive = true;
            m_itemType = Item.ItemType.hache_pierre;
            m_craftButton.alpha = 1f;

        }
        
    }

    public void NotEnoughItemToCraft()
    {
        m_itemResult.sprite = mask;
        m_craftActive = false;
        m_craftButton.alpha = .5f;

    }

}
