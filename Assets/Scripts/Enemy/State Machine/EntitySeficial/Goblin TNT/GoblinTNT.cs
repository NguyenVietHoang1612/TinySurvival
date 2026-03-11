using UnityEngine;

namespace GameRPG
{
    public class GoblinTNT : Enemy
    {
        public GoblinTNT_IdieState IdieState { get; private set; }
        public GoblinTNT_WalkState WalkState { get; private set; }
        public GoblinTNT_ChaseState ChaseState { get; private set; }
        public GoblinTNT_DieState DieState { get; private set; }
        public GoblinTNT_RangeAttackState RangeAttackState { get; private set; }
        public GoblinTNT_RetreatState RetreatState { get; private set; }
        public GoblinTNTStatsManager GoblinTNTStatsManager { get; private set; }

        public ProjectileMotion ProjectileMotion { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            IdieState = new GoblinTNT_IdieState(StateMachine, this, this, "Idie");
            WalkState = new GoblinTNT_WalkState(StateMachine, this, this, "Walk");
            ChaseState = new GoblinTNT_ChaseState(StateMachine, this, this, "Walk");
            RetreatState = new GoblinTNT_RetreatState(StateMachine, this, this, "Walk");
            RangeAttackState = new GoblinTNT_RangeAttackState(StateMachine, this, this, "RangeAttack");
            DieState = new GoblinTNT_DieState(StateMachine, this, this, "Die");

        }

        protected override void Start()
        {
            base.Start();
            StateMachine.Initialize(IdieState);
            ProjectileMotion = GetComponent<ProjectileMotion>();
            GoblinTNTStatsManager = GetComponent<GoblinTNTStatsManager>();
        }


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }



        protected override void Update()
        {
            base.Update();
        }
    }
}
