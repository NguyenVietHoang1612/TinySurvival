using System;
using System.Collections;
using UnityEngine;
namespace GameRPG
{
    public class PlayerStatsManager : MonoBehaviour, IDamageable
    {
        private Player player;

        [Header("Reference")]
        [SerializeField] PlayerUIHubManager playerUIHubManager;
        [SerializeField] EquipmentManager equipmentManager;
        [SerializeField] private BaseStatsCharacter characterStats;

        [Header("Base Stats Survival")]
        private int baseHealth;
        private int baseHunger;
        private float baseHungerDecreaseRate;
        private float baseRateTimeHunger = 780f;

        [Header("Base Stats Action")]
        private int baseDamage;
        private int baseArmor;
        private int baseMagicDamage;
        private int baseSpeedInteract;
        private float baseMovementSpeed;

        [Header("Current Stats")]
        [SerializeField] private int currentHealth;
        [SerializeField] private float currentHunger;
        [SerializeField] private int currentPhysicDamage;
        [SerializeField] private int currentMagicDamage;
        [SerializeField] private int currentArmor;
        [SerializeField] private int currentSpeedInteract;
        [SerializeField] private float currentMovementSpeed;

        private float decrementHungerTimer;
        [SerializeField] private float decrementHungerInterval = 1f;

        public static event Action<int, int> OnHealthChange;

        public int MaxHealth 
        
        { 
            get => baseHealth; 
            private set
            {
                baseHealth = value;
            }
        } 

        public int CurrentHealth => currentHealth;

        public int TotalPhysicDamage => currentPhysicDamage;

        public int TotalMagicDamage => currentMagicDamage;

        public int TotalArmor => currentArmor;

        public int TotalSpeedInteract => currentSpeedInteract;

        public float CurrentMovementSpeed => currentMovementSpeed;

        private void Start()
        {
            player = GetComponent<Player>();

            if (characterStats == null)
            {
                Debug.LogWarning("characterStats is not Reference");
                return;
            }

            baseHealth = characterStats.baseHealth;
            baseHunger = characterStats.baseHunger;
            baseHungerDecreaseRate = characterStats.baseHungerDecreaseRate;
            currentHunger = baseHunger;
            currentHealth = baseHealth;
            
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, baseHealth);
            playerUIHubManager.HungerUI.SetMaxHungerValue(baseHunger);

            baseDamage = characterStats.baseDamage;
            baseArmor = characterStats.baseArmor;
            baseMagicDamage = characterStats.baseMagicDamage;
            baseSpeedInteract = characterStats.baseSpeedInteract;
            baseMovementSpeed = characterStats.baseMovementSpeed;

            equipmentManager.OnEquipmentChange += UpdateStats;

            UpdateStats();
        }
        private void OnEnable()
        {
            InventorySlots.OnEatConsumableItem += IncreaseStatWhenEat;
        }

        private void OnDisable()
        {
            InventorySlots.OnEatConsumableItem -= IncreaseStatWhenEat;
        }
        private void Update()
        {
            UpdateHungerOnTime();
        }

        public void UpdateHungerOnTime()
        {
            float reductionPerSecond = baseHunger / baseRateTimeHunger;
            float amountToDecrease = reductionPerSecond * baseHungerDecreaseRate * Time.deltaTime;

            float oldHunger = currentHunger;
            currentHunger = Mathf.Max(currentHunger - amountToDecrease, 0);

            if (Mathf.Abs(oldHunger - currentHunger) > 1f)
            {
                playerUIHubManager.HungerUI.SetCurrentHungerValue(currentHunger);
            }

            if (currentHunger <= 0)
            {
                decrementHungerTimer += Time.deltaTime;
                if (decrementHungerTimer >= decrementHungerInterval)
                {
                    decrementHungerTimer = 0;
                    TakeDamage(1);
                }
            }
        }

        public void IncreaseStatWhenEat(ConsumableData consumable)
        {
            currentHunger += consumable.hungerAmount;
            currentHealth += consumable.healAmount;

            currentHealth = Mathf.Min(currentHealth, baseHealth);
            currentHunger = Mathf.Min(currentHunger, baseHunger);
            playerUIHubManager.HungerUI.SetCurrentHungerValue(currentHunger);
            playerUIHubManager.HungerUI.PlayHungerAnimation();
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, baseHealth);
        }


        public void TakeDamage(int amout)
        {
            Debug.Log("Take damge: " + amout);
            currentHealth -= CalculateTheAmountOfArmor(amout);
            playerUIHubManager.HealthUI.anim.Play("TextUpdate");
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, baseHealth);
            player.Anim.SetTrigger("TakeDamage");
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

        }

        public void Die()
        {
            player.SetColliderDisable();
            player.Anim.SetBool("IsDead", true);
            PlayerInputManager.Instance.DisablePlayerController();

            StartCoroutine(SetGameObjectWhenDie());
        }
        private int CalculateTheAmountOfArmor(int amout)
        {
            int totalDamage = amout - currentArmor;

            if (totalDamage <= 0) return 1;


            return totalDamage;
        }

        private IEnumerator SetGameObjectWhenDie()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        private void UpdateStats()
        {
            currentArmor = baseArmor;
            currentPhysicDamage = baseDamage;
            currentMagicDamage = baseMagicDamage;
            currentSpeedInteract = baseSpeedInteract;
            currentMovementSpeed = baseMovementSpeed;

            int physicDamageBonus = 0;
            int MagicDamageBonus = 0;
            int armorBonus = 0;
            int speedInteractBonus = 0;
            int movementSpeedBonus = 0;

            foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
            {

                var equipment = equipmentManager.GetItemInSlot(item);

                if (equipment == null) continue;


                physicDamageBonus += equipment.GetPhysicDamage();
                MagicDamageBonus += equipment.GetMagicDamage();
                speedInteractBonus += equipment.GetSpeedInteract();
                armorBonus += equipment.GetArmor();
                movementSpeedBonus += equipment.GetMovementSpeed();
            }

            currentArmor += armorBonus;
            currentPhysicDamage += physicDamageBonus;
            currentMagicDamage += MagicDamageBonus;
            currentSpeedInteract += speedInteractBonus;
            currentMovementSpeed += movementSpeedBonus;
        }
           

        public void UpdateMaxHealth(float amount)
        {
            baseHealth = Mathf.RoundToInt(baseHealth + (baseHealth * amount));
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, baseHealth);
        }

        public void RefreshStatUI()
        {
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, baseHealth);
        }
       

        public int GetComboDamage(int comboCounter)
        {
            int baseDamage = currentPhysicDamage;
            float comboMultiplier = 1 + (comboCounter * 0.5f);
            return Mathf.RoundToInt(baseDamage * comboMultiplier);
        }

        public void SetCurrentHealth(int currentHealth, int maxHealth)
        {
            this.currentHealth = currentHealth;
            this.baseHealth = maxHealth;
            playerUIHubManager.HealthUI.SetHealthValue(currentHealth, maxHealth);
        }

    }

}

