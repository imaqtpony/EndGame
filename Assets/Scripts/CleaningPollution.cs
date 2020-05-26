using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningPollution : MonoBehaviour
{
    public int m_enemiesAround;

    public void TriggerAnimPollution()
    {

        GetComponent<Animator>().SetTrigger("Cleaning");

    }
}
