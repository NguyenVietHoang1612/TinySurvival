using UnityEngine;

namespace GameRPG
{
    public class DeadState : State
    {
        protected float resetStatTime = 6f;
        protected bool isReset = false;

        public DeadState(StateMachine stateMachine, Enemy enemy, string animBoolName) : base(stateMachine, enemy, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.RB.linearVelocity = Vector2.zero;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + resetStatTime)
            {
                isReset = true;
            }
        }

        public override void PhysicUpdate()
        {
            Enemy.RB.linearVelocity = Vector2.zero;
            base.PhysicUpdate();
        }
    }
}
