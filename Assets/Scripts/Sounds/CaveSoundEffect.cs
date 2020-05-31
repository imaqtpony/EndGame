using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// little script to activate the sound of the cave when the player is in the first dungeon
/// </summary>
public class CaveSoundEffect : MonoBehaviour
{
    /// <summary>
    /// light behind the camera to not have a level too much dark
    /// </summary>
    [SerializeField] GameObject m_dynamicLight;

    [SerializeField] QuestSystem m_questSystem;

    [SerializeField] AudioSource m_audioSource;
 
    bool m_CandleQuestDisplayed;

    /// <summary>
    /// we activate the light, we play the sound and we change the quest if the player is a the good place
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(m_audioSource != null)
            {
                m_audioSource.Play();

            }

            if (m_questSystem != null && !m_CandleQuestDisplayed)
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
            DeActivateLight();

        }
    }

    /// <summary>
    /// used when the player respawn
    /// </summary>
    public void DeActivateLight()
    {

        m_dynamicLight.SetActive(false);

        if (m_audioSource != null)
        {
            m_audioSource.Pause();
        }
    }
}
