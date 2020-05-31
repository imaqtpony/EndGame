// Last edit : 30/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Blinks the text at 0.6f max alpha
/// Attach to the textmeshpro TapToContinue
/// </summary>
public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI m_tmp;

    private bool m_invert = true;

    private float m_modifier = -1f;

    private float m_fadingTime = 3f;


    private void Start()
    {
        //the attached text component
        m_tmp = GetComponent<TextMeshProUGUI>();
        m_tmp.alpha = 0;
        
        StartCoroutine(FadeText());

    }


    private IEnumerator FadeText()
    {
        while(Input.touchCount == 0)
        {
            yield return new WaitForSeconds(0.4f);

            if (m_invert)
            {
                m_modifier = -m_modifier;
            }


            for (float i = 0f; i < 0.6f; i += Time.deltaTime / m_fadingTime)
            {
                m_tmp.alpha = i * m_modifier;
            }

            m_invert = !m_invert;

        }

        yield return null;

    }

}
