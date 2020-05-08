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

    private Player player;

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
        foreach (Transform child in m_ressourcesSlotContainer)
        {
            if (child == m_ressourcesSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 105f;

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
        //craft losange
        if (Inventory.m_amountCircle >= 4 && Inventory.m_amountTriangle >= 1 && CraftSystem.m_itemType == Item.ItemType.losange)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.losange, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.circle, amount = 4 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.triangle, amount = 1 });

        }
        if (Inventory.m_amountSquare >= 1 && Inventory.m_amountTriangle >= 1 && CraftSystem.m_itemType == Item.ItemType.squarangle)
        {
            inventory.AddTools(new Item { itemType = Item.ItemType.squarangle, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.square, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.triangle, amount = 1 });

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

}
