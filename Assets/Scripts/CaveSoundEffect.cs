using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundEffect : MonoBehaviour
{
    [SerializeField] AudioManager m_audioManager;

    [SerializeField] GameObject m_dynamicLight;

    [SerializeField] QuestSystem m_questSystem;

    bool m_CandleQuestDisplayed;

    private void Start()
    {
        Camera.main.GetComponent<AudioSource>().clip = m_audioManager.m_caveSound;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Camera.main.GetComponent<AudioSource>().Play();

            m_dynamicLight.SetActive(true);

            if(m_questSystem != null && !m_CandleQuestDisplayed)
            {
                m_questSystem.ChangeQuest("Allumez les bougies.");
                m_CandleQuestDisplayed = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().Pause();
            m_dynamicLight.SetActive(false);

            Camera.main.GetComponent<AudioSource>().Pause();

        }
    }
}
