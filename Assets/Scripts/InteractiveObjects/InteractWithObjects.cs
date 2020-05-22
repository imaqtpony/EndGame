﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class InteractWithObjects : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] GameObject m_goToShow;
    [SerializeField] InventoryButton m_inventoryButton;

    [SerializeField] TextMeshProUGUI m_interactText;

    public static bool m_canShow;

    public static bool m_gotInteracted;

    public Color m_colorText;

    private void Start()
    {
        gameObject.layer = 2;

    }

    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_canShow)
        {
            m_gotInteracted = true;

            if (gameObject.CompareTag("Workbench"))
            {
                m_inventoryButton.OpenInventory();
                m_gotInteracted = false;

            }
            m_goToShow.SetActive(true);
            Debug.Log("LOBJET INTERAGIT");
        }
        else
        {
            Debug.Log("LOBJET INTERAGIT PAS");

            m_gotInteracted = false;
            m_goToShow.SetActive(false);

        }

    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            gameObject.layer = 0;

            m_canShow = true;
            m_interactText.color = m_colorText;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_canShow = false;
            m_interactText.color = Color.white;

            m_goToShow.SetActive(false);
            gameObject.layer = 2;

        }
    }
}
