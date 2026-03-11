using System;
using UnityEngine;


namespace GameRPG
{
    public class InventoryManager : MonoBehaviour, IInventory
    {
        [SerializeField] private ItemDatabaseObject itemData;

        private InventorySlots[] itemSlots;
        public InventorySlots[] ItemSlots => itemSlots;

        private bool hasLoadedInventory = false;

        private int selectedSlot = -1;

        public static event Action<Item_SO, int, Vector2> DropLootInventory;

        private void Start()
        {
            selectedSlot = 0;
            itemSlots = new InventorySlots[12];
            itemSlots = GetComponentsInChildren<InventorySlots>();
        }


        private void Update()
        {
            if (!hasLoadedInventory && SaveGameManager.Instance != null && SaveGameManager.Instance.continueButtonActive)
            {
                if (itemSlots != null && itemSlots.Length > 0 && itemSlots[0] != null)
                {
                    SaveGameManager.Instance.continueButtonActive = false;
                    PlayerData data = SaveGameManager.Instance.currentPlayerData;
                    LoadInventoryData(data);
                    hasLoadedInventory = true;
                }

                if (Input.anyKeyDown) 
                {
                    if (int.TryParse(Input.inputString, out int number))
                    {
                        if (number >= 1 && number <= 8) 
                        {
                            ChangeInventorySlot(number - 1);
                        }
                    }
                }
            }
        }

        private void OnEnable()
        {
            PlayerInteract.LootDataItem += AddItem;
        }

        private void OnDisable()
        {
            PlayerInteract.LootDataItem -= AddItem;
        }


        public void ChangeInventorySlot(int newSelectedSlot)
        {
            if (selectedSlot >= 0)
            {
                itemSlots[selectedSlot].OnDisableBoder();
            }

            itemSlots[newSelectedSlot].OnEnableBoder();

            selectedSlot = newSelectedSlot;

        }


        public bool HasItem(Item_SO itemSO, int quantity)
        {
            int totalAmount = 0;
            foreach (var slot in itemSlots)
            {
                if (slot.Item == itemSO)
                {
                    totalAmount += slot.Quantity;
                }
            }
            return totalAmount >= quantity;
        }

        public void AddItem(Item_SO itemSO, int quantity)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.Item == itemSO && slot.Quantity < slot.Item.stackSize && slot.Item.isStackable)
                {
                    int availableSpace = slot.Item.stackSize - slot.Quantity;
                    int amountToAdd = Mathf.Min(availableSpace, quantity);
                    slot.AddQuantity(amountToAdd);
                    quantity -= amountToAdd;
                    slot.UpdateUI();

                    if (quantity <= 0)
                    {
                        return;
                    }
                }
            }

            foreach (var slot in itemSlots)
            {
                if (slot.Item == null)
                {
                    int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);

                    slot.SetItem(itemSO);
                    slot.SetQuantity(amountToAdd);
                    slot.SetID(itemData.GetID[itemSO]);

                    quantity -= amountToAdd;
                    slot.UpdateUI();

                    if (quantity <= 0) return;
                }
            }

            // TODO: inventorySlot is full 
            if (quantity > 0)
            {
                
            }
        }

        public void ClearInventory()
        {
            foreach (var slot in itemSlots)
            {
                if (slot.Item != null)
                {
                    RemoveItem(slot.Item, slot.Quantity);
                }
            }
        }

        public int CountItemInInventory(Item_SO item)
        {
            int total = 0;
            foreach (var slot in itemSlots)
            {
                if (slot.Item == item)
                    total += slot.Quantity;
            }
            return total;
        }

        public void RemoveItem(Item_SO itemSO, int quantityToRemove)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.Item == itemSO)
                {
                    int removed = Mathf.Min(slot.Quantity, quantityToRemove);
                    slot.RemoveQuantity(removed);
                    quantityToRemove -= removed;

                    if (slot.Quantity <= 0)
                    {
                        slot.ClearSlot();
                    }

                    slot.UpdateUI();
                    if (quantityToRemove <= 0) break;
                }
            }
        }

        public void DropItemInInventorySlot(InventorySlots slot, Vector2 positonItemDrop)
        {
            if (slot == null || slot.Item == null) return;

            DropLootInventory?.Invoke(slot.Item, 1, positonItemDrop);

            slot.RemoveQuantity(1);

            if (slot.Quantity <= 0)
            {
                slot.ClearSlot();
            }
        }


        public Item_SO GetItemWeaponOrTool()
        {
            Item_SO itemData = itemSlots[selectedSlot].GetItemInSlot();

            if (itemData == null) return null;

            Debug.Log("Get item: " + itemData.name);

            return itemData;
        }

        public void UpdateInventoryUI()
        {
            foreach (var slot in itemSlots)
            {
                if (slot.Item == null)
                {
                    continue;
                }

                slot.UpdateUI();
            }
        }

        public void LoadInventoryData(PlayerData data)
        {
            ClearInventory();
            Debug.Log("So luong trong: " + itemSlots.Length);
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (data.slots[i] == null) continue;

                Item_SO item = itemData.GetItemByID(data.slots[i].iD);
                int quantity = data.slots[i].quantity;
                Debug.Log("Quantity: " + quantity);
                AddItem(item, quantity);
            }

            UpdateInventoryUI();
        }
    }

}
