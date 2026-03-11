using UnityEngine;

namespace GameRPG
{
    public class RangeAttackState : AttackState
    {

        public RangeAttackState(StateMachine stateMachine, Enemy enemy, string animBoolName) : base(stateMachine, enemy, animBoolName)
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

        public override void FinishAtack()
        {
            base.FinishAtack();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Enemy.CheckIfShouldFlip(Mathf.RoundToInt(Enemy.rbVelocity.x));
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public override void TriggerAttack()
        {
            base.TriggerAttack();


        }


    }
}
