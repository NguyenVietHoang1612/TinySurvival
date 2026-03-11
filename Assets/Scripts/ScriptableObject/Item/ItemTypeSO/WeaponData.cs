using GameRPG.Assets.Scripts.Interface;
using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/NewItem/NewWeapon")]
    public class WeaponData : Item_SO, IEquippable
    {
        public int physicDamage;

        public int magicDamage;

        public int amountTakeAttackdurability;

        

        public GameObject visualPrefab;

        public GameObject VisualEquipmentPrefab => visualPrefab;

        public override int GetPhysicDamage() => physicDamage;
        public override int GetMagicDamage() => magicDamage;
    }

}
