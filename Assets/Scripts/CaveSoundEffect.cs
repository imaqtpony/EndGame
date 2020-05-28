using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundEffect : MonoBehaviour
{
    [SerializeField] AudioManager m_audioManager;

    [SerializeField] Light m_directionalLight;
    [SerializeField] Light m_dinamicLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(m_audioManager.m_caveSound);
            if(m_directionalLight != null)
            {
                m_directionalLight.enabled = false;

            }
            m_dinamicLight.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().Pause();
            if (m_directionalLight != null)
            {
                m_directionalLight.enabled = true;

            }
            m_dinamicLight.enabled = false;

        }
    }
}
