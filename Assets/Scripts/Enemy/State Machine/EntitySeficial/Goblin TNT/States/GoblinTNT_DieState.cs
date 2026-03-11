namespace GameRPG
{
    public class GoblinTNT_DieState : DeadState
    {
        GoblinTNT goblinTNT;
        public GoblinTNT_DieState(StateMachine stateMachine, Enemy enemy, GoblinTNT goblinTNT, string animBoolName) : base(stateMachine, enemy, animBoolName)
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
            if (isReset)
            {
                isReset = false;
                goblinTNT.GoblinTNTStatsManager.isDead = false;
                goblinTNT.GoblinTNTStatsManager.currentHealth = goblinTNT.GoblinTNTStatsManager.maxHealth;
                StateMachine.ChangeState(goblinTNT.WalkState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}
