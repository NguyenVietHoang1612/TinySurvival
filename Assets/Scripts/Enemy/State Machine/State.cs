using UnityEngine;

namespace GameRPG
{
    public class State
    {
        protected StateMachine StateMachine { get; private set; }

        protected Enemy Enemy { get; private set; }

        public float StartTime { get; protected set; }

        public string _animBoolName;

        public State(StateMachine stateMachine, Enemy enemy, string animBoolName)
        {
            StateMachine = stateMachine;
            Enemy = enemy;
            _animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            StartTime = Time.time;
            Enemy.Anim.SetBool(_animBoolName, true);

        }
        public virtual void Exit()
        {
            Enemy.Anim.SetBool(_animBoolName, false);
        }
        public virtual void LogicUpdate() { }
        public virtual void PhysicUpdate() { }
    }

}
