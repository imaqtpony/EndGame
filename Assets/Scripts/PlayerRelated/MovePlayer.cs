using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.EventSystems;

// estce que on clic et on attend la fi n du path ?
// camera ensuite
public class MovePlayer : MonoBehaviour
{
    // Component NavMeshAgent
    private NavMeshAgent m_agent;

    // range in which it detects a hit on the navmesh, from the touch on screen
    private float m_range = 10f;



    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        Debug.Log(m_agent.hasPath);

        //si quand j'envoie un raycast il n'y a pas d'objet devant, alors ca marche
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            // remove this to revert to follow the finger type of movement
            if (!m_agent.hasPath)
            {
                Touch touch = Input.GetTouch(0);
                RaycastHit hit;

                // if le tel marche plus
                //Vector3 mouse = Input.mousePosition;
                //Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                Ray castPoint = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    m_agent.destination = ClosestNavmeshLocation(hit.point, m_range);

                }
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
