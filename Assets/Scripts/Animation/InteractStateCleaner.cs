using UnityEngine;

namespace GameRPG
{
    public class InteractStateCleaner : StateMachineBehaviour
    {

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerInteract playerInteract = animator.GetComponent<PlayerInteract>();

            if (playerInteract != null)
            {
                playerInteract.DisableInteractColision();
            }
        }
    }
}
