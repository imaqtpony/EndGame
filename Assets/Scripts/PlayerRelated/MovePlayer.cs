using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


// estce que on clic et on attend la fi n du path ?
// camera ensuite
public class MovePlayer : MonoBehaviour
{
    // Component NavMeshAgent
    private NavMeshAgent m_agent;

    // range in which it detects a hit on the navmesh, from the touch on screen
    private float m_range = 10f;

    private Rigidbody m_rb;



    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        Debug.Log(m_agent.hasPath);

        //string pathStat = m_agent.pathStatus.ToString();
        //Debug.Log("z axis " + transform.forward.z);
        //Debug.Log("x axis " + transform.forward.x);
        //if (pathStat == "PathComplete" && input get touch 0 ?
        //{
        //    AgentPathUpdate();
        //}

        //nah
        //if (m_agent.hasPath && m_rb.velocity.magnitude < 0.05f)
        //{
        //    m_agent.isStopped = true;
        //    m_agent.ResetPath();
        //}

        // the pathcomplete bug fix where the agent stops but his path is not completed
        if ((transform.position.x < m_agent.destination.x + 0.2f && transform.position.x > m_agent.destination.x - 0.2f) && (transform.position.z < m_agent.destination.z + 0.2f && transform.position.z > m_agent.destination.z - 0.2f))
            m_agent.ResetPath();

        //if le tel marche
        //if (Input.touchCount > 0 )
        //{
        if (Input.GetMouseButtonDown(0)) {
            // remove this to revert to follow the finger type of movement

            if (!m_agent.hasPath)
            {
                //if le tel marche
                //Touch touch = Input.GetTouch(0);
                //Ray castPoint = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                //if le tel marche plus
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);


                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    m_agent.destination = ClosestNavmeshLocation(hit.point, m_range);

                }
                Debug.Log(m_agent.destination);
            }
            
        }

    }


    /// <summary>
    /// Generate the closest hit on navmesh from the player touch on screen when moving
    /// possible adds : Set this + delegate if effect or feedback when changing var ?  befare of range tho
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
