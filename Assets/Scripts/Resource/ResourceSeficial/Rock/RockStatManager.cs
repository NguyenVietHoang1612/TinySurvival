using UnityEngine;

namespace GameRPG
{
    public class RockStatManager : ResourceStatsManager
    {
        [SerializeField] private Sprite[] sprite;
        [SerializeField] AudioClip brokenRockSFX;
        [SerializeField] AudioClip[] miningSFX;
        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(int speedInteract, Transform targetTransform)
        {
            base.Interact(speedInteract, targetTransform);

            resource.Anim.Play("TakeDamageRock");
            AudioManager.Instance.PlayerSFXRandom(miningSFX, 0.5f);

            if (currentHealth <= maxHealth * 0.5f)
            {
                resource.SpriteRenderer.sprite = sprite[1];
            }
        }

        public override void Die()
        {
            base.Die();
            resource.Anim.SetBool("isExploited", isExploited);
            AudioManager.Instance.PlaySFX(brokenRockSFX, 1f);
            Destroy(gameObject, 0.5f);
        }
    }
}
