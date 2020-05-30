using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] Transform[] m_itemsUnlockedSlots;

    private Image m_image;

    [SerializeField] GameObject m_pollution;

    [SerializeField] Animator m_chestAnimator;

    [Header("ITEM A CHOISIR")]

    [SerializeField] Item.ItemType[] m_itemTypeGiven;
    [SerializeField] int m_amountItemGiven;

    [SerializeField] AudioManager m_audioManager;

    private AudioSource m_audioSource;

    private bool m_chestUsed;

    [SerializeField] GameObject m_donjonNotif;
    [SerializeField] TextMeshProUGUI m_donjonNotifText;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void HarvestChestItems()
    {
        m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(true);
        Invoke("DisableSlot", 5f);
        m_chestAnimator.SetTrigger("Open");
        m_audioSource.PlayOneShot(m_audioManager.m_openingChestSound);

        int p_amount = m_amountItemGiven;
        for (int i = 0; i < m_itemTypeGiven.Length; i++)
        {
            if (m_itemTypeGiven[i] != Item.ItemType.plan_echelle)
            {
                m_uiInventory.ChestItem(m_itemTypeGiven[i], p_amount);
            }

            m_image = m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].GetChild(i).GetComponent<Image>();
            m_image.sprite.name = m_itemTypeGiven[i].ToString();
            AssociateSprite();

        }

        if(gameObject.name == "PremierCoffreDonjon")
        {
            StartCoroutine(DisableDonjonNotif());
        }

        Destroy(this, 5.1f);
    }

    private IEnumerator DisableDonjonNotif()
    {
        yield return new WaitForSeconds(4);
        m_donjonNotif.SetActive(true);
        yield return new WaitForSeconds(4);
        m_donjonNotif.SetActive(false);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !m_chestUsed)
        {
            HarvestChestItems();
            Debug.Log(gameObject.name);
            m_chestUsed = true;

        }

    }

    private void DisableSlot()
    {
        m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(false);

    }


    public void AssociateSprite()
    {

        switch (m_image.sprite.name)
        {
            case "baton":
                m_image.sprite = ItemAssets.Instance.batonSprite;
                break;
            case "caillou":
                m_image.sprite = ItemAssets.Instance.caillouSprite;
                break;
            case "mrcFer":
                m_image.sprite = ItemAssets.Instance.mrcFerSprite;
                break;
            case "plan_echelle":
                m_image.sprite = ItemAssets.Instance.plan_echelleSprite;
                break;
        }
    }
}
