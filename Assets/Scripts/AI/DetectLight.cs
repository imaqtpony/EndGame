using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    public bool m_inLight = false;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LightCone")
        {
            //m_inLight = true;
            StartCoroutine(EnterLightCoroutine());
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "LightCone")
        {
            //m_inLight = false;
            StartCoroutine(LightCoroutine());
        }
    }

    IEnumerator LightCoroutine()
    {
        yield return new WaitForSeconds(2);

        m_inLight = false;
    }

    IEnumerator EnterLightCoroutine()
    {
        yield return new WaitForSeconds(0.2F);

        m_inLight = true;
    }
}
