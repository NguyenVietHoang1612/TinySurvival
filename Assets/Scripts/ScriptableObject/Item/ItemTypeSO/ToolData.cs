using GameRPG.Assets.Scripts.Interface;
using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/NewItem/NewTool")]
    public class ToolData : Item_SO, IEquippable
    {
        public ToolType toolType;
        public int speedInteract;

        public int physicDamage;
        public int amountTakeAttackdurability;

        public GameObject visualPrefab;

        public override int GetSpeedInteract() => speedInteract;
        public override int GetPhysicDamage() => physicDamage;

        public LayerMask layerTarget;

        public GameObject VisualEquipmentPrefab => visualPrefab;
    }

    public enum ToolType
    {
        Axe,
        Hoe,
        Shovel,
        Pickaxe,
        Pitchfork,
        Torch,
    }
}
