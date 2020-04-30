﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GD2Lib;

public class InventorySpace : MonoBehaviour
{
    [SerializeField] int m_inventorySpace;

    public IntVar m_ScriptableObject;
    
    private void OnEnable()
    {
        m_ScriptableObject.OnValueChanged += HandleInventorySpace;
        m_ScriptableObject.Value = m_inventorySpace;
    }

    private void OnDisable()
    {
        m_ScriptableObject.OnValueChanged -= HandleInventorySpace;
    }

    private void OnApplicationQuit()
    {
        m_ScriptableObject.Value = m_inventorySpace;
    }

    public void HandleInventorySpace(int m_vie)
    {

    }
}