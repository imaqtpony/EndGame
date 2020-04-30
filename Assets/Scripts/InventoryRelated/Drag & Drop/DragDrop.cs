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

    public bool m_isOnSlot;

    public static Item.ItemType itemType;

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
        rectTransform.anchoredPosition += eventData.delta / 1.3f;
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

        canvasGroup.blocksRaycasts = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = true;

    }

    public void DeplaceItemText()
    {
        m_amountText.transform.position = new Vector2(gameObject.transform.position.x + 10, gameObject.transform.position.y - 15);
    }

    public Item.ItemType DetectItem()
    {
        var itemSprite = image.sprite;


        switch (itemSprite.name)
        {
            default:
            case "circle":
                itemType = Item.ItemType.Item1;
                break;
            case "square":
                itemType = Item.ItemType.Item2;
                break;
            case "triangle":
                itemType = Item.ItemType.Item3;
                break;
            case "losange":
                itemType = Item.ItemType.Item4;
                break;
        }
        Debug.Log(itemType);

        return itemType;

    }

}
