using System;
using UnityEngine;


namespace GameRPG
{
    public class WeaponProperties : EquipmentProperties
    {
        private int physicalDamage;
        private int magicDamage;
        public override void ProcessCollision(GameObject target)
        {
            if (!isAttack) return; 

            IDamageable damageable = target.GetComponent<IDamageable>();

            if (damageable != null)
            {
                hasHitSomething = true;
                damageable.TakeDamage(physicalDamage + magicDamage);
            }
        }

        public void SetPhysicalDamage(int damage) 
        { 
            physicalDamage = damage;
        }

        public void SetMagicDamage(int damage) 
        { 
            magicDamage = damage;
        }
    }
}
