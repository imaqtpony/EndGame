using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventoryNS.Utils;
using GD2Lib;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    [Header("Quest system")]
    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;


    [Header("Elements needed to be found")]
    [SerializeField] Transform m_ressourcesSlotContainer;
    [SerializeField] Transform m_ressourcesSlotTemplate;
    [SerializeField] Transform m_toolsWindow;
    [SerializeField] Transform m_toolsSlotContainer;
    [SerializeField] Transform m_toolsSlotTemplate;
    [SerializeField] Transform m_craftResult;

    [SerializeField] List<Transform> m_craftSlot;

    [SerializeField] CraftSystem m_craftSystem;

    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    public List<Transform> m_itemForCraft;

    public IntVar m_inventorySpace;
    [SerializeField] TextMeshProUGUI m_amountItemsInventory;

    private HarvestItem player;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField] AudioSource m_audioSource;

    [SerializeField] Transform m_yellowStar;

    public void SetPlayer(HarvestItem player)
    {
        this.player = player;
    }

    private void Awake()
    {
        m_audioManager.m_audioSource = GetComponent<AudioSource>();

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        inventory.OnToolsListChanged += Inventory_OnToolsListChanged;
        RefreshInventoryRessources();
        RefreshInventoryTools();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryRessources();

    }

    private void Inventory_OnToolsListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryTools();

    }

    private IEnumerator YellowStarFeedBack()
    {
        //on active son animation qui tourne
        m_yellowStar.GetComponent<Animator>().SetTrigger("Activate");
        //quand on desactive l'inventaire des outils, l'animator et image de l'etoile se desactivent sans raison
        //donc on les reactive
        m_yellowStar.GetComponent<Animator>().enabled = true;
        m_yellowStar.GetComponent<Image>().enabled = true;

        //on attend 1seconde soit la fin de l'anim pour continuer
        yield return new WaitForSeconds(1f);

        //on desactive le visuel de l'étoile
        m_yellowStar.GetComponent<Image>().enabled = false;
        //on revient à l'etat de base de l'animation de l'etoile
        m_yellowStar.GetComponent<Animator>().SetTrigger("DeActivate");

    }

    private IEnumerator InventoryFull()
    {
        m_notification.SetActive(true);
        m_amountItemsInventory.GetComponent<Animator>().SetTrigger("Activate");
        m_textNotification.text = "Inventaire Plein !";
        yield return new WaitForSeconds(1);
        m_amountItemsInventory.GetComponent<Animator>().SetTrigger("DeActivate");

    }

    public void RefreshInventoryRessources()
    {
        foreach (Transform child in m_ressourcesSlotContainer)
        {
            if (child == m_ressourcesSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 105f;
        
        //lorsqu'on clique sur le bouton de craft
        m_craftResult.GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (m_craftSystem.m_craftActive)
            {
                if(Inventory.itemList.Count + Inventory.toolsList.Count < m_inventorySpace.Value)
                {
                    CraftTools();
                    UpdateAmountItems();
                    RemoveItemFromCraftSlot();
                    StartCoroutine(YellowStarFeedBack());
                }
                else
                {
                    StartCoroutine(InventoryFull());
                }

            }

        };

        

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(m_ressourcesSlotTemplate, m_ressourcesSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Item").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = image.transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");

            }

            x++;
            if (x > 2)
            {
                x = 0;
                y--;
            }
        }
    }

    public void RefreshInventoryTools()
    {
        foreach (Transform child in m_toolsSlotContainer)
        {
            if (child == m_toolsSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;


        foreach (Item item in inventory.GetToolsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(m_toolsSlotTemplate, m_toolsSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Tools").GetComponent<Image>();

            m_yellowStar.parent = itemSlotRectTransform;
            m_yellowStar.transform.SetSiblingIndex(2);

            m_yellowStar.position = image.transform.position;
            Debug.Log("fzzdadada");
            image.sprite = item.GetSprite();

            x++;
            if (x > 0)
            {
                x = 0;
                y--;
            }
        }
    }

    public void CraftTools()
    {
        //craft hache en fer
        if (Inventory.m_amountBaton >= 2 && Inventory.m_amountMrcFer >= 1 && CraftSystem.m_itemType_1 == Item.ItemType.hache)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.hache, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.mrcFer, amount = 1 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft allumette
        else if (Inventory.m_amountPoudre >= 1 && Inventory.m_amountBaton >= 1 && CraftSystem.m_itemType_1 == Item.ItemType.allumette)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.allumette, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.poudre, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });

            m_questManager.m_craftToolDone = true;
            if (!m_questManager.m_destroyPlantDone)
            {
                m_questSystem.ChangeQuest("Brulez les plantes.");

            }
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft hache en pierre
        else if (Inventory.m_amountBaton >= 1 && Inventory.m_amountCaillou >= 2 && CraftSystem.m_itemType_1 == Item.ItemType.hache_pierre)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.hache_pierre, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.caillou, amount = 2 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft echelle
        else if (Inventory.m_amountBaton >= 4 && m_craftSystem.m_craftSlotList.Count == 1 && CraftSystem.m_itemType_1 == Item.ItemType.echelle)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.echelle, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 4 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }

        //decraft
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.tissu)
        {
            inventory.RemoveTools(new Item { itemType = Item.ItemType.allumette, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.poudre, amount = 1 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            RemoveItemFromCraftSlot();

        }
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.caillou)
        {
            inventory.RemoveTools(new Item { itemType = Item.ItemType.hache_pierre, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.caillou, amount = 1 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            RemoveItemFromCraftSlot();

        }
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.mrcFer)
        {
            inventory.RemoveTools(new Item { itemType = Item.ItemType.hache, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.baton, amount = 2 });
            inventory.AddItem(new Item { itemType = Item.ItemType.mrcFer, amount = 1 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            RemoveItemFromCraftSlot();

        }
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton)
        {
            inventory.RemoveTools(new Item { itemType = Item.ItemType.echelle, amount = 1 });
            inventory.AddItem(new Item { itemType = Item.ItemType.baton, amount = 4 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            RemoveItemFromCraftSlot();

        }
        else
        {
            RefreshInventoryRessources();
            RefreshInventoryTools();
            m_notification.SetActive(true);
            m_textNotification.text = "Ressources insuffisantes";
            //pas assez de ressources
        }

    }

    public void DropItemFunction(Item.ItemType itemTypeToDrop, int p_amount)
    {
        //y avait un foreach ici
        Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
        inventory.RemoveItem(new Item { itemType = itemTypeToDrop, amount = p_amount });
        ItemWorld.DropItem(player.GetPosition(), duplicateItem);
        UpdateAmountItems();
    }

    public void DropToolFunction(Item.ItemType itemTypeToDrop, int p_amount)
    {
        if(itemTypeToDrop != Item.ItemType.echelle)
        {
            Debug.Log(itemTypeToDrop);
            Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
            inventory.RemoveTools(new Item { itemType = itemTypeToDrop, amount = p_amount });
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();
        }
        else if(itemTypeToDrop == Item.ItemType.echelle)
        {
            inventory.RemoveTools(new Item { itemType = itemTypeToDrop, amount = p_amount });

        }
    }

    public void UseItemFunction(Item.ItemType p_itemTypeToUse)
    {

        inventory.UseItem(p_itemTypeToUse);
        UpdateAmountItems();

    }

    public void DropAllItemFunction()
    {
        foreach (Item item in inventory.GetItemList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

        }

        inventory.RemoveAllItems();
        RemoveItemFromCraftSlot();
    }

    
    public void RemoveItemFromCraftSlot()
    {
        if (m_craftSystem.m_craftSlotList.Count > 0)
        {

            if (m_craftSlot[0].childCount > 2)
            {
                m_craftSystem.m_craftSlotList.Clear();
                m_craftSystem.NotEnoughItemToCraft();
                Destroy(m_craftSlot[0].transform.GetChild(2).gameObject);
                UpdateAmountItems();
            }
            if (m_craftSlot[1].childCount > 2)
            {
                m_craftSystem.m_craftSlotList.Clear();
                m_craftSystem.NotEnoughItemToCraft();
                Destroy(m_craftSlot[1].transform.GetChild(2).gameObject);
                UpdateAmountItems();
            }
        }
        
    }

    private void UpdateAmountItems()
    {
        m_amountItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count}/{m_inventorySpace.Value}";

    }

    public void ChestItem(Item.ItemType p_itemType, int p_amount)
    {
        inventory.AddItem(new Item { itemType = p_itemType, amount = 1 });
        UpdateAmountItems();

    }

}
