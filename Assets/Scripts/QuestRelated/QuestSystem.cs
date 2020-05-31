using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// display the current quest
/// </summary>
public class QuestSystem : MonoBehaviour
{
    public TextMeshProUGUI m_quests;

    [SerializeField] Animator m_animator;

    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    /// <summary>
    /// display the new quest
    /// </summary>
    /// <param name="p_description">the description is written in other script which use this function</param>
    public void ChangeQuest(string p_description)
    {
        m_quests.text = p_description;
        m_animator.SetTrigger("DisplayQuest");
        m_audioSource.PlayOneShot(m_audioManager.m_questSuccessSound);

    }
}
