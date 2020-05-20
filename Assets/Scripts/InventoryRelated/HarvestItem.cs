using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GD2Lib;
using TMPro;

public class HarvestItem : MonoBehaviour
{

    private Inventory inventory;

    private LifePlayer m_lifePlayer;

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
 
    [SerializeField] Animator m_animatorPlayer;


    // Start is called before the first frame update
    private void Awake()
    {

        m_audioManager.m_audioSource = GetComponent<AudioSource>();

        m_lifePlayer = GetComponent<LifePlayer>();
        inventory = new Inventory(UseItem);
        m_uiInventory.SetPlayer(this);
        m_uiInventory.SetInventory(inventory);

        // Reset the sync var between the plays in the editor
        m_inventorySpace.Value = 4;

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnEnable()
    {
        m_questManager.m_craftToolDone = false;
        m_questManager.m_destroyPlantDone = false;
        m_questManager.m_findDungeonDone = false;
        m_questManager.m_levierEnigmaDone = false;
        m_questManager.m_candleEnigmaDone = false;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (Inventory.itemList.Count + Inventory.toolsList.Count < m_inventorySpace.Value)
        {
            ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

            if (itemWorld != null)
            {
                m_audioManager.m_audioSource.PlayOneShot(m_audioManager.m_pickUpSound);

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
            }
        }

        if (collider.CompareTag("QuestObject"))
        {
            ActivateQuestObject.m_canUseItem = true;
            Key.m_canUseItem = true;
            Invoke("UI_ShowObject", .1f);
            Destroy(collider.gameObject, .5f);
        }

    }

    private void UI_ShowObject()
    {
        m_uiQuestObjects.UI_ShowObject();
        Debug.Log("LEVIER BORDEL");
    }

    private IEnumerator PickUpAnim(bool playerwasIdle)
    {
        m_animatorPlayer.SetTrigger("PickUp");
        yield return new WaitForSeconds(0.75f);
        if (playerwasIdle)
        {
            m_animatorPlayer.SetTrigger("Idle");
        }
        else
        {
            m_animatorPlayer.SetTrigger("IdleOutils");
        }
        StopAllCoroutines();
    }

    private void UseItem(Item.ItemType p_itemType)
    {
        switch (p_itemType)
        {
            case Item.ItemType.baton:
                //m_lifePlayer.HealingFunc();

                break;
            case Item.ItemType.tissu:
                Debug.Log("Carré utilisé");

                break;
            case Item.ItemType.mrcFer:
                Debug.Log("Triangle utilisé");

                break;

        }
    }
}
