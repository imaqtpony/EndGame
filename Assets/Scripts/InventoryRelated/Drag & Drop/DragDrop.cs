using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// the scripts manageS the interaction between the player and the items in the invenetory
/// Drag
/// Drop
/// Click
/// </summary>
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{

    private RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    //"Canvas" in the hierarchy to get its scale factor
    [SerializeField] Canvas m_canvas;

    public Transform itemSlot;
    public Transform m_thisItem;

    public Image m_itemImage;

    //used only the the tools section
    [SerializeField] Transform m_selectedIndicator;

    [Header("NOTIFICATION RELATED")]
    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    public static int m_amountItemToDrop;

    //check if it is a ressource or a tool
    public static bool m_isRessource;

    public static Item.ItemType m_itemType;

    //list of the tools on the player
    [SerializeField] List<GameObject> m_tools;

    [Header("SOUND RELATED")]
    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    [SerializeField] Animator m_animatorPlayer;

    [SerializeField] GameObject m_cursor;

    [SerializeField] AutoDisableNotification m_autoDisableNotification;

    [SerializeField] QuestManager m_questManager;


    /// <summary>
    /// we get the rect transform of the item sprite
    /// then the canvas group for the alpha
    /// the quest manager for the tutorials
    /// </summary>
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        //the name of the item is the name of his sprite
        gameObject.name = m_itemImage.sprite.name;

    }

    /// <summary>
    /// when we grab the item
    /// </summary>
    /// <param name="eventData">related to the pointer</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //we put it above all the others slots to not be hidden
        itemSlot.transform.SetSiblingIndex(Inventory.itemList.Count);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        //we play the click sound for the feedbacks
        m_audioSource.PlayOneShot(m_audioManager.m_clickSound);

        //and we check what type this item is
        DetectItem();

    }
    /// <summary>
    /// when we azre dragging the object
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //we adjust the drag of the item with the finger with the canvas scale factor
        rectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;

    }

    /// <summary>
    /// when we click on the item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        DetectItem();
    }

    /// <summary>
    /// when we end dragging
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //we reset the values
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

    }



    /// <summary>
    /// we detect what item we grab and associate a type
    /// </summary>
    /// <returns></returns>
    public Item.ItemType DetectItem()
    {
        var itemSprite = m_itemImage.sprite;

        switch (itemSprite.name)
        {
            //we check the sprite name
            case "baton":
                //we give the correct type
                m_itemType = Item.ItemType.baton;

                //the good amount
                m_amountItemToDrop = Inventory.m_amountBaton;

                //and check if it is a ressource
                m_isRessource = true;
                break;

            case "tissu":
                m_itemType = Item.ItemType.tissu;
                m_amountItemToDrop = Inventory.m_amountTissu;
                m_isRessource = true;
                break;

            case "mrcFer":
                m_itemType = Item.ItemType.mrcFer;
                m_amountItemToDrop = Inventory.m_amountMrcFer;
                m_isRessource = true;
                break;

            case "caillou":
                m_itemType = Item.ItemType.caillou;
                m_amountItemToDrop = Inventory.m_amountCaillou;
                m_isRessource = true;
                break;

            case "poudre":
                m_itemType = Item.ItemType.poudre;
                m_amountItemToDrop = Inventory.m_amountPoudre;
                m_isRessource = true;
                break;

            case "hache":
                m_itemType = Item.ItemType.hache;
                m_amountItemToDrop = 1;
                m_isRessource = false;

                //when we click on a tool
                SelectTools(m_itemType);
                break;

            case "allumette":
                m_itemType = Item.ItemType.allumette;
                m_amountItemToDrop = 1;
                m_isRessource = false;
                SelectTools(m_itemType);
                break;

            case "hache_pierre":
                m_itemType = Item.ItemType.hache_pierre;
                m_amountItemToDrop = 1;
                m_isRessource = false;
                SelectTools(m_itemType);
                break;

            case "echelle":
                m_itemType = Item.ItemType.echelle;
                m_amountItemToDrop = 1;
                m_isRessource = false;
                SelectTools(m_itemType);

                break;
        }

        //we can equip a tool only if the inventory is closed
        if (!m_isRessource && !InventoryButton.m_InventoryEnabled)
        {
            m_selectedIndicator.position = transform.position;
        }

        return m_itemType;

    }

    /// <summary>
    /// when we close the tools window
    /// </summary>
    public void ReplaceIndicator()
    {
        m_selectedIndicator.position = new Vector2(100, 100000);
    }

    /// <summary>
    /// the function is executed when we click on a tool on our inventory to equip it
    /// </summary>
    /// <param name="p_toolsType">the tool that we equip</param>
    public void SelectTools(Item.ItemType p_toolsType)
    {
        //if the current animation is not IdleOutils or CourseOutils, we play IdleOutils
        if(!m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils") || !m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils")) m_animatorPlayer.SetTrigger("IdleOutils"); 

        //tutorial how to use a tool
        if (!m_questManager.m_tutoToolsDone)
        {
            m_notification.SetActive(true);
            m_textNotification.text = "Glissez votre doigt pour utiliser l'outil.";

            m_cursor.SetActive(true);
            m_autoDisableNotification.PlayAnimCursor("Swipe");

            //tutorial done
            m_questManager.m_tutoToolsDone = true;
        }

        //if the inventory is closed
        if (!InventoryButton.m_InventoryEnabled)
        {
            //we execute this as many times there are tools
            for (int i = 0; i < m_tools.Count + 1; i++)
            {
                //if the tools on the player (the 3D object) has the same name of the sprite that we clicked on
                if (m_tools[i].name == p_toolsType.ToString())
                {
                    //we disabled all the tools
                    foreach (GameObject tools in m_tools)
                    {
                        tools.SetActive(false);
                    }

                    //except the good one
                    m_tools[i].SetActive(true);
                    break;
                }

            }
        }
        
    }
 
}
