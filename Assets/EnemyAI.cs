using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform m_player;

    private NavMeshAgent m_agent;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();        
    }

    void Update()
    {
        m_agent.SetDestination(m_player.transform.position);
    }
}