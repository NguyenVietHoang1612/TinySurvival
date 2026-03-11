using UnityEngine;

namespace GameRPG
{
    public class PlayerState
    {
        public PlayerFiniteMachine StateMachine { get; private set; }
        public Player Player { get; private set; }

        public float StartTime { get; private set; }

        public string _animBoolName;


        public PlayerState(PlayerFiniteMachine stateMachine, Player player, string animBoolName)
        {
            StateMachine = stateMachine;
            Player = player;
            _animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            StartTime = Time.time;
            Player.Anim.SetBool(_animBoolName, true);
        }

        public virtual void Exit()
        {
            Player.Anim.SetBool(_animBoolName, false);
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicUpdate() { }
    }
}
