using UnityEngine;

namespace GameRPG
{
    public class RetreatState : State
    {
        private Vector2 direction;

        public RetreatState(StateMachine stateMachine, Enemy enemy, string animBoolName)
            : base(stateMachine, enemy, animBoolName)
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

            if (Enemy.target == null) return;

            direction = (Enemy.transform.position - Enemy.target.position).normalized;

            Enemy.RB.linearVelocity = direction * Enemy.RandomMovementSpeed;
        }
    }
}