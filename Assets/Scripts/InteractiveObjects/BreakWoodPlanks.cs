//Last Edited : 30/05

using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Attach this to the Planches_qui_tombent GO in the second dungeon
/// the player has to use the axe to interact
/// </summary>
public class BreakWoodPlanks : MonoBehaviour
{
    [SerializeField]
    private NavMeshObstacle m_nmObs;

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    [SerializeField]
    private Animator m_animPlanks;

    [SerializeField]
    private Collider m_boxCol;

    private bool m_cutTheBranch;
    private bool m_planksDown;

    private void OnTriggerEnter(Collider p_other)
    {
        //blocks the way when the player is in front of this Go
        if (p_other.gameObject.CompareTag("Player") && m_planksDown)
            m_nmObs.enabled = true;

        if (p_other.gameObject.CompareTag("Axe") && m_cutTheBranch)
        {
            m_planksDown = true;

            m_audioSource.PlayOneShot(m_audioManager.m_fallingTreeSound);

            m_animPlanks.enabled = true;
            if (m_animPlanks.GetCurrentAnimatorStateInfo(0).IsName("New State")) m_animPlanks.SetTrigger("Timber");

            //the player can pass
            m_nmObs.enabled = false;

            m_boxCol.enabled = false;
        }
    }

    #region EVENT CUT WITH AXE
    private void OnEnable()
    {
        m_animPlanks.enabled = false;
        m_nmObs.enabled = true;

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
