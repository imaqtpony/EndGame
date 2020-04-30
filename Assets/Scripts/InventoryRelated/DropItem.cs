using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryNS.Utils;

public class DropItem : MonoBehaviour
{

    [SerializeField]private UI_Inventory uiInventory;

    public void DropItemFunc ()
    {
        uiInventory.DropItemFunction(DragDrop.itemType);
    }
}
