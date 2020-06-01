using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GD2Lib;
using TMPro;

/// <summary>
/// This class manage the harvest action and place the items in the inventory
/// Works with UI_Inventory scripts
/// </summary>
public class HarvestItem : MonoBehaviour
{

    private Inventory inventory;
    public IntVar m_inventorySpace;

    [Header("NOTIFICATION RELATED")]
    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [Header("QUEST RELATED")]
    [SerializeField] QuestManager m_questManager;
    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] UI_QuestObjects m_uiQuestObjects;
    [SerializeField] ActivateQuestObject m_activateQuestObject;

    [Header("INVENTORY RELATED")]
    [SerializeField] UI_Inventory m_uiInventory;
    [SerializeField] DropItemZone m_dropItemZone;
    [SerializeField] TextMeshProUGUI m_amounItemsInventory;

    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;
 
    [SerializeField] Animator m_animatorPlayer;

    private void Awake()
    {

        inventory = new Inventory(UseItem);
        m_uiInventory.SetPlayer(this);
        m_uiInventory.SetInventory(inventory);

        // Reset the sync var between the plays in the editor
        m_inventorySpace.Value = 4;

        //we chose this script to reset the values of the scriptable object Quest Manager
        if (!PauseMenu.m_restarted)
            m_questManager.m_craftToolDone = false;
            m_questManager.m_tutoToolsDone = false;
            m_questManager.m_tutoLadderDone = false;
            m_questManager.m_tutoDecraftDone = false;

        m_questManager.m_destroyPlantDone = false;
        m_questManager.m_findDungeonDone = false;
        m_questManager.m_levierEnigmaDone = false;
        m_questManager.m_candleEnigmaDone = false;

    }

    //position of the player
    public Vector3 GetPosition()
    {
        return transform.position;
    }


    private void OnTriggerEnter(Collider collider)
    {
        //if we have enough space in our inventory
        if (Inventory.itemList.Count + Inventory.toolsList.Count < m_inventorySpace.Value && DropItemZone.m_canHarvestItem)
        {

            //we get the ItemWorld script of the item
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

            //if it is an item
            if (itemWorld != null)
            {
                m_audioSource.PlayOneShot(m_audioManager.m_pickUpSound);

                //we play those animations
                if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils"))
                {
                    StartCoroutine(PickUpAnim(false));
                }
                else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Course") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    StartCoroutine(PickUpAnim(true));

                }

                //if it is a ressource
                if (collider.gameObject.tag == "Untagged")
                {

                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();

                    //tutorial 
                    if (!m_questManager.m_craftToolDone && Inventory.itemList.Count == 2)
                    {
                        m_questSystem.ChangeQuest("Construisez un outil");

                        m_questManager.m_craftToolDone = true;
                    }

                }
                else if (collider.gameObject.tag == "Tools" || collider.gameObject.tag == "SecondaryObject")
                {
                    inventory.AddTools(itemWorld.GetItem());
                    itemWorld.DestroySelf();

                }

            }

            //update the amount of items
            m_amounItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count }/{m_inventorySpace.Value}";

        }
        //if we don't have enough space
        else if(Inventory.itemList.Count + Inventory.toolsList.Count >= m_inventorySpace.Value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
            if (itemWorld != null)
            {
                m_notification.SetActive(true);
                m_textNotification.text = "Inventaire Plein !";
                m_textNotification.color = new Color(255, 75, 0);
            }
        }

        //QUEST OBJECT
        if (collider.CompareTag("Levier"))
        {
            ActivateQuestObject.m_gotLever = true;
            m_activateQuestObject.ShowSlot(collider.tag.ToString());
            Destroy(collider.GetComponent<MeshRenderer>());
            Destroy(collider.gameObject, 2f);

            if (!m_audioSource.isPlaying)
            {
                m_audioSource.PlayOneShot(m_audioManager.m_pickUpQuestObjectSound);
            }

        }
        //QUEST OBJECT
        else if (collider.CompareTag("Clef"))
        {
            Key.m_gotKey = true;
            m_activateQuestObject.ShowSlot(collider.tag.ToString());
            Destroy(collider.GetComponent<MeshRenderer>());

            Destroy(collider.gameObject, 2f);
            if (!m_audioSource.isPlaying)
            {
                m_audioSource.PlayOneShot(m_audioManager.m_pickUpQuestObjectSound);
            }
        }

    }

    /// <summary>
    /// pick up animation
    /// </summary>
    /// <param name="playerwasIdle">check the animation of the player when he picks up something</param>
    /// <returns></returns>
    private IEnumerator PickUpAnim(bool playerwasIdle)
    {
        m_animatorPlayer.SetTrigger("PickUp");
        yield return new WaitForSeconds(.5f);
        if (playerwasIdle)
        {
            m_animatorPlayer.SetTrigger("Course");
        }
        else
        {
            m_animatorPlayer.SetTrigger("CourseOutils");
        }
        StopAllCoroutines();
    }

    private void UseItem(Item.ItemType p_itemType)
    {

    }
}
