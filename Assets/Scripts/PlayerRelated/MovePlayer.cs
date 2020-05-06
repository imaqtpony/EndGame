using System;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Build;
using UnityEngine.EventSystems;



public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private Data m_data;

    // Component NavMeshAgent
    private NavMeshAgent m_agent;

    // range in which it detects a hit on the navmesh, from the touch on screen
    private float m_range = 10f;

    private int m_nbFramesElapsed;

    private void Awake()
    { 
        m_agent = GetComponent<NavMeshAgent>();

        m_agent.updateRotation = false;
        m_nbFramesElapsed = 0;
    }


    private void Update()
    {
        // Ended = movetotouch
        // looks like deltaTime is the sensibility 0.0333

        //If ended
        //If touch long
        // rotate, its a swipe or whatever
        //else
        // movetotouch

        //Debug.Log(m_agent.hasPath);
        //Debug.Log(touch.deltaTime);
        //Debug.Log(m_data.m_isSwiping);
        //Debug.Log(touch.phase);

        // the pathcomplete bug fix where the agent stops but his path is not completed, we reset the path if the agent enters a small square radius around the destination
        if ((transform.position.x < m_agent.destination.x + 0.2f && transform.position.x > m_agent.destination.x - 0.2f) && (transform.position.z < m_agent.destination.z + 0.2f && transform.position.z > m_agent.destination.z - 0.2f))
            m_agent.ResetPath();



        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_nbFramesElapsed = 0;
            }

            m_nbFramesElapsed++;

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (m_nbFramesElapsed > 20)
                {
                    Debug.Log("SWIPE OR W/E");
                }
                else
                {
                    MoveToTouch(touch);
                }
            }
        }
            
    }


    /// <summary>
    /// Move the player to the touch location on the screen
    /// </summary>
    /// <param name="p_touch"> the touch on the screen </param>
    public void MoveToTouch(Touch p_touch)
    {


        //if le tel marche
        if (Input.touchCount > 0)
        {
            // if le tel marche plus
            //if (Input.GetMouseButtonDown(0)) {

            if (!m_agent.hasPath)
            {
                //if le tel marche
                Ray castPoint = Camera.main.ScreenPointToRay(p_touch.position);
                RaycastHit hit;

                ////if le tel marche plus
                //Vector3 mouse = Input.mousePosition;
                //Ray castPoint = Camera.main.ScreenPointToRay(mouse);


                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    m_agent.destination = ClosestNavmeshLocation(hit.point, m_range);
                    //transform.rotation = Quaternion.LookRotation(m_agent.destination - hit.point);
                    transform.LookAt(hit.point);

                }
            }

        }
    }


    /// <summary>
    /// Generate the closest hit on navmesh from the player touch on screen when moving
    /// </summary>
    private Vector3 ClosestNavmeshLocation(Vector3 p_v, float p_range)
    {
        
        NavMeshHit hit;

        Vector3 closestPosition = Vector3.zero;

        // Get the closest navmeshhit
        if (NavMesh.SamplePosition(p_v, out hit, p_range, 1))
        {
            closestPosition = hit.position;
        }


        return closestPosition;

    }

}

