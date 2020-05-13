using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public TextMeshProUGUI m_quests;

    [SerializeField] Animator m_animator;

    public void ChangeQuest(string p_description)
    {
        m_quests.text = p_description;
        m_animator.SetTrigger("DisplayQuest");
    }
}
