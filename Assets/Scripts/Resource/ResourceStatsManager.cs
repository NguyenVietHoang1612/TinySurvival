using UnityEngine;

namespace GameRPG
{
    public class ResourceStatsManager : MonoBehaviour, IInteractable
    {
        protected ResourceManager resource;

        public int currentHealth;
        public int maxHealth;
        protected bool isExploited = false;

        [SerializeField] protected Item_SO[] itemReward;
        [SerializeField] protected int minQuantityReward = 1;
        [SerializeField] protected int maxQuantityReward = 3;

        protected bool isLeftSide = false;

        public delegate void DropItemOnResourceDefeated(Item_SO[] itemDrop, int quantity, Vector2 position);
        public static event DropItemOnResourceDefeated OnDropItemOnResourceDefeated;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
        }

        protected virtual void Start()
        {
            resource = GetComponent<ResourceManager>();
        }

        public virtual void Interact(int speedInteract, Transform targetTransform)
        {
            if (isExploited) return;

            if (resource.transform.position.x <= targetTransform.position.x) 
            {  
                isLeftSide = true;
            }else 
            {
                isLeftSide = false;
                
            }

            currentHealth -= speedInteract;

            if (currentHealth <= 0 && !isExploited)
            {
                currentHealth = 0; 
                isExploited = true;
                Die();
                return;
            }
        }

        public Vector2 RandomPositionDrop()
        {

            float ranDomX = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);
            float ranDomY = Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f);
            return new Vector2(ranDomX, ranDomY);
        }

        public virtual void Die()
        {
            resource.Anim.SetBool("isExploited", isExploited);
            resource.SetCollider.enabled = false;
            int quantity = Random.Range(minQuantityReward, maxQuantityReward + 1);
            OnDropItemOnResourceDefeated(itemReward, quantity, RandomPositionDrop());

        }
    }
}
