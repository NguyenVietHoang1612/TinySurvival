using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace GameRPG
{
    public class WalkState : State
    {

        protected Vector2 _direction;
        protected Vector3 _targetPos;
        private float timeWalk;
        protected bool isWalkTimeOver;
        private Coroutine _searchCo;

        public WalkState(StateMachine stateMachine, Enemy enemy, string animBoolName) : base(stateMachine, enemy, animBoolName)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _searchCo = Enemy.StartCoroutine(Enemy.SearchRoutine());
            _targetPos = GetRandomPointInCircle();
            Enemy.CheckIfShouldFlip(Mathf.RoundToInt(Enemy.rbVelocity.x));
            isWalkTimeOver = false;
            Enemy.RandomMovementSpeed = Random.Range(3f, 4f);
            Enemy.RandomMovementRange = Random.Range(15f, 20f);
            timeWalk = Random.Range(Enemy.minTimeWalk, Enemy.maxTimeWalk);

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
            if ((Enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
            {
                _targetPos = GetRandomPointInCircle();
            }


            if (Time.time > StartTime + timeWalk)
            {
                isWalkTimeOver = true;
            }


        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
            _direction = (_targetPos - Enemy.transform.position).normalized;
            Enemy.RB.linearVelocity = _direction * Enemy.RandomMovementSpeed;

        }

        protected Vector3 GetRandomPointInCircle()
        {
            float randomX = Random.Range(-Enemy.RandomMovementRange, Enemy.RandomMovementRange);
            float randomY = Random.Range(-Enemy.RandomMovementRange, Enemy.RandomMovementRange);
            Vector3 randomPoint = new Vector3(randomX, randomY, 0);

            Collider2D hit = Physics2D.OverlapCircle(Enemy.transform.position + randomPoint, 0.2f, Enemy.Obstacle);

            if (hit == null)
            {
                return Enemy.transform.position + new Vector3(randomPoint.x, randomPoint.y, 0);
            }

            return new Vector3(0f, 0f, 0f);


           
        }


    }

}
