using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class InteractWithObjects : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] GameObject m_goToShow;
    [SerializeField] InventoryButton m_inventoryButton;

    [SerializeField] TextMeshProUGUI m_interactText;

    string m_initialText;
    private bool m_canShow;

    public static bool m_gotInteracted;

    private void Start()
    {
        m_initialText = m_interactText.text;
    }

    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_canShow)
        {
            if (gameObject.CompareTag("Workbench"))
            {
                m_inventoryButton.OpenInventory();
            }
            m_goToShow.SetActive(true);
            m_gotInteracted = true;
        }
        else
        {
            m_goToShow.SetActive(false);

        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_canShow = true;
            m_interactText.text = "Interagir";
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_canShow = false;
            m_interactText.text = m_initialText;

        }
    }
}
