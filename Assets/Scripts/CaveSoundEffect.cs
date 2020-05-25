using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundEffect : MonoBehaviour
{
    [SerializeField] AudioManager m_audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(m_audioManager.m_caveSound);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().Pause();
        }
    }
}
