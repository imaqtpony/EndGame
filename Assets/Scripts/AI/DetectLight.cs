//Last Edited : 30/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// Attach this to the enemies prefab
/// if they enter the light sphere emitted by the torch, they move in the opposite direction
/// </summary>
public class DetectLight : MonoBehaviour
{
    public bool m_inLight = false;

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LightCone")
        {
            StartCoroutine(EnterLightCoroutine());
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "LightCone")
        {
            StartCoroutine(LightCoroutine());
        }
    }

    private IEnumerator LightCoroutine()
    {
        yield return new WaitForSeconds(2);

        m_inLight = false;
    }

    private IEnumerator EnterLightCoroutine()
    {
        yield return new WaitForSeconds(0.2F);

        m_inLight = true;
    }


}
