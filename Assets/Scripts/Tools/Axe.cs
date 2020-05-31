//Last Edited : 30/05

using UnityEngine;
using GD2Lib;
using System;

/// <summary>
/// The axe tool behaviour when equipped
/// Attach this to the children objets of the player that are tagged as Axe
/// </summary>
public class Axe : MonoBehaviour
{

    [SerializeField]
    private GD2Lib.Event m_onCutWithAxe;

    [SerializeField]
    private GD2Lib.Event m_onSwipe;

    [SerializeField] [Tooltip("Attach player animator component")] Animator m_playerAnimator;

    private bool m_isCurrentlyCutting;

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            //anim idleoutils
            if (m_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tools")) m_playerAnimator.SetTrigger("IdleOutils");

            m_isCurrentlyCutting = false;
        }
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

            //when this handle triggers the player is using the axe
            //Raise this event to convey the message to other objets registered
            m_onCutWithAxe.Raise(m_isCurrentlyCutting, sData);

            //anim hache

            if (m_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils")) m_playerAnimator.SetTrigger("ToolsSwing");

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }

    /// <summary>
    /// Rotate the axe depending on the direction
    /// NOT USED
    /// </summary>
    private Quaternion RotateAxe(SwipeData p_sData)
    {
        Quaternion rota;

        if (p_sData.endPosition.x - p_sData.startPosition.x < 0 && gameObject.CompareTag("StoneAxe"))
        {
            return rota = transform.parent.rotation * Quaternion.Euler(180, 0, 90);

        }
        else if (p_sData.endPosition.x - p_sData.startPosition.x > 0 && gameObject.CompareTag("StoneAxe"))
        {
            //Right
            return rota = transform.parent.rotation * Quaternion.Euler(180, 0, -90);
        }

        if (p_sData.endPosition.x - p_sData.startPosition.x < 0 && gameObject.CompareTag("Axe"))
        {
            //left
            return rota = transform.parent.rotation * Quaternion.Euler(0, 0, 180);

        }
        else
        {
            //Right
            return rota = transform.parent.rotation;

        }
    }

}
