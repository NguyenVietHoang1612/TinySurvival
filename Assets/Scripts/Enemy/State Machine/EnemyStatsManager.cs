using System.Collections;
using UnityEngine;


namespace GameRPG
{
    public class EnemyStatsManager : MonoBehaviour, IDamageable
    {
        protected Enemy enemy;

        public int currentHealth;
        public int maxHealth;

        [SerializeField] protected int baseDamage = 0;
        [SerializeField] protected int baseMagic = 0;
        [SerializeField] protected int baseAmor = 0;

        [SerializeField] public bool isDead = false;

        public int expReward = 15;

        [SerializeField] protected Item_SO[] itemReward;
        [SerializeField] protected int quantityReward;

        public static event DropItemMonsterDefeated DropItemOnMonsterDefeated;
        public delegate void DropItemMonsterDefeated(Item_SO[] itemDrop, int quantity, Vector2 position);


        protected virtual void Awake() { }

        protected virtual void Start()
        {
            currentHealth = maxHealth;
            quantityReward = 1;
            enemy = GetComponent<Enemy>();
        }

        public virtual void TakeDamage(int amount)
        {
            if (isDead) return;

            Debug.Log("Take damge player: " + amount);
            currentHealth -= CalculateTheAmountOfArmor(amount);

            if (currentHealth <= 0)
            {
                isDead = true;
                enemy.DisableMyCollider();
                Die();
                return;
            }

        }

        private int CalculateTheAmountOfArmor(int amount)
        {
            int totalDamage = amount - baseAmor;

            if (totalDamage <= 0) return 1;


            return totalDamage;
        }


        public Vector2 RandomPositionDrop()
        {

            float ranDomX = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);
            float ranDomY = Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f);
            return new Vector2(ranDomX, ranDomY);
        }

        public virtual void Die()
        {
            Debug.Log("Enemy Die");

            enemy.Anim.SetBool("isDead", isDead);

            DropItemOnMonsterDefeated(itemReward, quantityReward, RandomPositionDrop());

            StartCoroutine(SetGameObjectWhenDie());

        }

        private IEnumerator SetGameObjectWhenDie()
        {
            yield return new WaitForSeconds(1.3f);
            gameObject.SetActive(false);

        }


        public int GetBaseDamage()
        {
            return baseDamage;
        }

        public int GetBaseMagic()
        {
            return baseMagic;
        }


    }
}
