using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Sprites;

/// <summary>
/// when we drag and drop an item on the slot
/// </summary>
public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] CraftSystem m_craftSystem;
    [SerializeField] AudioManager m_audioManager;
    public UI_Inventory m_uiInventory;

    private Transform m_itemChild;
    private Image m_itemSprite;

    //when we are close enough to a workbench, we add the craft slot to a list
    private void OnEnable()
    {
        if(gameObject.tag == "CraftSlot") m_uiInventory.m_slotsForCraft.Add(transform);
        m_audioManager.m_audioSource = GetComponent<AudioSource>();

    }

    //we remove them if we are to far away of the workbench
    private void OnDisable()
    {
        if (gameObject.tag == "CraftSlot") m_uiInventory.m_slotsForCraft.Remove(transform);
        m_audioManager.m_audioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        //this line is repeated 3 times to make sure that the audio source is recovered even if we disable the gameobject
        m_audioManager.m_audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// when we drop the item of the slot
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            //we check if the item slot has already an item on it or not 
            if (gameObject.transform.Find("Item") == null)
            {
                //the item take the anchored position of the slot (it works better without .anchoredPosition)
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                ChangingParent(eventData);

                //when we place an item on the craft slot, we check if we can craft something
                m_craftSystem.CheckCraftSlot();

                //if there is something, the slot is empty
                if (m_craftSystem.m_craftSlotList.Count < 1) m_craftSystem.NotEnoughItemToCraft();

                m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_placeItemOnSlotSound);

                //if we place a tool on the craft slkot, we increase its size to fit with the slot
                if (!DragDrop.m_isRessource && !gameObject.gameObject.CompareTag("ToolsSlot"))
                {
                    float p_sizeSprite = 100f;
                    ResizeSprite(p_sizeSprite, eventData);
                }

                //and if we replace it on its own slot, we fit its size with the initial slot
                else if(!DragDrop.m_isRessource && gameObject.gameObject.CompareTag("ToolsSlot"))
                {
                    float p_sizeSprite = 67.1f;
                    ResizeSprite(p_sizeSprite, eventData);
                }

            }

        }

    }

    /// <summary>
    /// we resize the item sprite
    /// </summary>
    /// <param name="p_size">size of the item sprite</param>
    /// <param name="eventData"></param>
    public void ResizeSprite(float p_size, PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(p_size, p_size);
    }


    /// <summary>
    /// each item has a slot as parent, if we move the item on another slot, we change his parent
    /// it allows us to check in what slot the item is
    /// </summary>
    /// <param name="eventData"></param>
    public void ChangingParent(PointerEventData eventData)
    {
        //we get the rectransform of the item
        m_itemChild = eventData.pointerDrag.GetComponent<RectTransform>();

        //the parent is now the slot attached to this script
        m_itemChild.transform.parent = gameObject.transform;

        //and the child index is 2
        m_itemChild.transform.SetSiblingIndex(2);

        //if this slot is a craft slot
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
