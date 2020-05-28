using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventoryNS.Utils;
using UnityEngine.EventSystems;


public class DropItemZone : MonoBehaviour, IDropHandler
{
    private Inventory inventory;

    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] List<GameObject> m_tools;

    [SerializeField] BoxCollider m_colliderPlayer;

    [SerializeField] Animator m_animatorPlayer;


    private void OnDisable()
    {
        if(m_colliderPlayer != null)
        {
            m_colliderPlayer.enabled = true;

        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.m_isRessource)
        {
            m_uiInventory.DropItemFunction(DragDrop.itemType, DragDrop.m_amountItemToDrop);
            Debug.Log(DragDrop.itemType);

        }

        else if (!DragDrop.m_isRessource)
        {
            for (int i = 0; i < m_tools.Count + 1; i++)
            {

                if (m_tools[i].name == DragDrop.itemType.ToString())
                {
                    if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Tools"))
                    {
                        m_animatorPlayer.SetTrigger("Idle");
                        Debug.Log("DAZCA");

                    }
                    else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils")) m_animatorPlayer.SetTrigger("Course");
                    m_tools[i].SetActive(false);
                    break;
                }

            }
            m_uiInventory.DropToolFunction(DragDrop.itemType, DragDrop.m_amountItemToDrop);
            
        }

        m_colliderPlayer.enabled = false;
        StartCoroutine(DeactivateCollider());

    }

    

    private IEnumerator DeactivateCollider()
    {
        yield return new WaitForSeconds(1);
        m_colliderPlayer.enabled = true;
        StopCoroutine(DeactivateCollider());

    }

}
