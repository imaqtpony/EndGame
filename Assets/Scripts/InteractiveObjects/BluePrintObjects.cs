using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the blueprints of the object(s) in the game
/// </summary>
public class BluePrintObjects : MonoBehaviour
{

    public static bool m_ladderBluePrintDiscovered;

    [Header("BLUEPRINT TYPE")]
    [SerializeField] Item.ItemType m_blueprintType;

    [Header("USES QUESTSYSTEM")]
    [SerializeField] QuestSystem m_questSystem;

    /// <summary>
    /// reset the boolean value at each game
    /// </summary>
    private void OnEnable()
    {
        m_ladderBluePrintDiscovered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player collide the game object (the chest in the frist dungeon)
        //he will get the ladder blueprint and be able to craft the ladder
        if (other.CompareTag("Player") && m_blueprintType == Item.ItemType.plan_echelle)
        {
            m_ladderBluePrintDiscovered = true;
            m_questSystem.ChangeQuest("Construisez une echelle.");
            Destroy(gameObject);
        }
    }
}
