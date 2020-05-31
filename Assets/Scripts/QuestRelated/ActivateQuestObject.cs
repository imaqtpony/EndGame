using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when we use the quest object at the good area
/// </summary>
public class ActivateQuestObject : MonoBehaviour
{
    [Header("ALL THE ANIMATORS")]
    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    [SerializeField] Animator m_animatorMusicBox;

    public static bool m_gotLever;
    public static bool m_canUseItem;

    [SerializeField] AudioManager m_audioManager;
    private AudioSource m_audioSource;

    [Header("SLOT RELATED")]
    [SerializeField] GameObject m_slotLevier;
    [SerializeField] GameObject m_slotKey;

    [Header("QUEST RELATED")]
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
            m_questSystem.ChangeQuest("Trouvez un levier");

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
        m_questManager.m_candleEnigmaDone = true;
        UI_QuestObjects.m_activateLevier = false;

        yield return new WaitForSeconds(.1f);
        //avoid the player to move when we click on it, that's why there is a little delay
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
