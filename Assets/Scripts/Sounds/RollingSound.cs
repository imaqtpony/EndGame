using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when the barrel is rolling, we play the sound 
/// </summary>
public class RollingSound : MonoBehaviour
{

    private AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        StartCoroutine(checkObjectPos());
    }


    private IEnumerator checkObjectPos()
    {
        while (true)
        {
            //we save the actual pos and the final pos
            yield return new WaitForSeconds(.1f);
            var actualPos = transform.position;
            yield return new WaitForSeconds(.3f);
            var finalPos = transform.position;
            yield return new WaitForSeconds(.1f);

            //and check if the object is moving
            if (actualPos == finalPos)
            {
                m_audioSource.Pause();
            }
            else if (actualPos != finalPos)
            {

                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.Play();
                }


            }
        }

    }
}
