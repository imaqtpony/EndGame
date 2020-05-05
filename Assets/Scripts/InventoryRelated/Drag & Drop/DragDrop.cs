using System.Collections;
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

    [SerializeField] Sprite circle;
    [SerializeField] Sprite square;
    [SerializeField] Sprite triangle;

    [SerializeField] UI_Inventory uiInventory;

    [SerializeField] Transform m_selectedIndicator;

    public static int m_amountItemToDrop;

    public bool m_isOnSlot;

    public static bool m_isRessource;

    public static Item.ItemType itemType;

    

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
            float timeSinceLastClic = Time.time - lastClickTime;

            lastClickTime = Time.time;
            Debug.Log(timeSinceLastClic);
            if (timeSinceLastClic < 0.2f)
            {
                uiInventory.UseItemFunction(itemType);

            }
        }
    }

    public void DeplaceItemText()
    {
        m_amountText.transform.position = new Vector2(gameObject.transform.position.x + 40, gameObject.transform.position.y - 50);
    }

    public Item.ItemType DetectItem()
    {
        var itemSprite = image.sprite;


        switch (itemSprite.name)
        {

            case "circle":
                itemType = Item.ItemType.circle;
                m_amountItemToDrop = Inventory.m_amountCircle;
                m_isRessource = true;
                break;
            case "square":
                itemType = Item.ItemType.square;
                m_amountItemToDrop = Inventory.m_amountSquare;
                m_isRessource = true;

                break;
            case "triangle":
                itemType = Item.ItemType.triangle;
                m_amountItemToDrop = Inventory.m_amountTriangle;
                m_isRessource = true;

                break;
            case "losange":
                itemType = Item.ItemType.losange;
                m_amountItemToDrop = 1;
                m_isRessource = false;

                break;
            case "squarangle":
                itemType = Item.ItemType.squarangle;
                m_amountItemToDrop = 1;
                m_isRessource = false;

                break;
        }
        Debug.Log(itemType);

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


}
