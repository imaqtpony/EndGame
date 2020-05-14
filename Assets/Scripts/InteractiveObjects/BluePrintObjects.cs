using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintObjects : MonoBehaviour
{

    public static bool m_ladderBluePrintDiscovered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.name == "Blueprint_echelle")
        {
            m_ladderBluePrintDiscovered = true;
        }
    }
}
