using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private bool m_InventoryEnabled;
    [SerializeField] GameObject m_Inventory;

    private void Awake()
    {

        m_Inventory.SetActive(true);

    }

    private void Start()
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
            //Time.timeScale = 0f;

        }
        else
        {
            m_Inventory.SetActive(false);
            Debug.Log("Inventory Closed !");
            //Time.timeScale = 1f;

        }
    }

}
