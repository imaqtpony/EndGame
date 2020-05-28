using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enable this script the first time the player picks a rock
/// </summary>
public class IsASmallRock : MonoBehaviour
{
    private GameObject m_theRealRock;

    public static GameObject m_theFalseRock;

    private void OnEnable()
    {
        m_theFalseRock = GameObject.FindGameObjectWithTag("caillou");
        m_theRealRock = m_theFalseRock;

        StartCoroutine(YouAreARock());
    }

    private IEnumerator YouAreARock()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if(m_theRealRock != null) {
                m_theFalseRock = m_theRealRock;

                Debug.Log($"The real rock = {m_theRealRock}");
            }
            
        }
    }

    public void AssignRealRock()
    {

    }
    
}
