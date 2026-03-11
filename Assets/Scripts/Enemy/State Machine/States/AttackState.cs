using UnityEngine;

namespace GameRPG
{
    public class AttackState : State
    {
        protected bool isAnimationAttackFinish;
        protected int attackDamage;

        public AttackState(StateMachine stateMachine, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
        {

        }

        public override void Enter()
        {
            base.Enter();
            Enemy.RB.linearVelocity = Vector2.zero;
            Enemy.ATSM.attackState = this;
            isAnimationAttackFinish = false;
            Enemy.CheckIfShouldFlipToTarget();

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Enemy.RB.linearVelocity = Vector2.zero;

            Enemy.CheckIfShouldFlip(Mathf.RoundToInt(Enemy.rbVelocity.x));

        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public virtual void TriggerAttack()
        {

        }

        public virtual void FinishAtack()
        {
            isAnimationAttackFinish = true;
        }
    }
}

