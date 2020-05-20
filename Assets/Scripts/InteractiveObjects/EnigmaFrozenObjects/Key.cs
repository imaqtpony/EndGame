using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public static bool m_canUseItem;

    [SerializeField] private GameObject m_slotKey;

    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    [SerializeField] AudioManager m_audioManager;

    private Renderer m_meshRenderer;


    private void Start()
    {
        m_audioManager.m_audioSource = GetComponent<AudioSource>();
        m_meshRenderer = GetComponent<Renderer>();
        m_canUseItem = false;
        m_questManager.m_keyEnigmaDone = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // here ?
        if (collider.CompareTag("Player") && !m_questManager.m_keyEnigmaDone)
        {
            m_questSystem.ChangeQuest("OUVRE CE CADENAS");

        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && m_canUseItem)
        {
            if (UI_QuestObjects.m_activateKey)
            {
                m_meshRenderer.enabled = true;
                m_questManager.m_keyEnigmaDone = true;
                m_slotKey.SetActive(false);
                m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_LockpickSound);
            }

            //m_questSystem.ChangeQuest("Allumez le feuuuuuuuuuu");

            //m_slotKey.SetActive(true);

        }

    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player") && m_questManager.m_keyEnigmaDone)
        {
            m_canUseItem = false;
        }
    }

}
