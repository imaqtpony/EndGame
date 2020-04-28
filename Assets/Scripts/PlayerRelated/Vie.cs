using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GD2Lib;

public class Vie : MonoBehaviour
{
    [SerializeField] int m_vie;

    public IntVar m_ScriptableObject;

    private void Start()
    {
        Debug.Log(m_ScriptableObject.Value);

    }

    
    private void OnEnable()
    {
        m_ScriptableObject.OnValueChanged += HandleVie;
        m_ScriptableObject.Value = 3;
    }

    private void OnDisable()
    {
        m_ScriptableObject.OnValueChanged -= HandleVie;
    }

    private void OnApplicationQuit()
    {
        m_ScriptableObject.Value = 3;
    }

    public void HandleVie(int m_vie)
    {

    }
}
