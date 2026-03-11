namespace GameRPG
{
    public class GoblinTNT_RetreatState : RetreatState
    {
        GoblinTNT goblin;

        public GoblinTNT_RetreatState(StateMachine sm, Enemy e, GoblinTNT g, string anim)
            : base(sm, e, anim)
        {
            goblin = g;
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

            if (goblin.GoblinTNTStatsManager.currentHealth <= 0)
            {
                StateMachine.ChangeState(goblin.DieState);
            }
            else if (!goblin.IsPlayerTooClose())
            {
                StateMachine.ChangeState(goblin.RangeAttackState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }
    }
}