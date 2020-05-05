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

    public bool m_craftActive;

    public void CheckCraftSlot()
    {
        Debug.Log("ON PEUT COMMENCER A CRAFTER");
        if (m_craftSlotList.Find(w => string.Equals(w.name, "circle")) != null && (m_craftSlotList.Find(w => string.Equals(w.name, "triangle")) != null))
        {
            m_itemResult.sprite = ItemAssets.Instance.losangeSprite;
            m_craftActive = true;
        }
    }

    public void NotEnoughItemToCraft()
    {
        m_itemResult.sprite = mask;
        m_craftActive = false;

    }

}
