using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public TextMeshProUGUI m_quests;

    [SerializeField] Animator m_animator;

    [SerializeField] AudioManager m_audioManager;

    private void Start()
    {
        m_audioManager.m_audioSource = GetComponent<AudioSource>();
    }

    public void ChangeQuest(string p_description)
    {
        m_quests.text = p_description;
        m_animator.SetTrigger("DisplayQuest");
        m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_questSuccessSound);

    }
}
