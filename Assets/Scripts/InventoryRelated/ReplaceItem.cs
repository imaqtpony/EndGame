using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// replace the item at its initial pos in the inventory
/// </summary>
public class ReplaceItem : MonoBehaviour, IDropHandler
{

    [SerializeField] DragDrop DragDrop;
    [SerializeField] UI_Inventory m_uiInventory;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //we check if the item slot has already an item on it 
            if (gameObject.transform.Find("Item") == null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().position;
                m_uiInventory.RemoveItemFromCraftSlot();
            }


        }

    }

}
