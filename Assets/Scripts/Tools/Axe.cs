using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2Lib;
using System;

public class Axe : MonoBehaviour
{

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onSwipe;

    private bool m_isCurrentlyCutting;


    private void Awake()
    {
        m_isCurrentlyCutting = false;
    }


    private void OnEnable()
    {
        if (m_onSwipe != null)
            m_onSwipe.Register(HandleCutOnSwipe);
    }
    private void OnDisable()
    {
        if (m_onSwipe != null)
            m_onSwipe.Unregister(HandleCutOnSwipe);
    }

    private void HandleCutOnSwipe(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out SwipeData sData, p_params))
        {
            Debug.Log("Axe is cutting stuff !");
            m_isCurrentlyCutting = true;
            m_onCutWithAxe.Raise(m_isCurrentlyCutting, sData);

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }
}
