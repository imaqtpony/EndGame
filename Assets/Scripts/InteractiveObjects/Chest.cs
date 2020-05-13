using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] UI_Inventory m_uiInventory;

    [SerializeField] Transform[] m_itemsUnlockedSlots;

    [SerializeField] string m_IDChest;

    private Image m_image;


    [Header("FOR SPECIAL CHEST")]

    [SerializeField] Item.ItemType[] m_itemTypeGiven;
    [SerializeField] int m_amountItemGiven;


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            int p_amount = m_amountItemGiven;
            for (int i = 0; i < m_itemTypeGiven.Length; i++)
            {
   
                m_uiInventory.ChestItem(m_itemTypeGiven[i], p_amount);
                m_image = m_itemsUnlockedSlots[m_itemTypeGiven.Length].GetChild(i).GetComponent<Image>();
                AssociateSprite();
                Debug.Log("give");
            }


            m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(true);

            Destroy(this);
        }
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
        }
    }
}
