using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private bool m_InventoryEnabled;
    [SerializeField] GameObject m_Inventory;

    void OnEnable()
    {
        m_Inventory.SetActive(false);

    }

    public void OpenInventory()
    {
        m_InventoryEnabled = !m_InventoryEnabled;
        if (m_InventoryEnabled)
        {
            m_Inventory.SetActive(true);
            Debug.Log("Inventory Opened !");

        }
        else
        {
            m_Inventory.SetActive(false);
            Debug.Log("Inventory Closed !");

        }
    }

}
