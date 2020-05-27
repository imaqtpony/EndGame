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

    [SerializeField] GameObject m_toolsWind;
    [SerializeField] GameObject m_toolsButton;

    public List<Transform> m_itemForCraft;
    public static bool m_firstToolsCrafted;

    public IntVar m_inventorySpace;
    [SerializeField] TextMeshProUGUI m_amountItemsInventory;

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

    //private void OnEnable()
    //{
    //    RefreshInventoryRessources();
    //    RemoveItemFromCraftSlot();
    //}

    private IEnumerator InventoryFullNotification()
    {
        m_notification.SetActive(true);
        m_textNotification.text = "Inventaire Plein !";
        m_textNotification.color = new Color(255, 75, 0);

        m_amountItemsInventory.GetComponent<Animator>().SetTrigger("Activate");
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
                //if(Inventory.itemList.Count + Inventory.toolsList.Count < m_inventorySpace.Value)
                //{
                    CraftTools();
                //UpdateAmountItems();
                //RemoveItemFromCraftSlot();

                //if (!m_firstToolsCrafted)
                //{
                //    m_toolsButton.SetActive(true);
                //    m_toolsWind.SetActive(true);
                //    m_toolsWind.GetComponent<Animator>().SetTrigger("OpenTools");
                //    m_firstToolsCrafted = true;
                //}
                //    }
                //    else
                //    {
                //        StartCoroutine(InventoryFullNotification());
                //    }

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

    /// <summary>
    /// craft and decraft tools checker
    /// </summary>
    public void CraftTools()
    {
        //craft hache en fer
        if (Inventory.m_amountBaton >= 2 && Inventory.m_amountMrcFer >= 1 && CraftSystem.m_itemType_1 == Item.ItemType.hache)
        {
            Item hache = new Item { itemType = Item.ItemType.hache, amount = 1 };
            Item composHache1 = new Item { itemType = Item.ItemType.baton, amount = 2 };
            Item composHache2 = new Item { itemType = Item.ItemType.mrcFer, amount = 1 };

            inventory.AddTools(hache);
            inventory.RemoveItem(composHache1);
            inventory.RemoveItem(composHache2);

            //if he has enough room
            if (!IsInventoryFull(hache, composHache1, composHache2, false))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }
        }
        //craft allumette
        else if (Inventory.m_amountPoudre >= 1 && Inventory.m_amountBaton >= 1 && CraftSystem.m_itemType_1 == Item.ItemType.allumette)
        {
            Item allumette = new Item { itemType = Item.ItemType.allumette, amount = 1 };
            Item composAllu1 = new Item { itemType = Item.ItemType.poudre, amount = 1 };
            Item composAllu2 = new Item { itemType = Item.ItemType.baton, amount = 1 };

            inventory.AddTools(allumette);
            inventory.RemoveItem(composAllu1);
            inventory.RemoveItem(composAllu2);
            
            //if he has enough room
            if (!IsInventoryFull(allumette, composAllu1, composAllu2, false))
            {
                m_questManager.m_craftToolDone = true;
                if (!m_questManager.m_destroyPlantDone)
                {
                    m_questSystem.ChangeQuest("Brulez les plantes");

                }
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            }
            else
            {
                //play fail craft sound here
            }
        }
        //craft hache en pierre
        else if (Inventory.m_amountBaton >= 1 && Inventory.m_amountCaillou >= 2 && CraftSystem.m_itemType_1 == Item.ItemType.hache_pierre)
        {
            Item hache_Pierre = new Item { itemType = Item.ItemType.hache_pierre, amount = 1 };
            Item composHacheP1 = new Item { itemType = Item.ItemType.baton, amount = 1 };
            Item composHacheP2 = new Item { itemType = Item.ItemType.caillou, amount = 2 };

            inventory.AddTools(hache_Pierre);
            inventory.RemoveItem(composHacheP1);
            inventory.RemoveItem(composHacheP2);

            //if he has enough room
            if (!IsInventoryFull(hache_Pierre, composHacheP1, composHacheP2, false))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }
        }
        //craft echelle
        else if (Inventory.m_amountBaton >= 4 && m_craftSystem.m_craftSlotList.Count == 1 && CraftSystem.m_itemType_1 == Item.ItemType.echelle)
        {
            Item echelle = new Item { itemType = Item.ItemType.echelle, amount = 1 };
            Item composEchelle = new Item { itemType = Item.ItemType.baton, amount = 4 };

            inventory.AddTools(echelle);
            inventory.RemoveItem(composEchelle);

            //if he has enough room
            if (!IsInventoryFull(echelle, composEchelle, null, false))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            } else
            {
                //play fail craft sound here
            }

        }

        //decraft allumette
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.tissu)
        {
            Item allumette = new Item { itemType = Item.ItemType.allumette, amount = 1 };
            Item composAllu1 = new Item { itemType = Item.ItemType.poudre, amount = 1 };
            Item composAllu2 = new Item { itemType = Item.ItemType.baton, amount = 1 };

            inventory.RemoveTools(allumette);
            inventory.AddItem(composAllu2);
            inventory.AddItem(composAllu1);

            //if he has enough room
            if (!IsInventoryFull(allumette, composAllu1, composAllu2, true))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }

        }
        //decraft hache en pierre
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.caillou)
        {
            Item hache_Pierre = new Item { itemType = Item.ItemType.hache_pierre, amount = 1 };
            Item composHacheP1 = new Item { itemType = Item.ItemType.baton, amount = 1 };
            Item composHacheP2 = new Item { itemType = Item.ItemType.caillou, amount = 2 };

            inventory.RemoveTools(hache_Pierre);
            inventory.AddItem(composHacheP1);
            inventory.AddItem(composHacheP2);

            //if he has enough room
            if (!IsInventoryFull(hache_Pierre, composHacheP1, composHacheP2, true))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }

        }
        //decraft hache
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.mrcFer)
        {
            Item hache = new Item { itemType = Item.ItemType.hache, amount = 1 };
            Item composHache1 = new Item { itemType = Item.ItemType.baton, amount = 2 };
            Item composHache2 = new Item { itemType = Item.ItemType.mrcFer, amount = 1 };

            inventory.RemoveTools(hache);
            inventory.AddItem(composHache1);
            inventory.AddItem(composHache2);

            //if he has enough room
            if (!IsInventoryFull(hache, composHache1, composHache2, true))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }

        }
        //decraft echelle
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton)
        {
            Item echelle = new Item { itemType = Item.ItemType.echelle, amount = 1 };
            Item composEchelle = new Item { itemType = Item.ItemType.baton, amount = 4 };

            inventory.RemoveTools(echelle);
            inventory.AddItem(composEchelle);

            //if he has enough room
            if (!IsInventoryFull(echelle, composEchelle, null, true))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }

        }
        //pas assez de ressources
        else
        {
            RefreshInventoryRessources();
            RefreshInventoryTools();
            m_notification.SetActive(true);
            m_textNotification.text = "Ressources insuffisantes";
            m_textNotification.color = new Color(255, 75, 0);
        }

    }

    private bool IsInventoryFull(Item p_tool, Item p_mat1, Item p_mat2, bool p_isDecraft)
    {
        if (Inventory.itemList.Count + Inventory.toolsList.Count <= m_inventorySpace.Value)
        {
            // c'est bon y'a la place
            UpdateAmountItems();
            RemoveItemFromCraftSlot();

            if (!m_firstToolsCrafted)
            {
                m_toolsButton.SetActive(true);
                m_toolsWind.SetActive(true);
                m_toolsWind.GetComponent<Animator>().SetTrigger("OpenTools");
                m_firstToolsCrafted = true;
            }

            return false;

        } else
        {
            StartCoroutine(InventoryFullNotification());

            if (!p_isDecraft)
            {
                inventory.AddItem(p_mat1);
                if (p_mat2 != null)
                    inventory.AddItem(p_mat2);
                inventory.RemoveTools(p_tool);
            } else
            {
                //inverse cause its a decraft
                inventory.AddTools(p_tool);
                inventory.RemoveItem(p_mat1);
                inventory.RemoveItem(p_mat2);

                RemoveItemFromCraftSlot();
            }


            return true;

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

        foreach (Item item in inventory.GetToolsList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

        }

        inventory.RemoveAllItems();
        inventory.RemoveAllTools();
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
        inventory.AddItem(new Item { itemType = p_itemType, amount = p_amount });
        UpdateAmountItems();

    }

}
