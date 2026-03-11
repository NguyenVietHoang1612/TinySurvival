using UnityEngine;

namespace GameRPG
{
    public class GoblinTNT_RangeAttackState : RangeAttackState
    {
        GoblinTNT goblinTNT;
        public GoblinTNT_RangeAttackState(StateMachine stateMachine, Enemy enemy, GoblinTNT goblinTNT, string animBoolName) : base(stateMachine, enemy, animBoolName)
        {
            this.goblinTNT = goblinTNT;
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

            if (goblinTNT.GoblinTNTStatsManager.isDead)
            {
                StateMachine.ChangeState(goblinTNT.DieState);
            }
            else if (isAnimationAttackFinish)
            {
                if (goblinTNT.IsPlayerTooClose())
                {
                    StateMachine.ChangeState(goblinTNT.RetreatState);
                }
                else if (!goblinTNT.IsPlayerInRangeAttack() && goblinTNT.isChaseRange)
                {
                    StateMachine.ChangeState(goblinTNT.ChaseState);
                }
                else if (!goblinTNT.IsPlayerInRangeAttack() && !goblinTNT.isChaseRange)
                {
                    StateMachine.ChangeState(goblinTNT.IdieState);
                }
                else
                {
                    StateMachine.ChangeState(goblinTNT.IdieState);
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
            if (Enemy.target != null)
            {
                goblinTNT.ProjectileMotion.Fire(goblinTNT.attackPosition, Enemy.target);
            }   
        }
    }
}
