using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// It's the workbench class
/// </summary>
public class CraftTable : MonoBehaviour
{
    [Header("Craft window in our inventory")]
    [SerializeField] GameObject m_craftcanvas;
    [SerializeField] GameObject m_craftText;

    [Header("Notification related")]
    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;
    [SerializeField] AutoDisableNotification m_autoDisableNotification;

    [Header("UI_Inventory window")]
    [SerializeField] GameObject m_uiInventory;

    [Header("Tutorial cursor")]
    [SerializeField] GameObject m_cursor;

    [Header("Quest Related")]
    [SerializeField] QuestManager m_questManager;

    bool tutoDecraftDone = false;

    /// <summary>
    /// we set variable to their initial value
    /// </summary>
    private void Awake()
    {
        m_questManager.m_tutoDecraftDone = false;
        m_craftcanvas.SetActive(false);

        //layer 2 is the ignore ray cast layer, when we click on it, nothing happens, except the player move to the position
        gameObject.layer = 2;

    }


    public void OnTriggerStay(Collider other)
    {
        //if it collides the player
        if (other.gameObject.CompareTag("Player"))
        {
            //we set active the craft window in the inventory
            m_craftcanvas.SetActive(true);
            m_craftText.SetActive(false);

            //we set the layer to default, so we can click on it and it opens the inventory
            gameObject.layer = 0;

            //if we have discovered the ladder blueprint and the inventory is active
            if (BluePrintObjects.m_ladderBluePrintDiscovered && m_uiInventory.activeInHierarchy)
            {
                //if we didn't already do this tutorial, we display the tutorial to uncraft the tools
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

    /// <summary>
    /// we reset the values
    /// </summary>
    /// <param name="other"></param>
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
