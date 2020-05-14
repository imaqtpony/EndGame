using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ladder : MonoBehaviour
{

    [SerializeField] MeshRenderer m_meshLadder;
    [SerializeField] NavMeshObstacle m_navMeshObs;

    [SerializeField] GameObject m_ladderOnPlayer;

    Renderer m_renderer;

    [SerializeField] Material m_material;

    public static bool m_ladderUsing;
    public void Start()
    {
        m_meshLadder.enabled = false;
        m_navMeshObs.enabled = true;
        m_renderer = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered)
        {
            Debug.Log("PLAYER CONTACT");
            m_meshLadder.enabled = true;
            if (m_ladderOnPlayer.activeInHierarchy)
            {
                m_renderer.material = m_material;
                m_navMeshObs.enabled = false;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered)
        {
            Debug.Log("PLAYER CONTACT");
            m_meshLadder.enabled = false;

        }
    }
}
