using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GD2Lib;

/// <summary>
/// maximum life value of the player
/// </summary>
public class Vie : MonoBehaviour
{
    [SerializeField] public int m_vie;

    public IntVar m_ScriptableObject;
    
    private void OnEnable()
    {
        m_ScriptableObject.OnValueChanged += HandleVie;
        m_ScriptableObject.Value = m_vie;
    }

    private void OnDisable()
    {
        m_ScriptableObject.OnValueChanged -= HandleVie;
    }

    private void OnApplicationQuit()
    {
        m_ScriptableObject.Value = m_vie;
    }

    public void HandleVie(int m_vie)
    {
        //useless for the moment
    }
}
