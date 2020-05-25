using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    [SerializeField] Animator m_animatorMusicBox;
    public static bool m_gotLever;
    public static bool m_canUseItem;

    [SerializeField] AudioManager m_audioManager;

    private AudioSource m_audioSource;

    [SerializeField] GameObject m_slotLevier;
    [SerializeField] GameObject m_slotKey;

    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    private Renderer m_meshRenderer;


    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        m_meshRenderer = GetComponent<Renderer>();
        m_gotLever = false;
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
        if (collider.CompareTag("Player") && m_gotLever)
        {
            m_canUseItem = true;
            m_animatorSlotLevier.SetTrigger("FlashingQuestObject");
            if (UI_QuestObjects.m_activateLevier)
            {
                StartCoroutine(ActivateObject());
            }
        }
        
    }

    private IEnumerator ActivateObject()
    {
        m_audioSource.PlayOneShot(m_audioManager.m_musicBoxSound);

        m_meshRenderer.enabled = true;
        m_animator.SetTrigger("Activate");
        m_animatorMusicBox.SetTrigger("Activate");
        m_questSystem.ChangeQuest("Allumez les bougies.");
        m_questManager.m_candleEnigmaDone = true;
        UI_QuestObjects.m_activateLevier = false;

        yield return new WaitForSeconds(.1f);
        m_slotLevier.SetActive(false);
    }

    public void ShowSlot(string p_nameObject)
    {
        if(p_nameObject == "Levier")
        {
            m_slotLevier.SetActive(true);
        }
        else if (p_nameObject == "Clef")
        {
            m_slotKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_animatorSlotLevier.SetTrigger("DisplayQuestObject");
        }
    }
}
