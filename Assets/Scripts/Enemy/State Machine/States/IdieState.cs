using UnityEngine;

namespace GameRPG
{
    public class IdieState : State
    {
        protected bool isIdieTimeOver;
        protected float idieTime;
        private Coroutine _searchCo;
        public IdieState(StateMachine stateMachine, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _searchCo = Enemy.StartCoroutine(Enemy.SearchRoutine());
            Enemy.RB.linearVelocity = Vector2.zero;
            idieTime = Random.Range(Enemy.minTimeIdie, Enemy.maxTimeIdie);
            isIdieTimeOver = false;
        }

        public override void Exit()
        {
            base.Exit();
            if (_searchCo != null)
            {
                Enemy.StopCoroutine(_searchCo);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time > StartTime + idieTime)
            {
                isIdieTimeOver = true;
            }


        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }


    }

}
