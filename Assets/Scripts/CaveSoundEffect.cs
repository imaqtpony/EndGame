using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundEffect : MonoBehaviour
{

    [SerializeField] GameObject m_dynamicLight;

    [SerializeField] QuestSystem m_questSystem;

    [SerializeField] AudioSource m_audioSource;
 
    bool m_CandleQuestDisplayed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            m_audioSource.Play();

            if(m_questSystem != null && !m_CandleQuestDisplayed)
            {
                m_questSystem.ChangeQuest("Allumez les bougies.");
                m_CandleQuestDisplayed = true;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_dynamicLight.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().Pause();
            m_dynamicLight.SetActive(false);
            m_audioSource.Pause();

        }
    }
}
