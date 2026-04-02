using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private InventorySO shopData;
    [SerializeField] private InventorySO playerInventoryData;

    [SerializeField] private UI_Item itemPrefab;
    [SerializeField] private UI_Item_Description itemDescription;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private List<UI_Item> itemList;
    [SerializeField] private UI_Item prevItem;
    [SerializeField] private int currentItem;
    [SerializeField] private int numberOfItem;
    private bool active;
    private Animator anim;
    private bool moveable;
    [SerializeField] private int key;

    void Awake()
    {
        anim = GetComponent<Animator>();
        itemList = new List<UI_Item>();
        itemDescription.ResetDescription();

        for (int i = 0; i < numberOfItem; i++)
        {
            UI_Item obj = Instantiate(itemPrefab);
            obj.transform.SetParent(contentPanel);
            itemList.Add(obj);
            obj.OnItemClicked += HandleItemSelection;
            obj.OnRightMouseBtnClick += HandleBuyAction;
        }

        //inventoryData.Initialize();
        moveable = true;
    }

    void Update()
    {
        if (active)
        {
            getValueKey();

            if (key != 0 && moveable)
            {
                currentItem += key;
                if (currentItem < 0)
                {
                    currentItem += numberOfItem;
                }
                currentItem = currentItem % numberOfItem;

                moveable = false;
                Invoke("Cooldown", 0.15f);
                HandleItemSelection(itemList[currentItem]);
            }
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        resetAllItem();
        foreach (var item in inventoryState)
        {
            updateData(
                item.Key,
                item.Value.item.itemImage,
                item.Value.quantity,
                item.Value.item.stackable
            );
        }
    }

    private void resetAllItem()
    {
        foreach (var item in itemList)
        {
            item.resetData();
        }
    }

    private void getValueKey()
    {
        bool up, down, left, right;
        int keysPressed = 0;

        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);

        keysPressed += up ? 1 : 0;
        keysPressed += down ? 1 : 0;
        keysPressed += left ? 1 : 0;
        keysPressed += right ? 1 : 0;

        if (keysPressed >= 2)
        {
            return;
        }

        if (right)
        {
            key = 1;
        }
        else if (left)
        {
            key = -1;
        }
        else if (up)
        {
            key = -5;
        }
        else if (down)
        {
            key = 5;
        }
        else
        {
            key = 0;
        }
    }

    private void Cooldown()
    {
        moveable = true;
    }

    private void HandleItemSelection(UI_Item item)
    {
        if (prevItem != null)
        {
            prevItem.Deselect();
        }
        prevItem = item;

        currentItem = getIndex(item);
        item.Select();
        HandleDescriptionRequest(currentItem);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryData = shopData.getItemAt(itemIndex);
        if (inventoryData.empty)
        {
            itemDescription.ResetDescription();
            return;
        }

        ItemSO item = inventoryData.item;

        itemDescription.SetDescription(
            item.itemImage,
            item.itemName,
            item.description
        );
    }

    private void HandleBuyAction(UI_Item item)
    {
        
    }

    public void buyItem()
    {
        if (shopData.lostItem(currentItem))
        {
            playerInventoryData.buyItem(shopData.getItemAt(currentItem));
        }
    }

    public void Show(InventorySO shopData_NPC)
    {
        anim.SetBool("Shopping", true);
        itemDescription.ResetDescription();
        active = true;
        shopData = shopData_NPC;
        shopData.OnInventoryUpdated += UpdateInventoryUI;

        foreach (var item in shopData.getCurrentInventoryState())
        {
            updateData(
                item.Key,
                item.Value.item.itemImage,
                item.Value.quantity,
                item.Value.item.stackable
            );
        }

        currentItem = 0;
        prevItem = null;
        deselectAllItem();
        HandleItemSelection(itemList[currentItem]);
    }

    public void Hide()
    {
        anim.SetBool("Shopping", false);
        active = false;
    }

    private void deselectAllItem()
    {
        foreach (UI_Item item in itemList)
        {
            item.Deselect();
        }
    }

    private int getIndex(UI_Item obj)
    {
        for (int i = 0; i < numberOfItem; i++)
        {
            if (obj == itemList[i])
            {
                return(i);
            }
        }

        return(-1);      
    }

    private void updateData(int keyIndex, Sprite newSprite, int newQuantity, bool stackable)
    {
        if (keyIndex >= itemList.Count || keyIndex < 0)
        {
            return;
        }

        itemList[keyIndex].setData(newSprite, newQuantity, stackable);
    }
}
