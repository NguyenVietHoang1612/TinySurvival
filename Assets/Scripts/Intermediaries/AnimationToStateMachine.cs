using UnityEngine;

namespace GameRPG
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public AttackState attackState;
        public PlayerAttackState playerAttackState;

        private void TriggerAttack()
        {
            attackState.TriggerAttack();

        }

        private void finishAttack()
        {
            attackState.FinishAtack();
        }


        private void PlayerfinishAttack()
        {
            playerAttackState.FinishAtack();
        }

    }
}

