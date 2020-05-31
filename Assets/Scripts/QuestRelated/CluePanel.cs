using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// when we approch to clue pannel, we can click on it
/// </summary>
public class CluePanel : MonoBehaviour
{
    [Header("VISUAL RELATED")]
    [SerializeField] Image m_cluePhoto;
    [SerializeField] Sprite m_clueSprite;
    [SerializeField] TextMeshProUGUI m_textClueUI;

    [SerializeField] string m_text;

    private void Start()
    {
        m_cluePhoto.enabled = false;
        m_textClueUI.enabled = false;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //the sprite and all are activated but not the parent
            //the parent is actuivated in the script InteractWithObject (m_goToEnable)
            m_cluePhoto.enabled = true;
            m_textClueUI.enabled = true;
            m_cluePhoto.sprite = m_clueSprite;
            m_textClueUI.text = m_text;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_cluePhoto.enabled = false;
            m_textClueUI.enabled = false;

        }
    }
}
