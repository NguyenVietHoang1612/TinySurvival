using UnityEngine;

namespace GameRPG
{
    public class Goblin_ChaseState : ChaseState
    {
        private Goblin _goblin;
        public Goblin_ChaseState(StateMachine stateMachine, Enemy entity, Goblin goblin, string animBoolName) : base(stateMachine, entity, animBoolName)
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
            float distanceToPlayer = Vector2.Distance(_goblin.transform.position, _goblin.target.position);
            if (_goblin.GoblinStatsManager.currentHealth <= 0)
            {
                StateMachine.ChangeState(_goblin.DieState);

            }
            else if (_goblin.IsPlayerInRangeAttack() && _goblin.isChaseRange)
            {
                StateMachine.ChangeState(_goblin.MeleeAttackState);
            }
            else if (!_goblin.isChaseRange && !_goblin.IsPlayerInRangeAttack())
            {
                _goblin.StateMachine.ChangeState(_goblin.IdieState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

    }
}

