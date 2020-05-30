using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningPollution : MonoBehaviour
{
    public int m_enemiesAround;
    public static bool m_zoneDepolluted;
    public void TriggerAnimPollution()
    {

        GetComponent<Animator>().SetTrigger("Cleaning");
        m_zoneDepolluted = true;
    }
}
