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

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [SerializeField] QuestManager m_questManager;
    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] UI_QuestObjects m_uiQuestObjects;

    [SerializeField] UI_Inventory m_uiInventory;
    [SerializeField] DropItemZone m_dropItemZone;
    [SerializeField] TextMeshProUGUI m_amounItemsInventory;

    [SerializeField] AudioManager m_audioManager;

    private AudioSource m_audioSource;
 
    [SerializeField] Animator m_animatorPlayer;

    [SerializeField] ActivateQuestObject m_activateQuestObject;



    private void Awake()
    {

        m_audioSource = GetComponent<AudioSource>();

        inventory = new Inventory(UseItem);
        m_uiInventory.SetPlayer(this);
        m_uiInventory.SetInventory(inventory);

        // Reset the sync var between the plays in the editor
        m_inventorySpace.Value = 4;

        if (!PauseMenu.m_restarted)
            m_questManager.m_craftToolDone = false;

        m_questManager.m_destroyPlantDone = false;
        m_questManager.m_findDungeonDone = false;
        m_questManager.m_levierEnigmaDone = false;
        m_questManager.m_candleEnigmaDone = false;

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (Inventory.itemList.Count + Inventory.toolsList.Count < m_inventorySpace.Value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

            if (itemWorld != null)
            {
                m_audioSource.PlayOneShot(m_audioManager.m_pickUpSound);

                if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("CourseOutils") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("IdleOutils"))
                {
                    StartCoroutine(PickUpAnim(false));
                }
                else if (m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Course") || m_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    StartCoroutine(PickUpAnim(true));

                }

                if (collider.gameObject.tag == "Untagged")
                {

                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
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
            m_amounItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count }/{m_inventorySpace.Value}";

        }
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
