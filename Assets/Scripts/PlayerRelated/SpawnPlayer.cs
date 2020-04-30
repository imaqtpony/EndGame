
using UnityEngine;
using GD2Lib;
using System;
using UnityEngine.AI;

/// <summary>
/// Use this scirpt attached to the player GO to spawn it every time it changes from a board to another.
/// </summary>
public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    public GD2Lib.Event m_boardChangeEvent;

    private NavMeshAgent m_agent;


    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    //private void OnEnable()
    //{
    //    if (m_boardChangeEvent!=null)
    //        m_boardChangeEvent.Register(HandlePlacePlayer);
    //}
    //private void OnDisable()
    //{
    //    if (m_boardChangeEvent!=null)
    //        m_boardChangeEvent.Register(HandlePlacePlayer);
    //}

    //private void HandlePlacePlayer(GD2Lib.Event p_event, object[] p_params)
    //{
    //    if (GD2Lib.Event.TryParseArgs(out string borderName, out Vector3 borderPos, p_params))
    //    {
    //        // do stuff here
    //        Debug.Log("Works");

    //    }
    //    else
    //    {
    //        Debug.LogError("Invalid type of argument !");
    //    }

    //}

    public void PlacePlayer(string p_borderName)
    {
        m_agent.ResetPath();
        
        m_agent.enabled = false;

        switch (p_borderName)
        {

            case "LeftBorder":

                transform.position = new Vector3(transform.position.x - 6, transform.position.y, transform.position.z);
                //transform.position = new Vector3(-6,0,0);

                break;
            case "RightBorder":

                transform.position = new Vector3(transform.position.x + 6, transform.position.y, transform.position.z);

                break;
            case "BottomBorder":

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 6);

                break;
            case "TopBorder":

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 6);

                break;

            default:
                break;

        }

        m_agent.enabled = true;


    }
}
