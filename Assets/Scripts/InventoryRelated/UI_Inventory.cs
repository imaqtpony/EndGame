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

    [SerializeField] List<Transform> m_craftSlot;


    [SerializeField] CraftSystem m_craftSystem;


    public List<Transform> m_itemForCraft;


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

    public void RefreshInventoryItems()
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
                //inventory.UseItem(item);
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
                    craftLosange(item);

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
            if (x > 2)
            {
                x = 0;
                y--;
            }
        }


    }

    public void DropItemFunction(Item.ItemType itemTypeToDrop, int p_amount)
    {

        foreach (Item item in inventory.GetItemList())
        {

                Item duplicateItem = new Item { itemType = itemTypeToDrop, amount = p_amount };
                inventory.RemoveItem(new Item { itemType = itemTypeToDrop, amount = p_amount });
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            
        }

    }

    public void UseItemFunction(Item.ItemType p_itemTypeToUse)
    {

        inventory.UseItem(p_itemTypeToUse);

    }



    public void DropAllItemFunction()
    {
        foreach (Item item in inventory.GetItemList())
        {
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            ItemWorld.DropItem(player.GetPosition(), duplicateItem);
        }
        inventory.RemoveAllItems();
        RemoveItemFromCraftSlot();
    }

    private void craftLosange(Item item)
    {

        if(Inventory.m_amountCircle > 4)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.losange, amount = 1 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.circle, amount = 2 });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.triangle, amount = 1 });

            RemoveItemFromCraftSlot();
        }
        
        
    }

    public void RemoveItemFromCraftSlot()
    {

        foreach(Transform craftSlot in m_craftSlot)
        {
            Destroy(craftSlot.transform.GetChild(2).gameObject);
            m_craftSystem.m_craftSlotList.Clear();
            m_craftSystem.NotEnoughItemToCraft();
        }

    }

}
