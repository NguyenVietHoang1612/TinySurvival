using UnityEngine;

namespace GameRPG
{
    public class MeleeAttackState : AttackState
    {

        public MeleeAttackState(StateMachine stateMachine, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(Enemy.attackPosition.position, Enemy.attackRadius, Enemy.layerMaskPlayer);

            foreach (Collider2D obj in detectedObjects)
            {

                PlayerStatsManager damageable = obj.GetComponent<PlayerStatsManager>();
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                }


            }
        }

        public override void FinishAtack()
        {
            base.FinishAtack();
        }
    }
}

