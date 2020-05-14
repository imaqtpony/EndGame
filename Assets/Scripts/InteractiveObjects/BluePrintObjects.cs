using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintObjects : MonoBehaviour
{

    public static bool m_ladderBluePrintDiscovered;

    [SerializeField] Item.ItemType m_blueprintType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_blueprintType == Item.ItemType.plan_echelle)
        {
            m_ladderBluePrintDiscovered = true;
            Destroy(gameObject);
        }
    }
}
