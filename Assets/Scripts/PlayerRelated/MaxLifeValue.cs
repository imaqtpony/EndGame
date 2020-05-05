using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GD2Lib;

public class MaxLifeValue : MonoBehaviour
{
    [SerializeField] int m_maxVie;

    public IntVar m_ScriptableObject;

    private void Start()
    {
        //Debug.Log(m_ScriptableObject.Value);

    }

    
    private void OnEnable()
    {
        m_ScriptableObject.OnValueChanged += HandleVie;
        m_ScriptableObject.Value = m_maxVie;
    }

    private void OnDisable()
    {
        m_ScriptableObject.OnValueChanged -= HandleVie;
    }

    private void OnApplicationQuit()
    {
        m_ScriptableObject.Value = m_maxVie;
    }

    public void HandleVie(int m_vie)
    {

    }
}
