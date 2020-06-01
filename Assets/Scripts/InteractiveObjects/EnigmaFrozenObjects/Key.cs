//Last Edited : 30/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Attach this to the Go Clef_du_cadenas, child of the Cadenas Go
/// </summary>
public class Key : MonoBehaviour
{

    public static bool m_canUseItem;
    public static bool m_gotKey;

    [SerializeField] private GameObject m_slotKey;

    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    [SerializeField] Animator m_animatorSlotKey;

    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    private Renderer m_meshRenderer;

    [SerializeField] GameObject m_goToEnable;
    [SerializeField] GameObject m_goToDisable;
    [SerializeField] GameObject m_blackScreen;

    [SerializeField] GameObject m_donjonNotif;
    [SerializeField] TextMeshProUGUI m_donjonNotifText;


    private void Start()
    {
        m_meshRenderer = GetComponent<Renderer>();
        m_canUseItem = false;
        m_questManager.m_keyEnigmaDone = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !m_questManager.m_keyEnigmaDone)
        {
            m_questSystem.ChangeQuest("Deverouille le cadenas");

        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && m_gotKey)
        {
            m_animatorSlotKey.SetTrigger("FlashingQuestObject");

            m_canUseItem = true;
            if (UI_QuestObjects.m_activateKey)
            {
                m_meshRenderer.enabled = true;
                m_questManager.m_keyEnigmaDone = true;
                m_slotKey.SetActive(false);
                m_audioSource.PlayOneShot(m_audioManager.m_LockpickSound);
                UI_QuestObjects.m_activateKey = false;
                StartCoroutine(Transition());
            }
        }
    }

    private IEnumerator Transition()
    {
        m_blackScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        m_goToEnable.SetActive(true);
        m_goToDisable.transform.position = new Vector3(-100, -100, -100);

        yield return new WaitForSeconds(1);

        m_blackScreen.SetActive(false);
        m_donjonNotif.SetActive(true);
        m_donjonNotifText.text = "Deuxieme Zone Depolluee";

        yield return new WaitForSeconds(4);
        m_donjonNotif.SetActive(false);
        m_goToDisable.SetActive(false);

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player") && m_questManager.m_keyEnigmaDone)
        {
            m_animatorSlotKey.SetTrigger("DisplayQuestObject");

        }
    }

}
