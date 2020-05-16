using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    [SerializeField] Animator m_animatorMusicBox;
    public static bool m_canUseItem;

    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    private Renderer m_meshRenderer;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_meshRenderer = GetComponent<Renderer>();
        m_canUseItem = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !m_questManager.m_levierEnigmaDone)
        {
            m_questSystem.ChangeQuest("Trouvez un levier.");
            m_questManager.m_levierEnigmaDone = true;

        }
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
                m_questSystem.ChangeQuest("Allumez les bougies.");
                m_questManager.m_candleEnigmaDone = true;

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
