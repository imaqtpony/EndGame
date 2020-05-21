using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    [SerializeField] Animator m_animatorMusicBox;
    public static bool m_canUseItem;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField] GameObject m_slotLevier;

    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    private Renderer m_meshRenderer;


    private void Start()
    {

        m_audioManager.m_audioSource = GetComponent<AudioSource>();
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
                m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_musicBoxSound);

                m_meshRenderer.enabled = true;
                m_animator.SetTrigger("Activate");
                m_animatorMusicBox.SetTrigger("Activate");
                m_questSystem.ChangeQuest("Allumez les bougies.");
                m_questManager.m_candleEnigmaDone = true;
                m_slotLevier.SetActive(false);
                UI_QuestObjects.m_activateLevier = false;
            }
        }
        
    }

    public void ShowSlot(string p_nameObject)
    {
        if(p_nameObject == "Levier")
        {
            m_slotLevier.SetActive(true);
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
