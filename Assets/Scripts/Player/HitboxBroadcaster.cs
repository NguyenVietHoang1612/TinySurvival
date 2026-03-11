using System;
using UnityEngine;

namespace GameRPG
{
    public class HitboxBroadcaster : MonoBehaviour
    {
        [SerializeField] private Collider2D hitboxCollider;
        [SerializeField] Player player;

        private bool hasHit;
        private void Start()
        {
            player = GetComponentInParent<Player>();
            hitboxCollider = GetComponent<Collider2D>();

            hitboxCollider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasHit) return;

            IDamageable damageable = collision.GetComponent<IDamageable>();
           
            if (damageable != null)
            {
                hasHit = true;
                int damage = player.PlayerStats.TotalPhysicDamage + player.PlayerStats.TotalMagicDamage;
                damageable.TakeDamage(damage);
            }
        } 

        public void EnableHitbox()
        {
            hasHit = false;
            hitboxCollider.enabled = true;
        }

        public void DisableHitbox() {
            hitboxCollider.enabled = false;
        }
    }
}
