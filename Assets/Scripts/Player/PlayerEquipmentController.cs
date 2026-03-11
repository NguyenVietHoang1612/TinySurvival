using GameRPG.Assets.Scripts.Interface;
using UnityEngine;

namespace GameRPG
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField] private EquipmentManager equipmentManager;

        private EquipmentProperties currentEquippment;

        public EquipmentProperties CurrentEquippment => currentEquippment;

        [SerializeField] Transform equipmentSlot;

        private PlayerStatsManager playerStatsManager;

        private void Start()
        {
            playerStatsManager = GetComponent<PlayerStatsManager>();
        }

        private void OnEnable()
        {
            equipmentManager.UpdateVisualItem += SpawnWeapon;
            equipmentManager.OnEquipmentChange += DestroyVisual;
        }

        private void OnDisable()
        {
            equipmentManager.UpdateVisualItem -= SpawnWeapon;
            equipmentManager.OnEquipmentChange -= DestroyVisual;
        }

        public void SpawnWeapon(Item_SO newItem)
        {
            Debug.Log("Vũ khí đang được Spawn lại!");
            if (equipmentSlot.childCount > 0)
            {
                foreach (Transform slot in equipmentSlot)
                {
                    Destroy(slot.gameObject);
                }
            }

            if (newItem is IEquippable visualItem)
            {
                GameObject equipItem = Instantiate(visualItem.VisualEquipmentPrefab);

                equipItem.transform.SetParent(equipmentSlot);
                equipItem.transform.localPosition = Vector3.zero;
                equipItem.transform.localRotation = Quaternion.identity;
                currentEquippment = equipItem.GetComponent<EquipmentProperties>();

                if (currentEquippment != null)
                {
                    if (currentEquippment is ToolProperties tool)
                    {
                        tool.SetDamageAttack(playerStatsManager.TotalPhysicDamage);
                        tool.SetSpeedInteract(playerStatsManager.TotalSpeedInteract);
                    }
                    else if (currentEquippment is WeaponProperties weapon)
                    {
                        weapon.SetPhysicalDamage(playerStatsManager.TotalPhysicDamage);
                        weapon.SetMagicDamage(playerStatsManager.TotalMagicDamage);
                    }
                } 
            }
        }

        public void DestroyVisual()
        {
            if (equipmentSlot.childCount > 0)
            {
                foreach (Transform slot in equipmentSlot)
                {
                    Destroy(slot.gameObject);
                }
            }
        }
    }
}
