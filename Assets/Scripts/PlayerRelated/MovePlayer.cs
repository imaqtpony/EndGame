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
    private float m_range = 0.5f;

    private int m_nbFramesElapsed;

    [SerializeField] private LayerMask m_blockRaycastMask;

    [SerializeField] private LayerMask m_ignoreRaycastMask;

    private void Awake()
    {
        m_data.m_player = gameObject;

        m_agent = GetComponent<NavMeshAgent>();

        m_agent.updateRotation = false;
        m_nbFramesElapsed = 0;

        //invert bitmask for the raycast to hit everything but layermask m_ignoreRaycastMask
        m_ignoreRaycastMask = ~m_ignoreRaycastMask;
    }


    private void Update()
    {

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
    private void MoveToTouch(Touch p_touch)
    {

        //if le tel marche
        //if (Input.touchCount > 0)
        //{
            // if le tel marche plus
            //if (Input.GetMouseButtonDown(0)) {

            if (!m_agent.hasPath)
            {
                //if le tel marche
                Ray castPoint = Camera.main.ScreenPointToRay(p_touch.position);

                ////if le tel marche plus
                //Vector3 mouse = Input.mousePosition;
                //Ray castPoint = Camera.main.ScreenPointToRay(mouse);

                if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, m_ignoreRaycastMask) && hit.transform.gameObject.layer == (m_blockRaycastMask & (1 << hit.transform.gameObject.layer)))
                {

                    m_agent.destination = ClosestNavmeshLocation(hit.point, m_range);

                    Vector3 direction = hit.point - transform.position;
                    direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

                    gameObject.transform.LookAt(transform.position + direction);


                }
            }

        //}
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

