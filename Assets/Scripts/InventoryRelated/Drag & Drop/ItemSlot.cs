using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] DragDrop DragDrop;

    private Transform m_itemChild;

    private bool m_itemIsInSlot; 
    private bool m_isCraftSlotsFilled;

    [SerializeField] CraftSystem m_craftSystem;
 
    public static Vector3 m_craftSlotPos;

    public Image m_itemSprite;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            //we check if the item slot has already an item on it or not 
            if (gameObject.transform.Find("Item") == null)
            {
                Debug.Log("OnDrop");
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                ChangingParent(eventData);

                if (m_craftSystem.m_craftSlotList.Count > 1)
                {
                    m_craftSystem.CheckCraftSlot();
                }else
                {
                    m_craftSystem.NotEnoughItemToCraft();
                }
            }

        }

    }

    public void ChangingParent(PointerEventData eventData)
    {
        m_itemChild = eventData.pointerDrag.GetComponent<RectTransform>();
        m_itemChild.transform.parent = gameObject.transform;
        m_itemChild.transform.SetSiblingIndex(2);


        if(gameObject.tag == "CraftSlot")
        {
            m_itemSprite = transform.GetChild(2).GetComponent<Image>();

            m_craftSystem.m_craftSlotList.Add(transform.GetChild(2));


        }
        else
        {
            m_craftSystem.m_craftSlotList.Remove(transform.GetChild(2));

        }
        
        
    }

}
