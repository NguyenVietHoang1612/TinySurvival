using UnityEngine;

namespace GameRPG
{
    public class PlayerAttackState : PlayerState
    {
        protected bool isAnimationAttackFinish = true;

        public PlayerAttackState(PlayerFiniteMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Player.ATSM.playerAttackState = this;
            Player.RB.linearVelocity = Vector2.zero;
            isAnimationAttackFinish = false;
            PlayerInputManager.Instance.UseMouseLeftInput();
        }

        public override void Exit()
        {
            base.Exit();
            Player.SetLastTimeAttacked(Time.time);
            Player.canAttack = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAnimationAttackFinish)
            {
                StateMachine.ChangeState(Player.PlayerIdieState);
            }

        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public virtual void FinishAtack()
        {
            isAnimationAttackFinish = true;
        }


    }
}
