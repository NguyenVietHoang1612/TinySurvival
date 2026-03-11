using UnityEngine;

namespace GameRPG
{
    public class PlayerIdieState : PlayerState
    {
        public PlayerIdieState(PlayerFiniteMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Player.RB.linearVelocity = Vector2.zero;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (PlayerInputManager.Instance.LeftMouse_Input) 
            {
                PlayerInputManager.Instance.UseMouseLeftInput();
                if (Player.canAttack)
                {
                    StateMachine.ChangeState(Player.PlayerAttackState);
                }
            }
            else if ((Mathf.Abs(Player.movementHorizontal) > 0.01f || Mathf.Abs(Player.movementVertical) > 0.01f))
            {
                StateMachine.ChangeState(Player.PlayerMoveState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }


    }

}
