using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameRPG
{
    public class InventorySlots : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int iD;
        [SerializeField] private Item_SO item_SO;
        [SerializeField] private Image imageIteam;
        [SerializeField] private TMP_Text quantityItem;
        [SerializeField] private GameObject borderSlot;

        [SerializeField] InventoryManager inventoryManager;

        private Player player;

        private int currentDurability;

        private int currentQuantity;
        public int Quantity
        {
            get => currentQuantity;
            private set => currentQuantity = value;
        }

        public static event Action<BuildingData> OnBuildingItemClicked;
        public static event Action<ConsumableData> OnEatConsumableItem;

        private void Start()
        {
            inventoryManager = GetComponentInParent<InventoryManager>();
            player = FindFirstObjectByType<Player>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (item_SO == null) return;

                inventoryManager.DropItemInInventorySlot(this, RandomPositionItemDropPlayer());
            }

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (item_SO == null) return;

                if (item_SO.itemType == ItemType.ConsumableCraftItem)
                {
                    OnEatConsumableItem?.Invoke(item_SO as ConsumableData);
                    inventoryManager.RemoveItem(item_SO, 1);
                    return;
                }
                else if (item_SO.itemType == ItemType.Building)
                {
                    if (item_SO is BuildingData buildingData)
                    {
                        OnBuildingItemClicked?.Invoke(buildingData);
                    }
                    
                    return;
                }

                GameEvents.RequestEquip(item_SO);
                UpdateUI();
            }
        }

        public void OnEnableBoder()
        {
            borderSlot.SetActive(true);
        }

        public void OnDisableBoder()
        {
            borderSlot.SetActive(false);
        }

        public void UpdateUI()
        {
            if (item_SO != null && item_SO.isStackable && currentQuantity > 0)
            {
                imageIteam.sprite = item_SO.itemIcon;
                imageIteam.gameObject.SetActive(true);
                quantityItem.text = currentQuantity.ToString();
            }
            else if (item_SO != null && !item_SO.isStackable && currentQuantity > 0)
            {
                imageIteam.sprite = item_SO.itemIcon;
                imageIteam.gameObject.SetActive(true);
                quantityItem.text = "";
            }
            else
            {
                item_SO = null;
                imageIteam.gameObject.SetActive(false);
                quantityItem.text = "";
            }
        }

        public Item_SO GetItemInSlot()
        {
            if (item_SO == null) return null;

            return item_SO;
        }


        private Vector2 RandomPositionItemDropPlayer()
        {
            float randomX = UnityEngine.Random.Range(player.transform.position.x - 0.5f, player.transform.position.x + 0.5f);
            float randomY = UnityEngine.Random.Range(player.transform.position.y - 0.5f, player.transform.position.y + 0.5f);

            Vector2 ranDomPositionDropup = new Vector2(randomX, randomY);

            return ranDomPositionDropup;
        }

        public int ID
        {
            get => iD;
            private set => iD = value;
        }

        public void SetID(int id)
        {
            iD = id;
        }

        public Item_SO Item
        {
            get => item_SO;
            private set => item_SO = value;
        }

        public void SetItem(Item_SO item)
        {
            item_SO = item;
        }



        public void SetQuantity(int quantity)
        {
            currentQuantity = quantity;
            UpdateUI();
        }
        public void AddQuantity(int quantity)
        {
            currentQuantity += quantity;
            UpdateUI();
        }
        public void RemoveQuantity(int quantity)
        {
            currentQuantity -= quantity;
            UpdateUI();
        }

        public void ClearSlot()
        {
            Item = null; 
            currentQuantity = 0;
            iD = -1;
        }
    }


}
