﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public Transform itemSlot;
    public Transform m_amountText;
    public Transform m_thisItem;

    public Image image;

    [SerializeField] UI_Inventory uiInventory;

    [SerializeField] Transform m_selectedIndicator;

    public static int m_amountItemToDrop;

    public bool m_isOnSlot;

    public static bool m_isRessource;

    public static Item.ItemType itemType;

    [SerializeField] List<GameObject> m_tools;

    private float lastClickTime;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        //the name of the item is the name of his sprite
        gameObject.name = image.sprite.name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        itemSlot.transform.SetSiblingIndex(Inventory.itemList.Count);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        DetectItem();


    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / 2f;
        DeplaceItemText();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        DeplaceItemText();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DetectItem();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            lastClickTime = Time.time;
            if (timeSinceLastClick < 0.2f)
            {
                uiInventory.UseItemFunction(itemType);

            }
        }
    }

    public void DeplaceItemText()
    {
        //m_amountText.transform.position = new Vector2(transform.position.x + 40, transform.position.y - 50);
    }

    public Item.ItemType DetectItem()
    {
        var itemSprite = image.sprite;


        switch (itemSprite.name)
        {

            case "baton":
                itemType = Item.ItemType.baton;
                m_amountItemToDrop = Inventory.m_amountBaton;
                m_isRessource = true;
                break;
            case "tissu":
                itemType = Item.ItemType.tissu;
                m_amountItemToDrop = Inventory.m_amountSquare;
                m_isRessource = true;

                break;
            case "mrcFer":
                itemType = Item.ItemType.mrcFer;
                m_amountItemToDrop = Inventory.m_amountMrcFer;
                m_isRessource = true;

                break;
            case "hache":
                itemType = Item.ItemType.hache;
                m_amountItemToDrop = 1;
                m_isRessource = false;
                SelectTools(itemType);
                break;
            case "torche":
                itemType = Item.ItemType.torche;
                m_amountItemToDrop = 1;
                m_isRessource = false;
                SelectTools(itemType);
                
                //m_audioManager.m_audioSource.loop = true;

                break;
        }

        if (!m_isRessource)
        {
            m_selectedIndicator.position = transform.position;
        }
        return itemType;

    }

    public void ReplaceIndicator()
    {
        m_selectedIndicator.position = new Vector2(100, 100000);
    }

    public void SelectTools(Item.ItemType p_toolsType)
    {
        for (int i = 0; i < m_tools.Count + 1; i++)
        {
            if (m_tools[i].name == p_toolsType.ToString() && !InventoryButton.m_InventoryEnabled)
            {
                foreach(GameObject tools in m_tools)
                {
                    tools.SetActive(false);
                }
                m_tools[i].SetActive(true);
                break;
            }

        }
    }

    
}
