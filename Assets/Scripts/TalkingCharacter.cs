using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkingCharacter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_text;

    [SerializeField] string m_textCharacter;

    private bool m_displayedText;

    private void Awake()
    {
        m_text.enabled = false;

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !m_displayedText)
        {
            StartCoroutine(DisplayText());
            m_displayedText = true;
        }
    }

    private IEnumerator DisplayText()
    {
        m_text.enabled = true;
        m_text.text = m_textCharacter;
        yield return new WaitForSeconds(4);
        m_text.enabled = false;
    }
}
