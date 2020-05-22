using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakWoodPlanks : MonoBehaviour
{
    [SerializeField]
    private NavMeshObstacle m_nmObs;

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private Animator m_animPlanks;

    [SerializeField]
    private Collider m_boxCol;

    private bool m_cutTheBranch;
    private bool m_planksDown;

    private void Update()
    {
        //if (Key.m_gotKey)
        //    m_nmObs.enabled = false;
            
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (p_other.gameObject.CompareTag("Player") && m_planksDown)
            m_nmObs.enabled = true;

        if (p_other.gameObject.CompareTag("Axe") && m_cutTheBranch)
        {
            Debug.Log("Cut down these planks");
            m_planksDown = true;

            m_animPlanks.enabled = true;
            if (m_animPlanks.GetCurrentAnimatorStateInfo(0).IsName("New State")) m_animPlanks.SetTrigger("Timber");

            m_nmObs.enabled = false;

            m_boxCol.enabled = false;
        }
    }

    #region EVENT CUT WITH AXE
    private void OnEnable()
    {
        m_animPlanks.enabled = false;

        if (m_onCutWithAxe != null)
            m_onCutWithAxe.Register(HandleCutPlanks);
    }

    private void OnDisable()
    {
        if (m_onCutWithAxe != null)
            m_onCutWithAxe.Unregister(HandleCutPlanks);
    }

    private void HandleCutPlanks(GD2Lib.Event p_event, object[] p_params)
    {
        if(GD2Lib.Event.TryParseArgs(out bool axeCutting, out SwipeData sData, p_params))
        {
            m_cutTheBranch = axeCutting;

        } else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }
    #endregion

}
