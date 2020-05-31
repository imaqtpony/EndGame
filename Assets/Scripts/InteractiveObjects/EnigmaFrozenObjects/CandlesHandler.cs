//Last Edited : 30/05

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GD2Lib;
using System.Runtime.InteropServices.ComTypes;
using TMPro;

/// <summary>
/// Attach this to the parent GO of the candles Enigma GO
/// Handles the event onLightenCandles and solves the puzzle
/// </summary>
public class CandlesHandler : MonoBehaviour
{
    [SerializeField]
    private GD2Lib.Event m_onLightenCandles;

    private int[] m_orderArray;
    private ParticleSystem[] m_pSysArray;

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [SerializeField] ParticleSystem m_fxFire;

    [SerializeField] [Tooltip("The volet gameobject animator")] Animator[] m_animatorVolet;

    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;

    [SerializeField] [Tooltip("The volet gameobject navmeshObstacle")]  NavMeshObstacle[] m_navMeshObsVolets;

    [Tooltip("nb candles to solve the puzzle (add 1 to the actual number ) to fit how the checking is done")]
    private int m_nbCandles = 5;

    private int m_currentArrayIndex = 0;


    private void OnEnable()
    {

        //initialize arrays
        m_pSysArray = new ParticleSystem[m_nbCandles];
        m_orderArray = new int[m_nbCandles];
        for (int i = 0; i < m_nbCandles; i++)
            m_orderArray[i] = 0;

        if (m_onLightenCandles != null)
            m_onLightenCandles.Register(HandleCandleOrder);


    }

    private void OnDisable()
    {
        if (m_onLightenCandles != null)
            m_onLightenCandles.Unregister(HandleCandleOrder);
    }

    private void HandleCandleOrder(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out int candleOrder, out ParticleSystem candlePSys, p_params))
        {
            Debug.Log($"Candle {candleOrder}");
            m_currentArrayIndex++;

            // sign up which ever candle has been lightened on the current array Index
            m_orderArray[m_currentArrayIndex] = candleOrder;
            m_pSysArray[m_currentArrayIndex] = candlePSys;

            // if the last Index has been filled with a candle order int
            if (m_orderArray[m_nbCandles-1] != 0)
            {
                // if true then gj
                if (SolvePuzzle())
                {
                    foreach (Animator animator in m_animatorVolet)
                    {
                        animator.SetTrigger("Activate");

                        foreach (NavMeshObstacle navMeshObs in m_navMeshObsVolets)
                        {
                            navMeshObs.enabled = false;
                        }
                    }
                    m_fxFire.Play();
                    StartCoroutine(TutoEnemies());

                } else
                {
                    // deactivate candles, wrong order
                    Debug.Log("WRONG ORDER");

                    // Set a new color as a negative feedback
                    Gradient grad = new Gradient();
                    grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.black, 1.0f) }, 
                        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                    var col = m_pSysArray[m_nbCandles - 1].colorOverLifetime;
                    col.color = grad;

                    // stop after 2 seconds
                    StartCoroutine(WaitForSec(2));


                }
            }

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }

    /// <summary>
    /// Pop down this coroutine after the Enigma is done
    /// </summary>
    /// <returns></returns>
    private IEnumerator TutoEnemies()
    {
        yield return new WaitForSeconds(2f);

        m_notification.SetActive(true);
        m_textNotification.text = "Les ennemis sont sensibles a la lumiere, et aux coups.";
        m_audioSource.PlayOneShot(m_audioManager.m_fallingShuttersSound);

    }

    /// <summary>
    /// Resets the candles info on this script, the player can try again
    /// Call this script also if the puzzle is solved ?
    /// </summary>
    private void ResetCandles()
    {

        for (int i = 1; i < m_nbCandles; i++)
        {
            m_pSysArray[i].Stop();
            m_pSysArray[i] = null;
            m_orderArray[i] = 0;
        }

        m_currentArrayIndex = 0;
    }

    private IEnumerator WaitForSec(int s)
    {
        // Give back the old color to the last candle 
        var oldCol = m_pSysArray[m_nbCandles - 2].colorOverLifetime;
        var col = m_pSysArray[m_nbCandles - 1].colorOverLifetime;

        yield return new WaitForSeconds(s);

        ResetCandles();

        // wait for the duration of the animation before changing its color back to normal
        yield return new WaitForSeconds(s+2);

        //Reassign the original color to the last candle
        col.color = oldCol.color;

    }

    private bool SolvePuzzle()
    {
        bool solved = false;

        int rightOrder = 0;

        for(int i = 0; i < m_nbCandles; i++)
        {
            if (m_orderArray[i] == i)
            {
                rightOrder++;
            }
        }

        if (rightOrder == m_nbCandles)
            solved = true;

        return solved;
    }

}
