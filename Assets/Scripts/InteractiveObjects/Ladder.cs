using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ladder : MonoBehaviour
{

    [SerializeField] MeshRenderer m_meshLadder;
    [SerializeField] NavMeshObstacle m_navMeshObs;

    private Inventory m_inventorydze;
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] GameObject m_ladderOnPlayer;

    Renderer m_renderer;

    private bool m_ladderPlaced;

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
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {
            m_meshLadder.enabled = true;
            if (m_ladderOnPlayer.activeInHierarchy)
            {
                m_renderer.material = m_material;
                m_navMeshObs.enabled = false;
                m_ladderOnPlayer.SetActive(false);
                m_uiInventory.DropToolFunction(Item.ItemType.echelle, 1);

                m_ladderPlaced = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && BluePrintObjects.m_ladderBluePrintDiscovered && !m_ladderPlaced)
        {
            m_meshLadder.enabled = false;

        }
    }
}
