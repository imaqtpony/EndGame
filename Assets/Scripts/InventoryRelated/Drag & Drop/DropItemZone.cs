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
        else
        {
            m_uiInventory.DropToolFunction(DragDrop.itemType, DragDrop.m_amountItemToDrop);

        }


    }

}
