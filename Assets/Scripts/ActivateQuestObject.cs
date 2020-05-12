using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuestObject : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] Animator m_animatorSlotLevier;
    public static bool m_canUseItem;

    [SerializeField] GameObject m_player;

    private Renderer m_meshRenderer;

    private float m_distanceWithPlayer;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_meshRenderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        //check the distance with the player
        m_distanceWithPlayer = Vector3.Distance(m_player.transform.position, transform.position);

        if (m_distanceWithPlayer <= 2.5f)
        {
            m_animatorSlotLevier.SetTrigger("FlashingQuestObject");
            m_canUseItem = true;
            if (UI_QuestObjects.m_activateLevier)
            {
                m_meshRenderer.enabled = true;
                m_animator.SetTrigger("Activate");
            }
        }
        else
        {
            m_canUseItem = false;
            m_animatorSlotLevier.SetTrigger("DisplayQuestObject");

        }

    }


    private void OnTriggerExit(Collider collider)
    {
        m_canUseItem = false;

    }
}
