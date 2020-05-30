using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            yield return new WaitForSeconds(.1f);
            var actualPos = transform.position;
            yield return new WaitForSeconds(.3f);
            var finalPos = transform.position;
            yield return new WaitForSeconds(.1f);


            if (actualPos == finalPos)
            {
                m_audioSource.Pause();
            }
            else if (actualPos != finalPos)
            {
                bool playOnce = false;

                if (!playOnce)
                {
                    m_audioSource.Play();
                    playOnce = true;
                }


            }
        }

    }
}
