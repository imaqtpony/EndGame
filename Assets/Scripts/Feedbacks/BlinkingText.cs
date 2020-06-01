// Last edit : 30/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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

    [SerializeField] TextMeshProUGUI m_tipText;

    bool m_canLaunchGame;
    private void Start()
    {
        //the attached text component
        m_tmp = GetComponent<TextMeshProUGUI>();
        m_tmp.alpha = 0;
        
        StartCoroutine(FadeText());

        int randNumb = UnityEngine.Random.Range(0, 3);

        switch (randNumb)
        {
            default:
                break;
            case 0:
                m_tipText.text = "Astuce: Les ennemis sont sensibles au sources lumineuses et aux coups !";
                break;
            case 1:
                m_tipText.text = "Astuce: Faites attention aux objets que vous jetez au sol.";
                break;
            case 2:
                m_tipText.text = "Astuce: Couper les plantes est meilleur que de les bruler !";
                break;
        }

    }

    private IEnumerator FadeText()
    {
        yield return new WaitForSeconds(5f);
        m_canLaunchGame = true;
        while (Input.touchCount == 0)
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

    private void Update()
    {
        if (Input.touchCount > 0 && m_canLaunchGame)
        {
            SceneManager.LoadScene(2);
        }
    }

}
