using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent m_agent;

    private DetectLight EnemyDetect;

    [SerializeField]
    private Data m_data;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        EnemyDetect = GetComponent<DetectLight>();
    }

    private void Update()
    {
        //m_agent.SetDestination(m_player.transform.position);
        if (EnemyDetect.m_inLight == true)
        {
            //Debug.Log(EnemyDetect.m_inLight);
            //m_agent.SetDestination(m_back.transform.position);
            //transform.position += Vector3.forward * Time.deltaTime * 10;
            m_agent.SetDestination(transform.forward * -10);
        }

        if (EnemyDetect.m_inLight == false)
        {
            m_agent.SetDestination(m_data.m_player.transform.position);
        }
        

    }
}