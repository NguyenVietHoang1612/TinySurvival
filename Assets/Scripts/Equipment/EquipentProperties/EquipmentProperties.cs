using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    public abstract class EquipmentProperties : MonoBehaviour
    {
        protected Collider2D myCollider;
        protected Player player;

        protected bool hasHitSomething;
        protected bool isAttack;
        protected bool isInteractable;

        private void Awake()
        {
            myCollider = GetComponent<Collider2D>();  
            if (myCollider != null) myCollider.enabled = false;
        }

        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasHitSomething) return;

            ProcessCollision(collision.gameObject);
        }

        public abstract void ProcessCollision(GameObject target);

        public void EnableCollider()
        {
            hasHitSomething = false;
            myCollider.enabled = true;
        }

        public void FullReset()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            Debug.Log("Full reset: "+ t.ToString());

            isAttack = false;
            isInteractable = false;
            hasHitSomething = false;
            if (myCollider != null) myCollider.enabled = false;
        }

        public void SetIsAttack() => isAttack = true; 
        public void SetIsNotAttack() => isAttack = false;
        public void SetIsInteractable() => isInteractable = true;
        public void SetIsNotInteractable() => isInteractable = false;
    }
}
