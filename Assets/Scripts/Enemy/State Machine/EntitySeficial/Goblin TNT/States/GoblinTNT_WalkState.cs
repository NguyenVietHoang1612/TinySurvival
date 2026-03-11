namespace GameRPG
{
    public class GoblinTNT_WalkState : WalkState
    {
        GoblinTNT goblinTNT;
        public GoblinTNT_WalkState(StateMachine stateMachine, Enemy entity, GoblinTNT goblinTNT, string animBoolName) : base(stateMachine, entity, animBoolName)
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
            else if (isWalkTimeOver)
            {
                StateMachine.ChangeState(goblinTNT.IdieState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }


    }
}
