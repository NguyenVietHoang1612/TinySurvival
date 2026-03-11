using UnityEngine;

namespace GameRPG
{
    public interface IToolAction
    {
        ToolType ToolType { get; }

        LayerMask TargetLayerInteract { get; }
        void Execute(Player player, ToolData toolData, Collider2D targetCollider);
    }
}
