using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// animation played when the player success a dungeon
/// uts cleans the pollution 
/// </summary>
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
