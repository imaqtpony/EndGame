using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InventoryNS.Utils;

/// <summary>
/// Script which make object interact between themselves and the raycast (=touch) of the player
/// </summary>
public class InteractWithObjects : MonoBehaviour
{
    [Tooltip("EACH OBJECT SHOWS A CANVAS")]
    [SerializeField] GameObject m_goToShow;

    [SerializeField] InventoryButton m_inventoryButton;

    [Tooltip("CHANGING TEXT COLOR")]
    [SerializeField] TextMeshProUGUI m_interactText;
    public Color m_colorText;

    //check if the player is in the trigger
    bool m_canShow;

    //avoid the player to close the current window and move 
    public static bool m_gotInteracted;


    private void Start()
    {
        //layer 2 = ignore raycast (the raycast goes through)
        gameObject.layer = 2;

    }

    private void Update()
    {
        //we detect the touch and if the player is near
        if (Input.touchCount > 0 && m_canShow)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //we launch a raycast from the camera to the object in the scene
                RaycastHit hit;
                Ray castRay = Camera.main.ScreenPointToRay(touch.position);

                //its range is infinity
                if (Physics.Raycast(castRay, out hit, Mathf.Infinity))
                {
                    //we check if we hit something and if it the workbench
                    if (hit.transform != null && hit.collider.gameObject.CompareTag("Workbench"))
                    {
                        m_gotInteracted = true;

                        if (gameObject.CompareTag("Workbench"))
                        {
                            //we display the inventory
                            m_inventoryButton.OpenInventory();
                            m_gotInteracted = false;

                        }
                    }
                    //if it is the clue pannel
                    else if (hit.transform != null && hit.collider.gameObject.CompareTag("CluePannel"))
                    {
                        //we show the canvas with the clue in it
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
            //layer 0 = default (the raycast hits it)
            gameObject.layer = 0;

            m_canShow = true;

            //the text takes the new color
            m_interactText.color = m_colorText;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_canShow = false;

            //the text takes the initial color
            m_interactText.color = Color.white;

            //we disable the canvas
            m_goToShow.SetActive(false);
            gameObject.layer = 2;

        }
    }
}
