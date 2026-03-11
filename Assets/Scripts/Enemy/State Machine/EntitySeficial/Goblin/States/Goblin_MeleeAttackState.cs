using UnityEngine;

namespace GameRPG
{
    public class Goblin_MeleeAttackState : MeleeAttackState
    {
        private Goblin _goblin;
        public Goblin_MeleeAttackState(StateMachine stateMachine, Goblin goblin, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
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

        public override void FinishAtack()
        {
            base.FinishAtack();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            attackDamage = _goblin.attackDamage;
            if (_goblin.GoblinStatsManager.currentHealth <= 0)
            {
                StateMachine.ChangeState(_goblin.DieState);

            }
            else if (isAnimationAttackFinish)
            {
                if (!_goblin.IsPlayerInRangeAttack() && _goblin.isChaseRange)
                {
                    StateMachine.ChangeState(_goblin.ChaseState);
                }
                else if (!_goblin.IsPlayerInRangeAttack() && !_goblin.isChaseRange)
                {
                    StateMachine.ChangeState(_goblin.IdieState);
                }
                else
                {
                    StateMachine.ChangeState(_goblin.IdieState);
                }
            }

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

