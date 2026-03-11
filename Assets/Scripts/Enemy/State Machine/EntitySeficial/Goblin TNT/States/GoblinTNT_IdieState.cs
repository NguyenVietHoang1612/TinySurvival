namespace GameRPG
{
    public class GoblinTNT_IdieState : IdieState
    {
        GoblinTNT goblinTNT;
        public GoblinTNT_IdieState(StateMachine stateMachine, Enemy entity, GoblinTNT goblinTNT, string animBoolName) : base(stateMachine, entity, animBoolName)
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
            else if (goblinTNT.isChaseRange)
            {
                StateMachine.ChangeState(goblinTNT.ChaseState);
            }
            else if (isIdieTimeOver)
            {
                StateMachine.ChangeState(goblinTNT.WalkState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}
