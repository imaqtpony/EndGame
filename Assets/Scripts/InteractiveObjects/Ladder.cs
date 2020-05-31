//Last Edited : 30/05

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

/// <summary>
/// Attach this to a Ladder Gameobject
/// Make sure its disabled
/// </summary>
public class Ladder : MonoBehaviour
{
    [Header("LADDER VISUAL RELATED")]
    [SerializeField] Material m_material;
    [SerializeField] MeshRenderer m_meshLadder;
    [SerializeField] NavMeshObstacle m_navMeshObs;
    [SerializeField] GameObject m_ladderOnPlayer;

    Renderer m_renderer;

    //ladder 2 visual related
    private Transform m_ladder2;
    private MeshRenderer m_meshLadder2;
    private NavMeshObstacle m_navMeshObs2;

    [SerializeField] UI_Inventory m_uiInventory;

    public static bool m_ladderUsing;
    private bool m_ladderPlaced;
    private bool m_alreadyUsedLadder;

    [Header("NOTIFICATION RELATED")]
    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_notificationText;

    [Header("QUEST RELATED")]
    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;

    //sound related
    [SerializeField] AudioManager m_audioManager;
    private AudioSource m_audioSource;

    [SerializeField]
    private GD2Lib.Event m_onLadderClimb;

    public void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        m_meshLadder.enabled = false;
        m_navMeshObs.enabled = true;
        m_alreadyUsedLadder = false;

        //if the object is a double ladder (like LadderOnSameBoard GO)
        // initialize it that way (assign in the inspector the properties of the 1st ladder
        // the second one is initialized here
        if (transform.childCount > 0)
        {
            m_ladder2 = transform.GetChild(1);
            m_meshLadder2 = m_ladder2.gameObject.GetComponent<MeshRenderer>();
            m_navMeshObs2 = m_ladder2.gameObject.GetComponent<NavMeshObstacle>();
            m_meshLadder2.enabled = false;
            m_renderer = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

        } else
        {
            // get the renderer if its a lone ladder
            m_renderer = GetComponent<Renderer>();
        }
    }

    private void OnTriggerStay(Collider p_other)
    {
        //if the player has discovered the ladder blueprint they will appear on his way
        if (p_other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {

            if (!m_questManager.m_tutoLadderDone && m_notification != null)
            {
                m_notification.SetActive(true);
                m_notificationText.text = "Utilisez l'echelle dans votre inventaire en appuyant dessus.";
                m_questManager.m_tutoLadderDone = true;

            }

            //Show ladder
            m_meshLadder.enabled = true;

            //use ladder tool
            if (m_ladderOnPlayer.activeInHierarchy)
            {
                m_audioSource.PlayOneShot(m_audioManager.m_dropLadderSound);

                //Show placed ladder
                m_renderer.material = m_material;
                m_navMeshObs.enabled = false;
                m_ladderOnPlayer.SetActive(false);
                m_uiInventory.DropItemFunction(Item.ItemType.echelle, 1, false);

                if (!m_questManager.m_ladderPlacedDone)
                {
                    m_questSystem.ChangeQuest("A vous de jouer.");

                    m_questManager.m_ladderPlacedDone = true;
                }

                //if its a ladderOnSameBoard prefab
                if (m_ladder2 != null)
                {
                    m_meshLadder2.enabled = true;
                    m_meshLadder2.material = m_material;
                    m_navMeshObs2.enabled = false;
                }

                m_ladderPlaced = true;
                StartCoroutine(WaitForSec());
            }
        }

        //Ladders on same board handler
        if (p_other.CompareTag("Player") && m_ladderPlaced && gameObject.CompareTag("LadderOnSameBoard") && !m_alreadyUsedLadder && transform.childCount>0)
        {
            float distToLadder1 = Vector3.Distance(transform.GetChild(0).position, p_other.transform.position);
            float distToLadder2 = Vector3.Distance(m_ladder2.position, p_other.transform.position);

            // 1.75 is the distance in which the player will teleport from a ladder to the other
            if (distToLadder1 < 1.75f || distToLadder2 < 1.75f)
            {
                if (distToLadder1 < distToLadder2)
                {
                    //raise position de la ladder 2 car on s'y rend
                    Debug.Log("ladder 1");
                    m_onLadderClimb.Raise(m_ladder2.position);
                }
                else if (distToLadder1 > distToLadder2)
                {
                    // l'inverse
                    Debug.Log("ladder 2");
                    m_onLadderClimb.Raise(transform.GetChild(0).position);
                }
                m_alreadyUsedLadder = true;
            }

        }

    }

    /// <summary>
    /// Wait half a sec so that the player doesnt get tped at the same frame he uses the ladder
    /// makes it easier to understand and less surprising
    /// </summary>
    private IEnumerator WaitForSec()
    {
        m_alreadyUsedLadder = true;
        yield return new WaitForSeconds(0.7f);
        m_alreadyUsedLadder = false;
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (p_other.CompareTag("Player") && m_alreadyUsedLadder)
        {
            m_alreadyUsedLadder = false;

        }
    }

    private void OnTriggerExit(Collider p_other)
    {
        //hide the ladder if not placed 
        if (p_other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {
            m_meshLadder.enabled = false;

        }

    }

   
}
