using System;
using UnityEngine;

namespace GameRPG
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private ToolInteractManager toolInteractManager;

        public static event Action<Item_SO, int> LootDataItem;
        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {

            if (PlayerInputManager.Instance.TryToInteract)
            {
                toolInteractManager.UseCurrentTool();
            }
            else
            {
                player.Anim.SetBool("chop", false);
                player.Anim.SetBool("hoe", false);
            }

            if (PlayerInputManager.Instance.TryPickUp)
            {
                HandlePickUpInput();
            }
        }

        public void EnableInteractColision()
        {
            if (player == null || player.PlayerEquipmentController == null) return;

            var currentEquipment = player.PlayerEquipmentController.CurrentEquippment;

            if (currentEquipment != null)
            {
                currentEquipment.EnableCollider();
                currentEquipment.SetIsInteractable();
                currentEquipment.SetIsNotAttack();
            }
        }

        public void DisableInteractColision()
        {
            if (player == null || player.PlayerEquipmentController == null) return;

            var currentEquipment = player.PlayerEquipmentController.CurrentEquippment;
            
            if(currentEquipment != null)
            {
                currentEquipment.FullReset();
            }
        }

        private void HandlePickUpInput()
        {
            float pickUpRadius = 1.5f; 
            LayerMask itemLayer = LayerMask.GetMask("Items"); 

            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, itemLayer);

            foreach (var col in items)
            {
                if (col.TryGetComponent<LootItem>(out LootItem loot))
                {
                    LootDataItem?.Invoke(loot.GetItemData(), loot.GetQuantity());

                    loot.Picked();
                }
            }
        }
    }
}
