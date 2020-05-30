using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using GD2Lib;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent m_agent;

    private DetectLight m_enemyDetect;

    [SerializeField] Animator m_animatorEnemy;

    [SerializeField] CapsuleCollider m_ColliderEnemy;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField] CleaningPollution m_cleaningPollution;

    [SerializeField]
    private Data m_data;

    public IntVar m_lifeValue;

    public static string m_tagCollision;

    public static bool m_activeEnemyMusic;

    private Vector3 m_startingPos;

    //[SerializeField]
    //public GameObject m_player;

    private void OnEnable()
    {
        m_startingPos = transform.position;

        m_agent = GetComponent<NavMeshAgent>();
        m_enemyDetect = GetComponent<DetectLight>();

        m_audioManager.m_audioSource = GetComponent<AudioSource>();

        m_agent.enabled = false;
    }

    private void Update()
    {
        //m_agent.SetDestination(m_player.transform.position);

        float dist = Vector3.Distance(transform.position, m_data.m_player.transform.position);
        Vector3 direction = m_data.m_player.transform.position - transform.position;
        direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

        //float dist = Vector3.Distance(transform.position, m_player.transform.position);

        //detection distance 
        if (Mathf.Abs(dist) < 7f)
        {
            m_activeEnemyMusic = true;
            if (m_enemyDetect.m_inLight)
            {
                //m_agent.SetDestination(m_back.transform.position);
                transform.position += -direction * Time.deltaTime * 2;
                transform.LookAt(transform.position - direction );

                //transform.forward += new Vector3(0,0,-Time.deltaTime * 10);
                //m_agent.SetDestination(transform.forward * -10);
            }

            if (!m_enemyDetect.m_inLight)
            {
                //m_agent.SetDestination(m_data.m_player.transform.position);
                //m_agent.SetDestination(m_player.transform.position);

                float step = 1f * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, m_data.m_player.transform.position, step);


                transform.LookAt(transform.position + direction);

            }
        } else
        {
            m_activeEnemyMusic = false;
            //m_agent.ResetPath();
            if (m_lifeValue.Value == 0)
                transform.position = m_startingPos;

            float step = 0.5f * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, m_startingPos, step);

        }

        //if (m_agent.velocity.magnitude == 0f && m_agent.pathStatus == NavMeshPathStatus.PathPartial)
        //{
        //    m_agent.ResetPath();

        //    m_agent.enabled = false;
        //    transform.forward += Vector3.forward;
        //    m_agent.enabled = true;
        //    //Debug.Log(m_agent.velocity.magnitude);
        //    //if (m_agent.autoRepath)
        //    //    Debug.Log("INVALID");
        //}

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Axe" || collision.gameObject.tag == "StoneAxe")
        {
            if (InteractiveObject.m_cutThePlant)
            {
                KillFunc();

            }
        }

        m_tagCollision = collision.gameObject.tag.ToString();
    }

    private void KillFunc()
    {
        if (gameObject.name == "DungeonEnemy")
        {
            m_cleaningPollution.m_enemiesAround -= 1;
            Debug.Log(m_cleaningPollution.m_enemiesAround);

            if (m_cleaningPollution.m_enemiesAround == 0)
            {
                m_cleaningPollution.TriggerAnimPollution();
            }
        }
        else if (gameObject.name == "Dungeon2Enemy")
        {
            m_cleaningPollution.m_enemiesAround -= 1;
            Debug.Log(m_cleaningPollution.m_enemiesAround);

            if (m_cleaningPollution.m_enemiesAround == 0)
            {
                m_cleaningPollution.TriggerAnimPollution();

            }
        }

        m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_hitEnemySound);
        m_animatorEnemy.SetTrigger("Activate");
        Destroy(m_ColliderEnemy);
        Destroy(gameObject, 1);
    }
}