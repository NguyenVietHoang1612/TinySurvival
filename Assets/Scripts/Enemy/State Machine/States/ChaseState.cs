using UnityEngine;

namespace GameRPG
{
    public class ChaseState : State
    {
        private Vector3 _direction;
        private Coroutine _searchCo;

        public ChaseState(StateMachine stateMachine, Enemy entity, string animBoolName) : base(stateMachine, entity, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.RandomMovementSpeed = Random.Range(Enemy.minSpeedChase, Enemy.maxSpeedChase);
            _searchCo = Enemy.StartCoroutine(Enemy.SearchRoutine());

        }

        public override void Exit()
        {
            base.Exit();
            if (_searchCo != null)
                Enemy.StopCoroutine(_searchCo);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Enemy.CheckIfShouldFlip(Mathf.RoundToInt(Enemy.rbVelocity.x));


        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            if (Enemy.target != null) 
            {
                _direction = (Enemy.target.transform.position - Enemy.transform.position).normalized;

                Enemy.RB.linearVelocity = _direction * Enemy.RandomMovementSpeed;
            }
            
        }
    }
}

