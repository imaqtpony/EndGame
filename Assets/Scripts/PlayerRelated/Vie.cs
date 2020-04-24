using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vie : MonoBehaviour
{
    [SerializeField] int m_vie;

    public SerInt m_ScriptablObject;

    private void Start()
    {
        Debug.Log(m_ScriptablObject.Value);

    }

    
    private void OnEnable()
    {
        m_ScriptablObject.onValueChange += HandleVie;
        m_ScriptablObject.Value = 3;
    }

    private void OnDisable()
    {
        m_ScriptablObject.onValueChange -= HandleVie;
    }

    private void OnApplicationQuit()
    {
        m_ScriptablObject.Value = 3;
    }

    public void HandleVie(int m_vie)
    {

    }
}
