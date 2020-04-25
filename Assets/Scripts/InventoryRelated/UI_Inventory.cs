using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventoryNS.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    [Header("Elements needed to be found")]
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Transform craftItemSlot;
    private Transform craftResult;

    [SerializeField] Transform m_craftSlot_1;
    [SerializeField] Transform m_craftSlot_2;


    [SerializeField] CraftSystem m_craftSystem;


    private Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlot");
        craftItemSlot = transform.Find("CraftItemSlot");
        craftResult = craftItemSlot.Find("CraftResult");
        
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();

    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;

        foreach(Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);


            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                //use item
                inventory.UseItem(item);
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                //drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            craftResult.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if (m_craftSystem.m_craftActive)
                {
                    craftLosange();
                }
                

            };

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
            if (x > 4)
            {
                x = 0;
                y--;
            }
        }

    }

    public void DropItemFunction()
    {
        foreach (Item item in inventory.GetItemList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            inventory.RemoveItem(item);
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
        }
    }

    private void craftLosange()
    {
        inventory.AddItem(new Item { itemType = Item.ItemType.Item4, amount = 1 });
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Item1, amount = 1 });
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Item3, amount = 1 });
        Destroy(m_craftSlot_1.transform.GetChild(2).gameObject);
        Destroy(m_craftSlot_2.transform.GetChild(2).gameObject);
        m_craftSystem.m_craftSlotList.Clear();
        m_craftSystem.NotEnoughItemToCraft();
    }

}
