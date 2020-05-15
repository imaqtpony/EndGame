using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    [SerializeField] Animator m_animatorMusicBox;
    public static bool m_canUseItem;

    private Renderer m_meshRenderer;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_meshRenderer = GetComponent<Renderer>();
        m_canUseItem = false;
    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && m_canUseItem)
        {
            m_animatorSlotLevier.SetTrigger("FlashingQuestObject");
            if (UI_QuestObjects.m_activateLevier)
            {
                m_meshRenderer.enabled = true;
                m_animator.SetTrigger("Activate");
                m_animatorMusicBox.SetTrigger("Activate");
            }
        }
        
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_canUseItem = false;
            m_animatorSlotLevier.SetTrigger("DisplayQuestObject");
        }
    }
}
