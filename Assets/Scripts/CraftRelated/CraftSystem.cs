﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using InventoryNS.Utils;

public class CraftSystem : MonoBehaviour
{
    [Header("Basic Objects")]
    [SerializeField] Sprite circle;
    [SerializeField] Sprite square;
    [SerializeField] Sprite triangle;

    [Header("Item results sprites")]
    [SerializeField] Image m_itemResult;
    [SerializeField] Sprite mask;
    [SerializeField] Sprite losange;

    private Inventory inventory;

    public List<Transform> m_craftSlotList;

    public bool m_craftActive;

    public void CheckCraftSlot()
    {
        Debug.Log("ON PEUT COMMENCER A CRAFTER");
        if (m_craftSlotList[0].name == "circle" && m_craftSlotList[1].name == "triangle" || m_craftSlotList[0].name == "triangle" && m_craftSlotList[1].name == "circle")
        {
            m_itemResult.sprite = losange;
            m_craftActive = true;
        }
    }

    public void NotEnoughItemToCraft()
    {
        m_itemResult.sprite = mask;
        m_craftActive = false;

    }

}