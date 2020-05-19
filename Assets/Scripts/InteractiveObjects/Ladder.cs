using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GD2Lib;

public class Ladder : MonoBehaviour
{

    [SerializeField] MeshRenderer m_meshLadder;
    [SerializeField] NavMeshObstacle m_navMeshObs;

    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] GameObject m_ladderOnPlayer;

    Renderer m_renderer;

    private bool m_ladderPlaced;

    private bool m_alreadyUsedLadder;

    [SerializeField] Material m_material;

    public static bool m_ladderUsing;

    private Transform m_ladder2;
    private MeshRenderer m_meshLadder2;
    private NavMeshObstacle m_navMeshObs2;

    [SerializeField]
    private GD2Lib.Event m_onLadderClimb;

    public void Start()
    {
        m_meshLadder.enabled = false;
        m_navMeshObs.enabled = true;
        m_alreadyUsedLadder = false;

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

    private void OnTriggerStay(Collider other)
    {
        //BluePrintObjects.m_ladderBluePrintDiscovered = true;
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {
            m_meshLadder.enabled = true;
            if (m_ladderOnPlayer.activeInHierarchy)
            {
                m_renderer.material = m_material;
                m_navMeshObs.enabled = false;
                m_ladderOnPlayer.SetActive(false);
                m_uiInventory.DropToolFunction(Item.ItemType.echelle, 1);

                //if its a ladderOnSameBoard prefab
                if (m_ladder2 != null)
                {
                    m_meshLadder2.enabled = true;
                    m_meshLadder2.material = m_material;
                    m_navMeshObs2.enabled = false;
                }

                m_ladderPlaced = true;
            }
        }

        if (other.CompareTag("Player") && m_ladderPlaced && gameObject.CompareTag("LadderOnSameBoard") && !m_alreadyUsedLadder && transform.childCount>0)
        {
            float distToLadder1 = Vector3.Distance(transform.GetChild(0).position, other.transform.position);
            float distToLadder2 = Vector3.Distance(m_ladder2.position, other.transform.position);

            // 1 is the distance in which the player will teleport from a ladder to the other
            if (distToLadder1 < 2 || distToLadder2 < 2)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {
            m_meshLadder.enabled = false;

        }

        if (other.CompareTag("Player"))
            m_alreadyUsedLadder = false;

    }
}
