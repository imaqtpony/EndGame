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

    [SerializeField] Player player;

    [SerializeField] List<GameObject> m_tools;


    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.m_isRessource)
        {
            m_uiInventory.DropItemFunction(DragDrop.itemType, DragDrop.m_amountItemToDrop);

        }
        else if (!DragDrop.m_isRessource)
        {
            for (int i = 0; i < m_tools.Count + 1; i++)
            {

                if (m_tools[i].name == DragDrop.itemType.ToString())
                {
                    m_tools[i].SetActive(false);
                    break;
                }

            }
            m_uiInventory.DropToolFunction(DragDrop.itemType, DragDrop.m_amountItemToDrop);

            
        }


    }

}
