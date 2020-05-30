//Last edited : 30/05

using System;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private Data m_data;

    [SerializeField]
    private GD2Lib.Event m_onLadderClimb;

    // Component NavMeshAgent
    [SerializeField]
    private NavMeshAgent m_agent;

    // range in which it detects a hit on the navmesh, from the touch on screen
    private float m_range = 0.5f;

    //nb frames elapsed to detect if its a swipe or a touch
    private int m_nbFramesElapsed;

    public static bool m_stopSwipe;

    [SerializeField] private LayerMask m_blockRaycastMask;

    [SerializeField] private LayerMask m_ignoreRaycastMask;

    [SerializeField] private AudioManager m_audioManager;
    [SerializeField] private AudioSource m_audioSourceStepSound;
    [SerializeField] private AudioSource m_audioSourceEnemyMusic;

    [SerializeField] Animator m_animatorPlayer;

    [SerializeField] private GameObject m_crossHit;

    private void Awake()
    {
        m_data.m_player = gameObject;

        m_audioSourceStepSound.clip = m_audioManager.m_grassStepSound;

        m_audioSourceEnemyMusic.clip = m_audioManager.m_musicEnemy;

        m_agent.updateRotation = false;
        m_nbFramesElapsed = 0;

        //invert bitmask for the raycast to hit everything but layermask m_ignoreRaycastMask
        m_ignoreRaycastMask = ~m_ignoreRaycastMask;
    }


    private void Update()
    {

        // the pathcomplete bug fix where the agent stops but his path is not completed, we reset the path if the agent enters a small square radius around the destination
        if ((transform.position.x < m_agent.destination.x + 0.2f && transform.position.x > m_agent.destination.x - 0.2f) && (transform.position.z < m_agent.destination.z + 0.2f && transform.position.z > m_agent.destination.z - 0.2f))
            m_agent.ResetPath();

        if (EnemyAI.m_activeEnemyMusic)
        {
            if (!m_audioSourceEnemyMusic.isPlaying)
            {
                m_audioSourceEnemyMusic.Play();
                m_audioSourceEnemyMusic.volume = 0f;
            }

            m_audioSourceEnemyMusic.volume += 0.01f;
            if (m_audioSourceEnemyMusic.volume >= .5)
            {
                m_audioSourceEnemyMusic.volume = .5f;
            }

            Debug.Log("MUSIC");
        }
        else if (!EnemyAI.m_activeEnemyMusic && m_audioSourceEnemyMusic.isPlaying)
        {
            m_audioSourceEnemyMusic.volume -= 0.01f;
            if (m_audioSourceEnemyMusic.volume <= 0)
            {
                m_audioSourceEnemyMusic.volume = 0f;
                m_audioSourceEnemyMusic.Pause();

            }
            Debug.Log("PAS DE MUSIC");

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_nbFramesElapsed = 0;
            }

            m_nbFramesElapsed++;

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (m_nbFramesElapsed > 20)
                {
                    m_stopSwipe = true;
                }
                else
                {
                    MoveToTouch(touch);
                }
            }
        }

    }

    /// <summary>
    /// Move the player to the touch location on the screen
    /// </summary>
    /// <param name="p_touch"> the touch on the screen </param>
    private void MoveToTouch(Touch p_touch)
    {
        //if le tel marche
        Ray castPoint = Camera.main.ScreenPointToRay(p_touch.position);

        if (Physics.Raycast(castPoint, out RaycastHit hit, Mathf.Infinity, m_ignoreRaycastMask) && hit.transform.gameObject.layer == (m_blockRaycastMask & (1 << hit.transform.gameObject.layer)) && !InteractWithObjects.m_gotInteracted)
        {
            Vector3 navMeshLoc = ClosestNavmeshLocation(hit.point, m_range);
            if (navMeshLoc != Vector3.zero) { m_agent.destination = navMeshLoc; }
                        
            Vector3 direction = hit.point - transform.position;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
            gameObject.transform.LookAt(transform.position + direction);
                
            if(m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Idle")) m_animatorPlayer.SetTrigger("Course");
            else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils")) m_animatorPlayer.SetTrigger("CourseOutils");


            m_audioSourceStepSound.Play();
            StartCoroutine(checkPlayerPos());
            m_crossHit.transform.position = new Vector3(hit.point.x, hit.point.y + .1f, hit.point.z);

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

    /// <summary>
    /// Animations and sound coroutine
    /// </summary>
    private IEnumerator checkPlayerPos()
    {

        var actualPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        var finalPos = transform.position;

        if (actualPos == finalPos)
        {
            m_audioSourceStepSound.Pause();

            if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Course")) m_animatorPlayer.SetTrigger("Idle");
            else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils")) m_animatorPlayer.SetTrigger("IdleOutils");

            StopCoroutine(checkPlayerPos());

        }
        else if (actualPos != finalPos)
        {
            StartCoroutine(checkPlayerPos());
        }

    }

    #region ONLADDERCLIMB
    private void OnEnable()
    {
        if (m_onLadderClimb != null)
            m_onLadderClimb.Register(HandleLadderClimb);
    }
    
    private void OnDisable()
    {
        if (m_onLadderClimb != null)
            m_onLadderClimb.Unregister(HandleLadderClimb);
    }
    
    /// <summary>
    /// Teleports the player to the given position whenever the player climbs a ladder
    /// </summary>
    private void HandleLadderClimb(GD2Lib.Event p_event, object[] p_params)
    {
        if (GD2Lib.Event.TryParseArgs(out Vector3 p_pos, p_params))
        {
            m_agent.enabled = false;
            transform.position = ClosestNavmeshLocation(p_pos, m_range*4);
            m_agent.enabled = true;

        }
        else
        {
            Debug.LogError("Invalid type of argument !");
        }
    }
    #endregion

}

