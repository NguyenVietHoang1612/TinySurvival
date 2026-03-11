using Unity.VisualScripting;
using UnityEngine;

namespace GameRPG
{
    public class HoeAction : IToolAction
    {
        public ToolType ToolType => ToolType.Hoe;

        public LayerMask TargetLayerInteract { get; private set; }

        public HoeAction(LayerMask treeLayer)
        {
            TargetLayerInteract = treeLayer;
        }

        public void Execute(Player player, ToolData toolData, Collider2D targetCollider)
        {
            if (targetCollider == null)
            {
                Debug.Log($"[{ToolType}] Không tìm thấy mục tiêu trong phạm vi.");
                return;
            }

            if (targetCollider != null)
            {
                IInteractable interactableResource = targetCollider.GetComponent<IInteractable>();

                if (interactableResource != null)
                {
                    Debug.Log("Hoe is playing");    
                    player.Anim.SetBool("hoe", true);
                }
            }
        }
    }
}
