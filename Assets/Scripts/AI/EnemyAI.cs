using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent m_agent;

    private DetectLight m_enemyDetect;

    [SerializeField] Animator m_animatorEnemy;

    [SerializeField] CapsuleCollider m_ColliderEnemy;

    [SerializeField]
    private Data m_data;

    public static string m_tagCollision;
    //[SerializeField]
    //public GameObject m_player;

    private void OnEnable()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_enemyDetect = GetComponent<DetectLight>();

    }

    private void Update()
    {

        //m_agent.SetDestination(m_player.transform.position);
        if (m_enemyDetect.m_inLight)
        {
            //Debug.Log(EnemyDetect.m_inLight);
            //m_agent.SetDestination(m_back.transform.position);
            //transform.position += Vector3.forward * Time.deltaTime * 10;
            m_agent.SetDestination(transform.forward * -10);
        }

        if (!m_enemyDetect.m_inLight)
        {
            m_agent.SetDestination(m_data.m_player.transform.position);

            //m_agent.SetDestination(m_player.transform.position);

        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Axe" || collision.gameObject.tag == "StoneAxe")
        {
            KillFunc();
        }

        m_tagCollision = collision.gameObject.tag.ToString();
    }

    private void KillFunc()
    {

        m_animatorEnemy.SetTrigger("Activate");
        Destroy(m_ColliderEnemy);
        Destroy(gameObject, 1);
        Debug.Log("tue l'ennemi");
    }
}