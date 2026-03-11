using Mono.Cecil.Cil;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace GameRPG
{
    public class ToolInteractManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] EquipmentManager equipmentManager;
        private Player player;

        [Header("Interaction Settings")]
        public float interactDistance = 3f;

        [Header("Target Layers")]
        [SerializeField] LayerMask treeLayer;
        [SerializeField] LayerMask rockLayer; 

        private readonly Dictionary<ToolType, IToolAction> toolActions = new();

        private void Start()
        {
            player = GetComponentInParent<Player>();
            if (equipmentManager == null)
            {
                equipmentManager = FindFirstObjectByType<EquipmentManager>();
            }

            RegisterToolActions();
        }

        private void RegisterToolActions()
        {
            IToolAction axeAction = new AxeAction(treeLayer);
            toolActions.Add(axeAction.ToolType, axeAction);

            IToolAction hoeAction = new HoeAction(rockLayer);
            toolActions.Add(hoeAction.ToolType, hoeAction);
        }

        public void UseCurrentTool()
        {
            var itemData = equipmentManager.GetItemInSlot(ItemType.Tool);
            if (!(itemData is ToolData toolData)) return;

            if (toolActions.TryGetValue(toolData.toolType, out IToolAction actionStrategy))
            {

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactDistance, actionStrategy.TargetLayerInteract);

                if (hit.collider != null)
                {
                    actionStrategy.Execute(player, toolData, hit.collider);
                }
                else
                {
                    Debug.Log("Raycast không trúng mục tiêu");
                }
            }

        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;

            Vector3 endPosition = transform.position + transform.right * interactDistance;

            Gizmos.DrawLine(transform.position, endPosition);
        }
    }
}