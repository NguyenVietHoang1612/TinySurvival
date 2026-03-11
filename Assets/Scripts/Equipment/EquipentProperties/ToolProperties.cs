using System;
using UnityEngine;


namespace GameRPG
{
    public class ToolProperties : EquipmentProperties
    {
        [SerializeField] private int damageAttack;
        [SerializeField] private int speedInteract;

        public override void ProcessCollision(GameObject target)
        {
            if (isInteractable)
            {
                Debug.Log("Collided Interact with: " + target.name);
                IInteractable interactable = target.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    hasHitSomething = true;
                    interactable.Interact(speedInteract, player.transform);
                    return;
                }
            }

            if (isAttack)
            {
                Debug.Log("Collided Attack with: " + target.name);
                IDamageable damageable = target.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    hasHitSomething = true;
                    damageable.TakeDamage(damageAttack);
                }
            }
        }

        public void SetDamageAttack(int damage) 
        { 
            damageAttack = damage; 
        }

        public void SetSpeedInteract(int speed) 
        { 
            speedInteract = speed;
        }
    }
}
