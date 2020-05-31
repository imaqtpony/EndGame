//Last Edited : 30/05

using UnityEngine;
using GD2Lib;
using System;
using UnityEngine.AI;

/// <summary>
/// Use this script attached to the player GO to spawn it every time it changes from a board to another.
/// </summary>
public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Own component")]
    private NavMeshAgent m_agent;


    /// <summary>
    /// Place player at a temporary position on the board he wishes to go
    /// move 6.5 units to left/top/bot/right
    /// This function is called in BoardManager.cs
    /// </summary>
    /// <param name="p_borderName"> which border the player is coming from the last board ?</param>
    public void PlacePlayer(string p_borderName)
    {
        m_agent.ResetPath();
        // enable disable otherwise you can't move a navmesh agent from a navmesh to another while active
        m_agent.enabled = false;

        float valueToMovePlayer = 6.5f;

        switch (p_borderName)
        {

            case "LeftBorder":

                transform.position = new Vector3(transform.position.x - valueToMovePlayer, transform.position.y, transform.position.z);

                break;
            case "RightBorder":

                transform.position = new Vector3(transform.position.x + valueToMovePlayer, transform.position.y, transform.position.z);

                break;
            case "BottomBorder":

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - valueToMovePlayer);

                break;
            case "TopBorder":

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + valueToMovePlayer);

                break;

            default:
                break;

        }

        m_agent.enabled = true;

    }
}
