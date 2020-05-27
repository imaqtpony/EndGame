using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintObjects : MonoBehaviour
{

    public static bool m_ladderBluePrintDiscovered;

    [SerializeField] Item.ItemType m_blueprintType;
    [SerializeField] QuestSystem m_questSystem;

    private void OnEnable()
    {
        m_ladderBluePrintDiscovered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_blueprintType == Item.ItemType.plan_echelle)
        {
            m_ladderBluePrintDiscovered = true;
            m_questSystem.ChangeQuest("Construisez une echelle.");
            Destroy(gameObject);
        }
    }
}
