namespace GameRPG
{
    public class GoblinTNT_ChaseState : ChaseState
    {
        GoblinTNT goblinTNT;
        public GoblinTNT_ChaseState(StateMachine stateMachine, Enemy entity, GoblinTNT goblinTNT, string animBoolName) : base(stateMachine, entity, animBoolName)
        {
            this.goblinTNT = goblinTNT;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (goblinTNT.GoblinTNTStatsManager.currentHealth <= 0)
            {
                StateMachine.ChangeState(goblinTNT.DieState);
            }
            else if (goblinTNT.IsPlayerTooClose())
            {
                StateMachine.ChangeState(goblinTNT.RetreatState);
            }
            else if (goblinTNT.IsPlayerInRangeAttack())
            {
                StateMachine.ChangeState(goblinTNT.RangeAttackState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}
