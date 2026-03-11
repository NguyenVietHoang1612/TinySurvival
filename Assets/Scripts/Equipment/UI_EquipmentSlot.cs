using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameRPG
{
    public class UI_EquipmentSlot : MonoBehaviour, IPointerClickHandler
    {
        public Item_SO item_SO;
        public Image equipmentPanel;

        public Image itemIcon;

        public ItemType[] typeItem;

        [SerializeField] EquipmentManager EquipmentManager;

        private void Start()
        {
            EquipmentManager.OnEquipmentUpdated += RefreshSlot;
            EquipmentManager.OnEquipmentChange += UpdateUI;

        }

        private void OnDisable()
        {
            EquipmentManager.OnEquipmentUpdated -= RefreshSlot;
            EquipmentManager.OnEquipmentChange -= UpdateUI;
        }


        public void UpdateUI()
        {
            if (item_SO != null)
            {
                itemIcon.sprite = item_SO.itemIcon;
                equipmentPanel.gameObject.SetActive(false);
                itemIcon.gameObject.SetActive(true);
            }
            else
            {
                itemIcon.gameObject.SetActive(false);
                equipmentPanel.gameObject.SetActive(true);

            }
        }

        private void RefreshSlot(ItemType type, Item_SO item)
        {
            foreach (var targetType in typeItem)
            {
                if (type == targetType)
                {
                    item_SO = item;
                    UpdateUI();
                }
            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Right)
            {

                if (item_SO == null) return;

                EquipmentManager.UnequipItem(item_SO.itemType);
                item_SO = null;
                UpdateUI();
            }
        }

    }
}
