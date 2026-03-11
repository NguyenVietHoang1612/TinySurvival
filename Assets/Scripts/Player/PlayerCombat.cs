using UnityEngine;

namespace GameRPG
{
    public class PlayerCombat : MonoBehaviour
    {
        private Player player;
        [SerializeField] private HitboxBroadcaster unarmedHitbox;

        private void Start()
        {
            player = GetComponent<Player>();
            unarmedHitbox = GetComponentInChildren<HitboxBroadcaster>();
            if (unarmedHitbox != null)
            {
                unarmedHitbox.GetComponent<Collider2D>().enabled = false;
            }
        }

        public void EnableColision()
        {
            var equipment = player.PlayerEquipmentController.CurrentEquippment;
            if (equipment != null)
            {
                equipment.EnableCollider();
                equipment.SetIsAttack();
            }
            else
            {
                unarmedHitbox.EnableHitbox();
            }
        }

        public void DisableColision()
        {
            var equipment = player.PlayerEquipmentController.CurrentEquippment;
            if (equipment != null)
            {
                equipment.FullReset();        
            }
            else
            {
                unarmedHitbox.DisableHitbox();
            }
        }
    }
}
