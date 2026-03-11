using UnityEngine;

namespace GameRPG
{
    public class Goblin : Enemy
    {
        public Goblin_IdieState IdieState { get; private set; }
        public Goblin_WalkState WalkState { get; private set; }
        public Goblin_ChaseState ChaseState { get; private set; }
        public Goblin_DieState DieState { get; private set; }
        public Goblin_MeleeAttackState MeleeAttackState { get; private set; }
        public GoblinStatsManage GoblinStatsManager { get; private set; }

        
        protected override void Awake()
        {
            base.Awake();
            IdieState = new Goblin_IdieState(StateMachine, this, this, "Idie");
            WalkState = new Goblin_WalkState(StateMachine, this, this, "Walk");
            ChaseState = new Goblin_ChaseState(StateMachine, this, this, "Walk");
            MeleeAttackState = new Goblin_MeleeAttackState(StateMachine, this, this, "MeleeAttack");
            DieState = new Goblin_DieState(StateMachine, this, this, "isDead");


        }

        protected override void Start()
        {
            base.Start();
            GoblinStatsManager = GetComponent<GoblinStatsManage>();
            StateMachine.Initialize(IdieState);
        }


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }



        protected override void Update()
        {
            base.Update();
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
        }

    }

}

