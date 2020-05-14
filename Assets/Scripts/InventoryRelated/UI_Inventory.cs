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

    public List<Transform> m_itemForCraft;

    public IntVar m_inventorySpace;
    [SerializeField] TextMeshProUGUI m_amountItemsInventory;

    [SerializeField] TextMeshProUGUI m_inventoryFull;

    private HarvestItem player;

    [SerializeField] AudioManager m_audioManager;

    [SerializeField] AudioSource m_audioSource;

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
        //craft hache
        if (Inventory.m_amountBaton >= 1 && Inventory.m_amountMrcFer >= 1 && CraftSystem.m_itemType == Item.ItemType.hache)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.hache, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.mrcFer, amount = 1 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft torche
        else if (Inventory.m_amountTissu >= 1 && Inventory.m_amountBaton >= 1 && CraftSystem.m_itemType == Item.ItemType.allumette)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.allumette, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.tissu, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });

            m_questManager.m_craftToolDone = true;
            if (!m_questManager.m_destroyPlantDone)
            {
                m_questSystem.ChangeQuest("Brulez les plantes.");

            }
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft hache pierre
        else if (Inventory.m_amountBaton >= 1 && Inventory.m_amountCaillou >= 2 && CraftSystem.m_itemType == Item.ItemType.hache_pierre)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.hache_pierre, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.caillou, amount = 2 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        //craft echelle
        else if (Inventory.m_amountBaton >= 4)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.echelle, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.baton, amount = 4 });
            m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

        }
        else
        {
            RefreshInventoryRessources();
            RefreshInventoryTools();
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
        Debug.Log(itemTypeToDrop);
        Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
        inventory.RemoveTools(new Item { itemType = itemTypeToDrop, amount = p_amount });
        ItemWorld.DropItem(player.GetPosition(), duplicateItem);
        UpdateAmountItems();
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
        if(m_craftSystem.m_craftSlotList.Count > 0)
        {
            foreach (Transform craftSlot in m_craftSlot)
            {
                m_craftSystem.m_craftSlotList.Clear();
                m_craftSystem.NotEnoughItemToCraft();
                Destroy(craftSlot.transform.GetChild(2).gameObject);

            }
        }
        
        UpdateAmountItems();
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
