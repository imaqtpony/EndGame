﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftTable : MonoBehaviour
{
    [SerializeField] GameObject m_craftcanvas;
    [SerializeField] GameObject m_craftText;

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [SerializeField] GameObject m_uiInventory;

    [SerializeField] GameObject m_cursor;

    [SerializeField] AutoDisableNotification m_autoDisableNotification;

    [SerializeField] QuestManager m_questManager;

    private bool m_tutoCraftDone;

    bool tutoDecraftDone = false;

    private void Awake()
    {

        m_questManager.m_tutoDecraftDone = false;

        m_craftcanvas.SetActive(false);
        gameObject.layer = 2;

    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(true);
            m_craftText.SetActive(false);
            gameObject.layer = 0;

            if (BluePrintObjects.m_ladderBluePrintDiscovered && m_uiInventory.activeInHierarchy)
            {
                if (!m_questManager.m_tutoDecraftDone)
                {
                    m_notification.SetActive(true);
                    m_textNotification.text = "Vous pouvez aussi demanteler les outils.";
                    m_cursor.SetActive(true);
                    m_autoDisableNotification.PlayAnimCursor("Decraft");
                    m_questManager.m_tutoDecraftDone = true;
                }
            }
        }


    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_craftcanvas.SetActive(false);
            m_craftText.SetActive(true);
            gameObject.layer = 2;

        }

    }


}
