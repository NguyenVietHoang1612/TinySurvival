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

        [Header("Base Stats")]
        [SerializeField] private int baseHealth;
        [SerializeField] private int baseDamage;
        [SerializeField] private int baseArmor;
        [SerializeField] private int baseMagicDamage;
        [SerializeField] private int baseSpeedInteract;
        [SerializeField] private float baseMovementSpeed;

        [Header("Current Stats")]
        [SerializeField] private int currentHealth;
        [SerializeField] private int currentPhysicDamage;
        [SerializeField] private int currentMagicDamage;
        [SerializeField] private int currentArmor;
        [SerializeField] private int currentSpeedInteract;
        [SerializeField] private float currentMovementSpeed;

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
            currentHealth = baseHealth;
            

            playerUIHubManager.healthUI.SetHealthValue(currentHealth, baseHealth);

            baseDamage = characterStats.baseDamage;
            baseArmor = characterStats.baseArmor;
            baseMagicDamage = characterStats.baseMagicDamage;
            baseSpeedInteract = characterStats.baseSpeedInteract;
            baseMovementSpeed = characterStats.baseMovementSpeed;

            equipmentManager.OnEquipmentChange += UpdateStats;
            UpdateStats();
        }

        public void TakeDamage(int amout)
        {
            Debug.Log("Take damge: " + amout);
            currentHealth -= CalculateTheAmountOfArmor(amout);
            playerUIHubManager.healthUI.anim.Play("TextUpdate");
            playerUIHubManager.healthUI.SetHealthValue(currentHealth, baseHealth);
            player.Anim.SetTrigger("TakeDamage");
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

        }

        private int CalculateTheAmountOfArmor(int amout)
        {
            int totalDamage = amout - currentArmor;

            if (totalDamage <= 0) return 1;


            return totalDamage;
        }

        public void Die()
        {
            player.SetColliderDisable();
            player.Anim.SetBool("IsDead", true);
            PlayerInputManager.Instance.DisablePlayerController();

            StartCoroutine(SetGameObjectWhenDie());
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
            playerUIHubManager.healthUI.SetHealthValue(currentHealth, baseHealth);
        }

        public void RefreshStatUI()
        {
            playerUIHubManager.healthUI.SetHealthValue(currentHealth, baseHealth);
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
            playerUIHubManager.healthUI.SetHealthValue(currentHealth, maxHealth);
        }

    }

}

