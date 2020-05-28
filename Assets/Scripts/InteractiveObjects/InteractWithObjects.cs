using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InventoryNS.Utils;


public class InteractWithObjects : MonoBehaviour
{

    [SerializeField] GameObject m_goToShow;
    [SerializeField] InventoryButton m_inventoryButton;

    [SerializeField] TextMeshProUGUI m_interactText;

    bool m_canShow;

    public static bool m_gotInteracted;

    public Color m_colorText;

    private void Start()
    {
        gameObject.layer = 2;

    }

    private void Update()
    {
        if (Input.touchCount > 0 && m_canShow)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray castRay = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(castRay, out hit, Mathf.Infinity))
                {
                    if (hit.transform != null && hit.collider.gameObject.CompareTag("Workbench"))
                    {
                        m_gotInteracted = true;

                        if (gameObject.CompareTag("Workbench"))
                        {
                            m_inventoryButton.OpenInventory();
                            m_gotInteracted = false;

                        }
                        m_goToShow.SetActive(true);
                    }
                }
            }

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
