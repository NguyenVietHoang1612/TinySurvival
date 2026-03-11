using Unity.VisualScripting;
using UnityEngine;

namespace GameRPG
{
    public class TreeStatManager : ResourceStatsManager
    {
        [SerializeField] Transform truckTree;
        [SerializeField] AudioClip treeFallingSFX;
        [SerializeField] AudioClip chopTreeSFX;
        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(int speedAttack, Transform targetTransfor)
        {
            base.Interact(speedAttack, targetTransfor);

            if (isExploited) return;

           
            if (isLeftSide)
            {
                resource.Anim.Play("TakeDamageLeft");
            }
            else
            {
                resource.Anim.Play("TakeDamageRight");
            }

            AudioManager.Instance.PlaySFX(chopTreeSFX, 0.5f);
        }

        public override void Die()
        {
            base.Die();     
            resource.Anim.SetBool("isExploitedLeft", isExploited);
            AudioManager.Instance.PlaySFX(treeFallingSFX, 0.5f);
        }

    }
}
