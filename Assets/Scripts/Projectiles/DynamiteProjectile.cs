using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    public class DynamiteProjectile : Projectile
    {
        [SerializeField] private float rotate = 2f;
        private bool isExplosion = false;

        [SerializeField] float timeExplotion = 4f;
        [SerializeField] float radius = 0.8f;

        [SerializeField] private AudioClip dynamiteExplosions;
        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(PlayEffect());
        }


        private void Explode()
        {
            if (isExplosion) return;

            if (RB != null) RB.linearVelocity = Vector2.zero;

            isExplosion = true;
            Animator.SetBool("isExplosions", isExplosion);
            AudioManager.Instance.PlaySFX(dynamiteExplosions, 0.2f);
            AddEffect();
            StartCoroutine(DisableObject());
        }

        IEnumerator PlayEffect()
        {
            yield return new WaitForSeconds(timeExplotion);
            if (!isExplosion)
            {
                Explode();
            }
        }
        public void AddEffect()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerPlayer);

            if (colliders.Length > 0)
            {
                HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
                foreach (var collider in colliders)
                {
                    if (collider != null)
                    {
                        if(collider.TryGetComponent<IDamageable>(out IDamageable damageable)) 
                        {
                            damageable.TakeDamage(damagePlayer);
                            Debug.Log("Hit collider: " + collider.name);
                        }
                        else
                        {
                            Debug.Log("Player not take damage");
                        }
                    }
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (isExplosion) return;

            if (collision.gameObject.CompareTag("Player"))
            {
                Explode();
            }
        }

        IEnumerator DisableObject()
        {
            yield return new WaitForSeconds(0.4f);
            isExplosion = false;

            Animator.SetBool("isReset", true);
            Animator.SetBool("isExplosions", false);
            GetComponent<PoolObject>().ReturnToPool();
        }
    }
}
