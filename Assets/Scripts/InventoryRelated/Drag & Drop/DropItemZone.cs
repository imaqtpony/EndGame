using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventoryNS.Utils;
using UnityEngine.EventSystems;

/// <summary>
/// this script is for the drop of item when we drag it out of the inventory
/// </summary>
public class DropItemZone : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    [SerializeField] UI_Inventory m_uiInventory;

    [Header("TOOLS ON THE PLAYER")]
    [SerializeField] List<GameObject> m_tools;

    [SerializeField] Animator m_animatorPlayer;

    public static bool m_canHarvestItem;

    //make sure that the collider of the player is activated after closing the inventory
    private void Start()
    {
        m_canHarvestItem = true;

    }

    private void OnDisable()
    {
        m_canHarvestItem = true;

    }

    /// <summary>
    /// when we drop an item on this zone of the UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        //if it is a ressource, we drop the good amount and "true" is to make sure that it is a ressource on UI_Inventory script
        if (DragDrop.m_isRessource)
        {
            m_uiInventory.DropItemFunction(DragDrop.m_itemType, DragDrop.m_amountItemToDrop, true);

        }

        //if it is a tool
        else if (!DragDrop.m_isRessource)
        {
            //we do the same process as in the drag drop script
            for (int i = 0; i < m_tools.Count + 1; i++)
            {

                if (m_tools[i].name == DragDrop.m_itemType.ToString())
                {
                    //if we drop the tool in our hand and the player is not moving, we play the animation idle
                    if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Tools"))
                    {
                        m_animatorPlayer.SetTrigger("Idle");

                    }
                    //else if the player is moving we play the animation course
                    else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils")) m_animatorPlayer.SetTrigger("Course");
                    m_tools[i].SetActive(false);
                    break;
                }

            }
            m_uiInventory.DropItemFunction(DragDrop.m_itemType, DragDrop.m_amountItemToDrop, false);
            
        }

        //we diasble the collider of the player during an amount of times to make sure the item doesn't go on his inventory immediately
        m_canHarvestItem = false;

        StartCoroutine(DeactivateCollider());

    }

    
    /// <summary>
    /// we re activate the collider 1.5 second later
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeactivateCollider()
    {
        yield return new WaitForSeconds(1.5f);
        m_canHarvestItem = true;


        //this line is a security, to make sure that we stop the coroutine
        StopCoroutine(DeactivateCollider());

    }

}
