using GameRPG.Assets.Scripts.Interface;
using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace GameRPG
{
    public class EquipmentManager : MonoBehaviour
    {
        private Dictionary<ItemType, Item_SO> equippedItems = new();

        private IInventory inventory;

        public event Action<ItemType, Item_SO> OnEquipmentUpdated;
        public event Action OnEquipmentChange;
        public event Action<Item_SO> UpdateVisualItem;

        private void Start()
        {
            inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<IInventory>();

            if (inventory == null)
            {
                Debug.LogError("Không tìm thấy implementation của IInventory trong RecipeItemSlot!");
            }


            GameEvents.OnItemEquipRequested += EquipItem;
        }

        private void OnDisable()
        {
            GameEvents.OnItemEquipRequested -= EquipItem;
        }

        public void EquipItem(Item_SO newItem)
        {
            ItemType type = newItem.itemType;
            Item_SO oldItem = null;

            if (equippedItems.ContainsKey(type))
            {
                oldItem = equippedItems[type];
            }

            inventory.RemoveItem(newItem, 1);
            if (oldItem != null)
            {
                inventory.AddItem(oldItem, 1);
            }

            equippedItems[type] = newItem;

            OnEquipmentChange?.Invoke();
            OnEquipmentUpdated?.Invoke(newItem.itemType, newItem);
            UpdateVisualItem?.Invoke(newItem);
            
        }

        public void UnequipItem(ItemType type)
        {
            if (equippedItems.TryGetValue(type, out Item_SO itemToUnequip))
            {
                inventory.AddItem(itemToUnequip, 1);
                equippedItems.Remove(type);

                OnEquipmentUpdated?.Invoke(type, itemToUnequip);
                OnEquipmentChange?.Invoke();
                UpdateVisualItem?.Invoke(null);
            }
        }

        public Item_SO GetItemInSlot(ItemType type)
        {
            return equippedItems.GetValueOrDefault(type);
        }
    }
}
