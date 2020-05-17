using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] Transform[] m_itemsUnlockedSlots;

    private Image m_image;


    [Header("ITEM A CHOISIR")]

    [SerializeField] Item.ItemType[] m_itemTypeGiven;
    [SerializeField] int m_amountItemGiven;

    public void HarvestChestItems()
    {
        m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(true);
        Invoke("DisableSlot", 5f);

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
        Debug.Log("give");
        Destroy(this, 5.1f);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            HarvestChestItems();
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
            case "tissu":
                m_image.sprite = ItemAssets.Instance.tissuSprite;
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
