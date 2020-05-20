using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftTable : MonoBehaviour
{
    [SerializeField] GameObject m_craftcanvas;
    [SerializeField] GameObject m_craftText;

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    private void Awake()
    {
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
        }

        if (BluePrintObjects.m_ladderBluePrintDiscovered)
        {
            bool tutoDecraftDone = false;
            if (!tutoDecraftDone)
            {
                m_notification.SetActive(true);
                m_textNotification.text = "Il est aussi possible de demanteler les outils.";
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
