namespace GameRPG
{
    public class Goblin_DieState : DeadState
    {
        Goblin _goblin;

        public Goblin_DieState(StateMachine stateMachine, Enemy entity, Goblin goblin, string animBoolName) : base(stateMachine, entity, animBoolName)
        {
            _goblin = goblin;
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
                _goblin.GoblinStatsManager.isDead = false;
                _goblin.GoblinStatsManager.currentHealth = _goblin.GoblinStatsManager.maxHealth;
                StateMachine.ChangeState(_goblin.WalkState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }

}
