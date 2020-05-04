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

    [Header("Elements needed to be found")]
    private Transform ressourcesSlotContainer;
    private Transform ressourcesSlotTemplate;
    private Transform toolsWindow;
    private Transform toolsSlotContainer;
    private Transform toolsSlotTemplate;

    private Transform craftItemSlot;
    private Transform craftResult;

    [SerializeField] List<Transform> m_craftSlot;

    [SerializeField] CraftSystem m_craftSystem;

    public List<Transform> m_itemForCraft;

    public IntVar m_inventorySpace;
    [SerializeField] TextMeshProUGUI m_amountItemsInventory;

    private Player player;

    private void Awake()
    {
        ressourcesSlotContainer = transform.Find("ItemSlotContainer");
        ressourcesSlotTemplate = ressourcesSlotContainer.Find("ItemSlot");

        craftItemSlot = transform.Find("CraftItemSlot");
        craftResult = craftItemSlot.Find("CraftResult");

        toolsWindow = transform.Find("ToolsWindow");
        toolsSlotContainer = toolsWindow.Find("ToolsSlotContainer");
        toolsSlotTemplate = toolsSlotContainer.Find("ToolsSlot");
        
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
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
        foreach (Transform child in ressourcesSlotContainer)
        {
            if (child == ressourcesSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;

        craftResult.GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (m_craftSystem.m_craftActive && Inventory.itemList.Count < m_inventorySpace.Value)
            {
                CraftTools();
                UpdateAmountItems();

            }

        };

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(ressourcesSlotTemplate, ressourcesSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Item").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
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
        foreach (Transform child in toolsSlotContainer)
        {
            if (child == toolsSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;


        foreach (Item item in inventory.GetToolsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(toolsSlotTemplate, toolsSlotContainer).GetComponent<RectTransform>();
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
        //craft losange
        if (Inventory.m_amountCircle >= 4 && Inventory.m_amountTriangle >= 1)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.losange, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.circle, amount = 4 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.triangle, amount = 1 });

            RemoveItemFromCraftSlot();
        }

    }

    public void DropItemFunction(Item.ItemType itemTypeToDrop, int p_amount)
    {
        foreach (Item item in inventory.GetItemList())
        {

            Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
            inventory.RemoveItem(new Item { itemType = itemTypeToDrop, amount = p_amount });
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

        }

    }

    public void DropToolFunction(Item.ItemType itemTypeToDrop, int p_amount)
    {
        foreach (Item item in inventory.GetToolsList())
        {

            Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
            inventory.RemoveTools(new Item { itemType = itemTypeToDrop, amount = p_amount });
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            UpdateAmountItems();

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

        foreach(Transform craftSlot in m_craftSlot)
        {
            m_craftSystem.m_craftSlotList.Clear();
            m_craftSystem.NotEnoughItemToCraft();
            Destroy(craftSlot.transform.GetChild(2).gameObject);

        }
        UpdateAmountItems();
    }

    private void UpdateAmountItems()
    {
        m_amountItemsInventory.text = $"{ Inventory.itemList.Count + Inventory.toolsList.Count}/{m_inventorySpace.Value}";

    }

}
