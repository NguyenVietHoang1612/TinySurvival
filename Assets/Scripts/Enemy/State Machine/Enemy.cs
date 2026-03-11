using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GameRPG
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D RB { get; private set; }

        public Animator Anim { get; private set; }

        public AnimationToStateMachine ATSM { get; private set; }

        public StateMachine StateMachine { get; private set; }

        public Collider2D MyCollider { get; private set; }
        public EnemyStatsManager EnemyStatsManager { get; private set; }

        public Transform target;

        public int facingDirection;


        [Header("WalkState")]
        public float RandomMovementRange;
        public float RandomMovementSpeed;
        public float minTimeWalk = 4f;
        public float maxTimeWalk = 6f;
        public Vector2 rbVelocity;

        [Header("IdieState")]
        public float minTimeIdie = 5f;
        public float maxTimeIdie = 10f;

        [Header("ChaseState")]
        public bool isChaseRange = false;
        public float minSpeedChase = 7;
        public float maxSpeedChase = 10;

        [Header("MeleeAttackState")]
        public int attackDamage;
        [SerializeField] private float rangeAttackDistance = 1f;
        public Transform attackPosition;
        public float attackRadius = 1f;
        public float attackCoolDownTimer = 2f;
        
        public LayerMask layerMaskPlayer;

        [Header("KnockBackState")]
        public Vector2 angle;
        public float strength = 1f;

        [Header("Detected player")]
        public float _detectRange = 10f;

        [Header("Distance Control")]
        public float tooCloseDistance = 2f;

        [SerializeField] public LayerMask Obstacle;

        protected virtual void Awake()
        {
            StateMachine = new StateMachine();
        }

        protected virtual void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            ATSM = GetComponent<AnimationToStateMachine>();
            MyCollider = GetComponent<Collider2D>();
            EnemyStatsManager = GetComponent<EnemyStatsManager>();

            facingDirection = 1;

        }


        private void OnEnable()
        {
            if (MyCollider != null)
                MyCollider.enabled = true;
        }

        public void DisableMyCollider()
        {
            if (MyCollider != null) 
                MyCollider.enabled = false;
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
            rbVelocity = RB.linearVelocity.normalized;
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicUpdate();
        }

        public void SetIsChase(bool isChase)
        {
            isChaseRange = isChase;
        }


        public void Flip()
        {
            facingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void CheckIfShouldFlip(int horizontalMovement)
        {
            if (horizontalMovement != 0 && horizontalMovement != facingDirection)
            {
                Flip();
            }
        }

        public void CheckIfShouldFlipToTarget()
        {
            if (target == null) return;

            float directionToTarget = target.position.x - transform.position.x;

            if (directionToTarget > 0 && facingDirection == -1)
            {
                Flip();
            }
            else if (directionToTarget < 0 && facingDirection == 1)
            {
                Flip();
            }
        }

        private void LookForPlayer()
        {
            Collider2D[] detectionArea = Physics2D.OverlapCircleAll(transform.position, _detectRange, layerMaskPlayer);

            float minDistance = Mathf.Infinity;
            GameObject target = null;

            foreach (Collider2D collider in detectionArea)
            {
                if (collider.CompareTag("Player") || collider.CompareTag("npc"))
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        target = collider.gameObject;
                    }
                }
            }

            if (target != null)
            {
                this.target = target.GetComponent<Transform>();
            }

        }
        public bool IsPlayerTooClose()
        {
            if (target == null) return false;

            float distance = Vector2.Distance(transform.position, target.position);
            return distance <= tooCloseDistance;
        }

        public bool IsPlayerInRangeAttack()
        {
            if (target == null) return false;

            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            return distanceToPlayer <= rangeAttackDistance;
        }

        public IEnumerator SearchRoutine()
        {
            while (true)
            {
                LookForPlayer();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

}
