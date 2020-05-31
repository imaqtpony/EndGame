//Last edited : 30/05

using UnityEngine.AI;
using UnityEngine;
using GD2Lib;

/// <summary>
/// Enemy behavior, attach this to the enemies prefab
/// </summary>
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

        float dist = Vector3.Distance(transform.position, m_data.m_player.transform.position);
        Vector3 direction = m_data.m_player.transform.position - transform.position;
        direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;


        //detection distance 
        if (Mathf.Abs(dist) < 7f)
        {
            m_activeEnemyMusic = true;

            //if the enemy enters the light radius of the torch
            if (m_enemyDetect.m_inLight)
            {
                transform.position += -direction * Time.deltaTime * 2;
                transform.LookAt(transform.position - direction );

            }

            //if not then move towards player
            if (!m_enemyDetect.m_inLight)
            {

                float step = 1f * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, m_data.m_player.transform.position, step);

                transform.LookAt(transform.position + direction);

            }
        } else
        {
            m_activeEnemyMusic = false;

            if (m_lifeValue.Value == 0)
                transform.position = m_startingPos;

            // Goes back to his original pos when the player is away
            float step = 0.5f * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, m_startingPos, step);

        }


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

    /// <summary>
    /// Kills this gameObject
    /// </summary>
    private void KillFunc()
    {
        if (gameObject.name == "DungeonEnemy")
        {
            m_cleaningPollution.m_enemiesAround -= 1;

            if (m_cleaningPollution.m_enemiesAround == 0)
            {
                m_cleaningPollution.TriggerAnimPollution();
            }
        }
        else if (gameObject.name == "Dungeon2Enemy")
        {
            m_cleaningPollution.m_enemiesAround -= 1;

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