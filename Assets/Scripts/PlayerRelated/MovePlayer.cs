using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;


public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private Data m_data;

    // Component NavMeshAgent
    private NavMeshAgent m_agent;

    // range in which it detects a hit on the navmesh, from the touch on screen
    private float m_range = 10f;


    [Tooltip ("This object's rigidbody")]
    private Rigidbody m_rb;



    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();

        m_agent.updateRotation = false;
    }


    private void Update()
    {
        //Debug.Log(m_agent.hasPath);
        Touch touch = Input.GetTouch(0);

        //Debug.Log(touch.phase);

        // the pathcomplete bug fix where the agent stops but his path is not completed, we reset the path if the agent enters a small square radius around the destination
        if ((transform.position.x < m_agent.destination.x + 0.2f && transform.position.x > m_agent.destination.x - 0.2f) && (transform.position.z < m_agent.destination.z + 0.2f && transform.position.z > m_agent.destination.z - 0.2f))
            m_agent.ResetPath();


        if ((touch.phase == TouchPhase.Stationary) && (touch.deltaPosition.magnitude < Screen.dpi / 400))
            //Debug.Log("hjagzel");


        if (!m_data.m_isSwiping && touch.phase == TouchPhase.Stationary)
                MoveToTouch(touch);

        Debug.Log(m_data.m_isSwiping);
        //switch (touch.phase)
        //{
        //    case TouchPhase.Stationary:
        //        //fonction mouv
        //        if (touch.phase != TouchPhase.Moved)
        //            MoveToTouch(touch);
        //        break;

        //    case TouchPhase.Moved:
        //            // nah
        //        //transform.LookAt(new Vector3(touch.position.x, 0, touch.position.y));
        //        break;

        //    default:
        //        break;

        //}

        
    }

    /// <summary>
    /// Move the player to the touch location on the screen
    /// </summary>
    /// <param name="p_touch"> the touch on the screen </param>
    private void MoveToTouch(Touch p_touch)
    {
        transform.rotation = Quaternion.LookRotation(m_agent.velocity.normalized);

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

