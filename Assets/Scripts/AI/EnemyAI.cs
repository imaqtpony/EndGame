using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform m_player;

    [SerializeField] Transform m_back;

    private NavMeshAgent m_agent;

    private DetectLight EnemyDetect;

    void Start()
    {
        EnemyDetect = GetComponent<DetectLight>();
    }
    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();        
    }

    void Update()
    {
        //m_agent.SetDestination(m_player.transform.position);

        if (EnemyDetect.m_inLight == true)
        {
            //Debug.Log(EnemyDetect.m_inLight);
            //m_agent.SetDestination(m_back.transform.position);
            transform.position += Vector3.forward * Time.deltaTime * 10;
        }

        if (EnemyDetect.m_inLight == false)
        {
            m_agent.SetDestination(m_player.transform.position);
        }
    }
}