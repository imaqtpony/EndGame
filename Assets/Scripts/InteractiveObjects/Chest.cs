using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    [SerializeField] UI_Inventory m_uiInventory;
    
    private Image m_imageitemUnlocked;

    [SerializeField] GameObject m_pollution;
    [SerializeField] Animator m_chestAnimator;

    [Header("ITEM TO CHOOSE AND AMOUNT")]
    [SerializeField] Item.ItemType[] m_itemTypeGiven;
    [SerializeField] int m_amountItemGiven;
    [SerializeField] Transform[] m_itemsUnlockedSlots;

    [Header("SOUND RELATED")]
    [SerializeField] AudioManager m_audioManager;
    private AudioSource m_audioSource;

    private bool m_chestUsed;

    [Header("DUNGEON NOTIFICATION")]
    [SerializeField] GameObject m_donjonNotif;
    [SerializeField] TextMeshProUGUI m_donjonNotifText;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// this function give us the number of different items
    /// the amount, and display according to the number of item the exact canvas in the hierarchy
    /// (if we obtain 1, 2 or 3 items)
    /// </summary>
    public void HarvestChestItems()
    {
        //we set active the good canvas according to the number of items given
        m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(true);

        //we wait for 5s before set it inactive (we wait for its animation end)
        Invoke("DisableSlot", 5f);

        //we trigger the animation of the chest
        m_chestAnimator.SetTrigger("Open");

        //and play the sound
        m_audioSource.PlayOneShot(m_audioManager.m_openingChestSound);

        //the variable is a security, to give to the player the good amount of each item
        int p_amount = m_amountItemGiven;

        //we execute this as much as there is item in the chest
        for (int i = 0; i < m_itemTypeGiven.Length; i++)
        {

            //we check if the item type is != from plan_echelle because it's not an "item"
            if (m_itemTypeGiven[i] != Item.ItemType.plan_echelle)
            {
                //then we give the item with the index equal to "i" in the list
                m_uiInventory.ChestItem(m_itemTypeGiven[i], p_amount);
            }

            //we get the image component to associate the sprite to
            m_imageitemUnlocked = m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].GetChild(i).GetComponent<Image>();
            m_imageitemUnlocked.sprite.name = m_itemTypeGiven[i].ToString();
            AssociateSprite();

        }

        if(gameObject.name == "PremierCoffreDonjon" && CleaningPollution.m_zoneDepolluted)
        {
            StartCoroutine(DisableDonjonNotif());
            CleaningPollution.m_zoneDepolluted = false;
        }

        Destroy(this, 5.1f);
    }

    /// <summary>
    /// we use coroutine instead of using update
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableDonjonNotif()
    {
        //we wait the previous notification
        yield return new WaitForSeconds(4);

        //we set active the notification telling us that we did the first dungeon
        m_donjonNotif.SetActive(true);
        m_audioSource.PlayOneShot(m_audioManager.m_depollutedZoneSound);

        yield return new WaitForSeconds(4);
        //and we disable it
        m_donjonNotif.SetActive(false);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !m_chestUsed)
        {
            HarvestChestItems();
            //we change the boolean value to avoid the player to farm the item in the chest
            m_chestUsed = true;

        }

    }

    private void DisableSlot()
    {
        m_itemsUnlockedSlots[m_itemTypeGiven.Length - 1].gameObject.SetActive(false);

    }

    /// <summary>
    /// we modify the sprite according to the item
    /// </summary>
    public void AssociateSprite()
    {

        switch (m_imageitemUnlocked.sprite.name)
        {
            case "baton":
                m_imageitemUnlocked.sprite = ItemAssets.Instance.batonSprite;
                break;
            case "caillou":
                m_imageitemUnlocked.sprite = ItemAssets.Instance.caillouSprite;
                break;
            case "mrcFer":
                m_imageitemUnlocked.sprite = ItemAssets.Instance.mrcFerSprite;
                break;
            case "plan_echelle":
                m_imageitemUnlocked.sprite = ItemAssets.Instance.plan_echelleSprite;
                break;
        }
    }
}
