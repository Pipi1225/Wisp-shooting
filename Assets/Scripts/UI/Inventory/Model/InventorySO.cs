using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int size {get; private set; } = 15;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();

            for (int i = 0; i < size; i++)
            {
                inventoryItems.Add(InventoryItem.getEmptyItem());
            }
        }

        public void addItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < size; i++)
            {
                if (inventoryItems[i].empty)
                {
                    inventoryItems[i] = new InventoryItem
                    {
                        item = item,
                        quantity = quantity,
                    };
                }
            }
        }

        public Dictionary<int, InventoryItem> getCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> value = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < size; i++)
            {
                if (!inventoryItems[i].empty)
                {
                    value[i] = inventoryItems[i];
                }
            }

            return value;
        }

        public InventoryItem getItemAt(int item_index)
        {
            return inventoryItems[item_index];
        }

        public bool lostItem(int item_index)
        {
            InventoryItem obj = inventoryItems[item_index];

            if (obj.empty || obj.quantity <= 0)
            {
                return false;
            }

            obj.quantity -= 1;

            inventoryItems[item_index] = obj;
            informAboutChanges();
            return true;
        }
        
        public void buyItem(InventoryItem boughtItem, int quantity = 1)
        {
            Debug.Log("Buying item: " + boughtItem.item.itemName + " x" + quantity);

            for (int i = 0; i < size; i++)
            {
                InventoryItem obj = inventoryItems[i];

                if (!obj.empty && obj.item == boughtItem.item && obj.item.stackable)
                {
                    obj.quantity += quantity;
                    inventoryItems[i] = obj;
                    informAboutChanges();
                    return;
                }

                if (obj.empty)
                {
                    inventoryItems[i] = boughtItem.changeQuantity(quantity);
                    informAboutChanges();
                    return;
                }
            }
        }

        private void informAboutChanges()
        {
            OnInventoryUpdated?.Invoke(getCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool empty => item == null;

        public InventoryItem changeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem getEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    };
}
