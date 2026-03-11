
using UnityEngine;

namespace GameRPG
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D RB { get; private set; }

        public Animator Anim { get; private set; }

        public PlayerFiniteMachine StateMachine { get; private set; }

        public AnimationToStateMachine ATSM { get; private set; }

        public PlayerStatsManager PlayerStats { get; set; }

        public InventoryManager InventoryManager { get; set; }

        public PlayerEquipmentController PlayerEquipmentController { get; set; }

        private Collider2D coll2D;

        [Header("State")]
        public PlayerIdieState PlayerIdieState { get; private set; }
        public PlayerMoveState PlayerMoveState { get; private set; }
        public PlayerAttackState PlayerAttackState { get; private set; }


        [Header("Movement")]
        public float movementHorizontal;
        public float movementVertical;
        [SerializeField] Vector2 movementInput;
        private int facingDirection;

        [Header("Combat")]
        public bool canAttack = true;

        [SerializeField] private float attackCooldown = 0.5f;
        private float lastTimeAttacked;

        private void Awake()
        {
            StateMachine = new PlayerFiniteMachine();
            PlayerIdieState = new PlayerIdieState(StateMachine, this, "idle");
            PlayerMoveState = new PlayerMoveState(StateMachine, this, "walk");
            PlayerAttackState = new PlayerAttackState(StateMachine, this, "isAttack");
            InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        }

        void Start()
        {

            RB = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            ATSM = GetComponent<AnimationToStateMachine>();
            coll2D = GetComponent<Collider2D>();

            PlayerStats = GetComponent<PlayerStatsManager>();
            PlayerEquipmentController = GetComponent<PlayerEquipmentController>();

            facingDirection = 1;
            StateMachine.Initinize(PlayerIdieState);

            if (SaveGameManager.Instance != null)
            {
                SaveGameManager.Instance.RegisterPlayer(this);
            }
            else
            {
                Debug.Log("SaveGameManager instance not found! Player will not load initial data.");
            }

        }

        void Update()
        {
            StateMachine.CurrentState.LogicUpdate();

            movementInput = PlayerInputManager.Instance.movementInput.normalized;
            movementHorizontal = movementInput.x;
            movementVertical = movementInput.y;

            if (!canAttack)
            {
                if (Time.time >= lastTimeAttacked + attackCooldown)
                {
                    canAttack = true;
                }
            }
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicUpdate();
        }

        public void SetColliderEnable()
        {
            coll2D.enabled = true;
        }

        public void SetColliderDisable()
        {
            coll2D.enabled = false;
        }


        public void Flip()
        {
            facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void CheckIfShouldFlip(int horizontalMovement)
        {
            if (horizontalMovement != 0 && horizontalMovement != facingDirection)
            {
                Flip();
            }
        }

        public void SetLastTimeAttacked(float time) => lastTimeAttacked = time;

        public void SaveDataPlayer(ref PlayerData data)
        {
            data.currentHealth = PlayerStats.CurrentHealth;
            data.maxHealth = PlayerStats.MaxHealth;
            data.positionPlayer = new float[3];
            data.positionPlayer[0] = transform.position.x;
            data.positionPlayer[1] = transform.position.y;
            data.positionPlayer[2] = transform.position.z;

            for (int i = 0; i < InventoryManager.ItemSlots.Length; i++)
            {

                if (InventoryManager.ItemSlots[i].Item != null)
                {
                    data.slots[i].iD = InventoryManager.ItemSlots[i].ID;
                    data.slots[i].item = InventoryManager.ItemSlots[i].Item;
                    data.slots[i].quantity = InventoryManager.ItemSlots[i].Quantity;

                }
            }

            data.itemsData.Clear();

            Debug.LogWarning("SaveItemDropped Warning");
        }

        public void LoadPlayerData(PlayerData data)
        {
            PlayerStats.SetCurrentHealth(data.currentHealth, data.maxHealth);
            Vector3 position;
            position.x = data.positionPlayer[0];
            position.y = data.positionPlayer[1];
            position.z = data.positionPlayer[2];
            transform.position = position;

            InventoryManager.LoadInventoryData(data);

            PlayerStats.RefreshStatUI();
        }
    }
}
