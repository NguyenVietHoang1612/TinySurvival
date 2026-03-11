namespace GameRPG
{
    public class StunState : State
    {
        public StunState(StateMachine stateMachine, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
        {
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
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}
