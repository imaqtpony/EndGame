using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventoryNS.Utils;
using GD2Lib;

/// <summary>
/// Manage the Interface of the inventory, how items interact with others 
/// Works with HarvestItems and Inventory scripts
/// </summary>
public class UI_Inventory : MonoBehaviour
{
    #region INITIALIZING VARIABLES
    [Header("RELATED TO THE INVENTORY")]
    private Inventory inventory;
    public IntVar m_inventorySpace;
    [SerializeField] TextMeshProUGUI m_amountItemsInventory;

    [Header("QUEST SYSTEM")]
    [SerializeField] QuestSystem m_questSystem;
    [SerializeField] QuestManager m_questManager;


    [Header("UI ELEMENTS FOR ITEMS SLOTS")]
    [SerializeField] Transform m_ressourcesSlotContainer;
    [SerializeField] Transform m_ressourcesSlotTemplate;
    [SerializeField] Transform m_toolsWindow;
    [SerializeField] Transform m_toolsSlotContainer;
    [SerializeField] Transform m_toolsSlotTemplate;
    [SerializeField] Transform m_craftResult;
    [SerializeField] List<Transform> m_craftSlot;

    [Header("USE THOSE SCRIPTS")]
    [SerializeField] CraftSystem m_craftSystem;
    private HarvestItem player;

    [Header("NOTIFICATIONS ON THE SCREEN")]
    [SerializeField] GameObject m_notification;
    [SerializeField] TextMeshProUGUI m_textNotification;

    [Header("RELATED TO THE TOOLS SECTION")]
    [SerializeField] GameObject m_toolsWind;
    [SerializeField] GameObject m_toolsButton;

    public List<Transform> m_slotsForCraft;

    [Header("TUTO FOR THE FIRST TOOL CRAFTED")]
    public static bool m_firstToolsCrafted;

    [Header("RELATED TO THE SOUND")]
    [SerializeField] AudioManager m_audioManager;
    [SerializeField] AudioSource m_audioSource;
    #endregion


    public void SetPlayer(HarvestItem player)
    {
        this.player = player;
    }

    private void Awake()
    {
        m_audioManager.m_audioSource = GetComponent<AudioSource>();
        m_firstToolsCrafted = false;
        m_toolsButton.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inventory">We call the inventory here to manage it in the UI</param>
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        //We use delegate to have best performance, instead of using Update for the items list
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        inventory.OnToolsListChanged += Inventory_OnToolsListChanged;
        RefreshInventoryRessources();
        RefreshInventoryTools();
    }

    /// <summary>
    /// List of the ressource
    /// </summary>
    /// <param name="sender">We use event system for this</param>
    /// <param name="e">name of the event</param>
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryRessources();

    }

    /// <summary>
    /// List of the tools
    /// </summary>
    /// <param name="sender">We use event system for this too</param>
    /// <param name="e">name of the event</param>
    private void Inventory_OnToolsListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryTools();

    }

    private void OnEnable()
    {
        RefreshInventoryRessources();
        RefreshInventoryTools();
        RemoveItemFromCraftSlot();
    }

    #region INVENTORY UI RELATED
    /// <summary>
    /// The coroutine is very powerful because we don't use update to do the same thing
    /// </summary>
    /// <returns>we interrupt the coroutine and wait for 1s and we continue to process it after</returns>
    private IEnumerator InventoryFullNotification()
    {
        //We set active the notification in first to display it
        m_notification.SetActive(true);

        //We set the text
        m_textNotification.text = "Inventaire Plein !";

        //We set the color
        m_textNotification.color = new Color(255, 75, 0);

        //We remove the items in the craft slot because we can't craft because our inventory is full
        RemoveItemFromCraftSlot();

        //We get the Animator of the Game Object m_amountItemsInventory to activate the trigger "Activate"
        //which changes the text color from red to white
        m_amountItemsInventory.GetComponent<Animator>().SetTrigger("Activate");

        //We wait 1s
        yield return new WaitForSeconds(1);

        //We reset the trigger at its initial state
        m_amountItemsInventory.GetComponent<Animator>().SetTrigger("DeActivate");

    }

    /// <summary>
    /// Refresh teh inventory of the ressources
    /// </summary>
    public void RefreshInventoryRessources()
    {
        //We regroup all the slot of the ressources
        foreach (Transform child in m_ressourcesSlotContainer)
        {
            //We use a template to duplicate it to have more items, so make sure to 
            //destroy the child object in the container except the template
            if (child == m_ressourcesSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int SPACE_BETWEEN_SLOTS_IN_X = 0;
        int SPACE_BETWEEN_SLOTS_IN_Y = 0;

        float itemSlotCellSize = 105f;
        
        //When we click on the craft button
        m_craftResult.GetComponent<Button_UI>().ClickFunc = () =>
        {
            //we check if the items placed on the craft slots 
            //are going to craft something
            if (m_craftSystem.m_craftActive)
            {
                //Then we craft
                CraftTools();

            }

        };

        //When we harvest an item
        foreach (Item item in inventory.GetItemList())
        {
            //We duplicate the template tha we refer earlier
            RectTransform itemSlotRectTransform = Instantiate(m_ressourcesSlotTemplate, m_ressourcesSlotContainer).GetComponent<RectTransform>();

            //And we set it active because the template is disabled so the new slot will be disabled too
            itemSlotRectTransform.gameObject.SetActive(true);

            //We set the space between each slot
            itemSlotRectTransform.anchoredPosition = new Vector2(SPACE_BETWEEN_SLOTS_IN_X * itemSlotCellSize, SPACE_BETWEEN_SLOTS_IN_Y * itemSlotCellSize);

            //We get the image of the item 
            Image image = itemSlotRectTransform.Find("Item").GetComponent<Image>();

            //We set the sprite according to its type
            image.sprite = item.GetSprite();

            //And the amonut of course
            TextMeshProUGUI uiText = image.transform.Find("AmountText").GetComponent<TextMeshProUGUI>();

            //If the amount > 1, we display it
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            //Else, we don't
            else
            {
                uiText.SetText("");

            }

            //And if we reach 3 items on the same line, we go under
            SPACE_BETWEEN_SLOTS_IN_X++;
            if (SPACE_BETWEEN_SLOTS_IN_X > 2)
            {
                SPACE_BETWEEN_SLOTS_IN_X = 0;
                SPACE_BETWEEN_SLOTS_IN_Y--;
            }
        }
    }

    /// <summary>
    /// Same function as above but with the tools
    /// </summary>
    public void RefreshInventoryTools()
    {
        foreach (Transform child in m_toolsSlotContainer)
        {
            if (child == m_toolsSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int SPACE_BETWEEN_SLOTS_IN_X = 0;
        int SPACE_BETWEEN_SLOTS_IN_Y = 0;
        float itemSlotCellSize = 100f;


        foreach (Item item in inventory.GetToolsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(m_toolsSlotTemplate, m_toolsSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(SPACE_BETWEEN_SLOTS_IN_X * itemSlotCellSize, SPACE_BETWEEN_SLOTS_IN_Y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Tools").GetComponent<Image>();

            image.sprite = item.GetSprite();

            SPACE_BETWEEN_SLOTS_IN_X++;
            if (SPACE_BETWEEN_SLOTS_IN_X > 0)
            {
                SPACE_BETWEEN_SLOTS_IN_X = 0;
                SPACE_BETWEEN_SLOTS_IN_Y--;
            }
        }
    }
    #endregion

    #region CRAFT RELATED
    /// <summary>
    /// Craft and decraft tools checker
    /// </summary>
    public void CraftTools()
    {
        //craft the iron axe
        //we check the amount of items before crafting
        if (Inventory.m_amountBaton >= 2 && Inventory.m_amountMrcFer >= 1 && CraftSystem.m_itemType_1 == Item.ItemType.hache)
        {
            //The tool added in our inventory
            Item hache = new Item { itemType = Item.ItemType.hache, amount = 1 };

            //And the ressources used
            Item composHache1 = new Item { itemType = Item.ItemType.baton, amount = 2 };
            Item composHache2 = new Item { itemType = Item.ItemType.mrcFer, amount = 1 };

            inventory.AddTools(hache);
            inventory.RemoveItem(composHache1);
            inventory.RemoveItem(composHache2);

            //Check the space in the inventory, if we can craft it or not
            if (!IsInventoryFull(hache, composHache1, composHache2, false))
            {
                //We play the sound of the craft action
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }
        }

        //craft match
        //Same thing for all crafting action
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
                    m_questSystem.ChangeQuest("Debarrassez-vous des plantes");

                }
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);
            }
            else
            {
                //play fail craft sound here
            }
        }

        //craft stone axe
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
        //Craft ladder
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

        //Uncraft match
        //In the CraftSystem script, we check the type of items that we will get
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.poudre)
        {
            //The tools destroyed
            Item allumette = new Item { itemType = Item.ItemType.allumette, amount = 1 };

            //And thje ressources that we will get
            Item composAllu1 = new Item { itemType = Item.ItemType.poudre, amount = 1 };
            Item composAllu2 = new Item { itemType = Item.ItemType.baton, amount = 1 };

            inventory.RemoveTools(allumette);
            inventory.AddItem(composAllu2);
            inventory.AddItem(composAllu1);

            //Check the space in the inventory, if we can craft it or not
            if (!IsInventoryFull(allumette, composAllu1, composAllu2, true))
            {
                m_audioSource.PlayOneShot(m_audioManager.m_craftingSound);

            }
            else
            {
                //play fail craft sound here
            }

        }
        //Uncraft stone axe
        //Same thing for all uncrafting action
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
        //Uncraft iron axe
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
        //Uncraft ladder
        else if (CraftSystem.m_itemType_1 == Item.ItemType.baton && CraftSystem.m_itemType_2 == Item.ItemType.plan_echelle)
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
        //We don't have enough ressources
        else
        {
            RefreshInventoryRessources();
            RefreshInventoryTools();

            RemoveItemFromCraftSlot();

            m_notification.SetActive(true);
            m_textNotification.text = "Ressources insuffisantes";
            m_textNotification.color = new Color(255, 75, 0);
        }

    }

    /// <summary>
    /// Check if we can craft or uncraft we the space in our inventory
    /// </summary>
    /// <param name="p_tool">The tool</param>
    /// <param name="p_mat1">The first component</param>
    /// <param name="p_mat2">The second component</param>
    /// <param name="p_isCraft">Check if we craft or uncraft</param>
    /// <returns></returns>
    private bool IsInventoryFull(Item p_tool, Item p_mat1, Item p_mat2, bool p_isCraft)
    {
        if (Inventory.itemList.Count + Inventory.toolsList.Count <= m_inventorySpace.Value)
        {
            //We have enough space
            UpdateAmountItems();
            RemoveItemFromCraftSlot();

            //When we craft our first tool, it activates the tools button and the tools window
            if (!m_firstToolsCrafted)
            {
                m_toolsButton.SetActive(true);
                m_toolsWind.SetActive(true);
                m_toolsWind.GetComponent<Animator>().SetTrigger("OpenTools");
                m_firstToolsCrafted = true;
            }

            //We stop
            return false;

        } else
        {
            //We play the notification "Inventory full"
            StartCoroutine(InventoryFullNotification());

            //We update the amount of different items that we have
            UpdateAmountItems();

            //if we uncraft
            if (!p_isCraft)
            {
                //We add 100% of the items taht we used previously to craft the tool
                inventory.AddItem(p_mat1);
                if (p_mat2 != null)
                    inventory.AddItem(p_mat2);

                //And remove the tools 
                inventory.RemoveTools(p_tool);
            } else
            {
                //Inverse because its a craft
                inventory.AddTools(p_tool);
                inventory.RemoveItem(p_mat1);
                if (p_mat2 != null)
                    inventory.RemoveItem(p_mat2);

                RemoveItemFromCraftSlot();
            }

            //We stop
            return true;

        }
    }
    #endregion

    #region DROP FUNCTION RELATED
    /// <summary>
    /// Drop function
    /// </summary>
    /// <param name="p_itemTypeToDrop">The item that we drop</param>
    /// <param name="p_amount">The amount</param>
    public void DropItemFunction(Item.ItemType p_itemTypeToDrop, int p_amount, bool p_isRessources)
    {
        //check if it is a ressources or not
        if (p_isRessources)
        {
            //we duplicate the item to drôp it on the ground with a physical game object
            Item duplicateItem = new Item { itemType = p_itemTypeToDrop, amount = p_amount };

            //the we remove the item from our inventory
            inventory.RemoveItem(new Item { itemType = p_itemTypeToDrop, amount = p_amount });

            //we get the position of the player then we drop the duplicated item
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
        }
        //same thing here but for the tools
        else if (!p_isRessources)
        {
            if (p_itemTypeToDrop != Item.ItemType.echelle)
            {
                Item duplicateItem = new Item { itemType = p_itemTypeToDrop, amount = p_amount };
                inventory.RemoveTools(new Item { itemType = p_itemTypeToDrop, amount = p_amount });
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                UpdateAmountItems();
            }
            //if we decide to use the ladder, we just remove it and it will be placed in the world
            else if (p_itemTypeToDrop == Item.ItemType.echelle)
            {
                inventory.RemoveTools(new Item { itemType = p_itemTypeToDrop, amount = p_amount });

            }
        }

        //then we update the space in the inventory
        UpdateAmountItems();
    }

    /// <summary>
    /// We call this function when we die be cause we drop all of our stuff on the ground
    /// </summary>
    public void DropAllItemFunction()
    {
        //we get the entire item list
        foreach (Item item in inventory.GetItemList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

        }

        //and the entire tool list
        foreach (Item item in inventory.GetToolsList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

        }

        //and we drop everything
        inventory.RemoveAllItems();
        inventory.RemoveAllTools();
        RemoveItemFromCraftSlot();
    }
    #endregion

    /// <summary>
    /// Whe nwe close, the inventory, or we don't have enough space or ressources 
    /// in inventory when we craft, it replaces the iteml on their initial slot
    /// </summary>
    public void RemoveItemFromCraftSlot()
    {
        //we check if their more than 0 item in the craft slot list
        if (m_craftSystem.m_craftSlotList.Count > 0)
        {
            //if it is, we check the number of children if the first craft slot, to check if an item is in it
            if (m_craftSlot[0].childCount > 2)
            {
                //we clear the list
                m_craftSystem.m_craftSlotList.Clear();

                //we reset the sprite of slot that shows the result of the craft
                m_craftSystem.NotEnoughItemToCraft();

                //then we destropy the item in the craft slot to avoid to duplicate it
                Destroy(m_craftSlot[0].transform.GetChild(2).gameObject);

                //then we update
                UpdateAmountItems();
            }
            //same thing for the second craft slot
            if (m_craftSlot[1].childCount > 2)
            {
                m_craftSystem.m_craftSlotList.Clear();
                m_craftSystem.NotEnoughItemToCraft();

                Destroy(m_craftSlot[1].transform.GetChild(2).gameObject);

                UpdateAmountItems();
            }
        }
        
    }

    /// <summary>
    /// Just shows the number of item out of the space in the inventory, 
    /// it's a string
    /// </summary>
    private void UpdateAmountItems()
    {

        m_amountItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count}/{m_inventorySpace.Value}";

    }

    /// <summary>
    /// This function is executed when we get some items from a chest
    /// </summary>
    /// <param name="p_itemType">item that we harvest in the chest</param>
    /// <param name="p_amount">the amount></param>
    public void ChestItem(Item.ItemType p_itemType, int p_amount)
    {
        inventory.AddItem(new Item { itemType = p_itemType, amount = p_amount });
        UpdateAmountItems();

    }

}
