using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    public static bool m_canUseItem;

    private Renderer m_meshRenderer;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_meshRenderer = GetComponent<Renderer>();
    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_animatorSlotLevier.SetTrigger("FlashingQuestObject");
            m_canUseItem = true;
            if (UI_QuestObjects.m_activateLevier)
            {
                m_meshRenderer.enabled = true;
                m_animator.SetTrigger("Activate");
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
