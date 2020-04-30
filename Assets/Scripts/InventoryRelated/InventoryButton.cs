﻿using System.Collections;
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


        }
        else
        {
            m_Inventory.SetActive(false);

        }
    }

}