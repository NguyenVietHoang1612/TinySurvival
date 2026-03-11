namespace GameRPG
{
    public class Goblin_IdieState : IdieState
    {

        private Goblin _goblin;
        public Goblin_IdieState(StateMachine stateMachine, Enemy entity, Goblin goblin, string animBoolName) : base(stateMachine, entity, animBoolName)
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
            if (_goblin.GoblinStatsManager.currentHealth <= 0)
            {
                StateMachine.ChangeState(_goblin.DieState);

            }
            else if (_goblin.isChaseRange)
            {
                StateMachine.ChangeState(_goblin.ChaseState);
            }
            else if (isIdieTimeOver)
            {
                StateMachine.ChangeState(_goblin.WalkState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}

