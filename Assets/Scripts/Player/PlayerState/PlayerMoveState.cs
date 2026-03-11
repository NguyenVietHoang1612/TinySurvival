using UnityEngine;

namespace GameRPG
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(PlayerFiniteMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
            Player.RB.linearVelocity = new Vector2(Player.movementHorizontal, Player.movementVertical) * Player.PlayerStats.CurrentMovementSpeed;
            Player.CheckIfShouldFlip(Mathf.RoundToInt(Player.movementHorizontal));


            if (PlayerInputManager.Instance.LeftMouse_Input) 
            {
                PlayerInputManager.Instance.UseMouseLeftInput();
                if (Player.canAttack)
                {
                    StateMachine.ChangeState(Player.PlayerAttackState);
                }
            }
            else if (Mathf.Abs(Player.movementHorizontal) == 0 && Mathf.Abs(Player.movementVertical) == 0)
            {
                StateMachine.ChangeState(Player.PlayerIdieState);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }




    }
}

