using UnityEngine;

namespace GameRPG
{
    public class AxeAction : IToolAction
    {
        public ToolType ToolType => ToolType.Axe;
        public LayerMask TargetLayerInteract { get; private set; }

        public AxeAction(LayerMask treeLayer)
        {
            TargetLayerInteract = treeLayer;
        }

        public void Execute(Player player, ToolData toolData, Collider2D targetCollider)
        {
            if (targetCollider == null)
            {
                Debug.Log($"[{ToolType}] Không tìm thấy mục tiêu trong phạm vi.");
                player.Anim.SetBool("Chop", false); 
                return;
            }
            if (targetCollider.TryGetComponent(out ResourceStatsManager resourceStat))
            {
                IInteractable interactableResource = targetCollider.GetComponent<IInteractable>();

                if (interactableResource != null)
                {
                    player.Anim.SetBool("chop", true);
                }
            }
        }
    }
}
