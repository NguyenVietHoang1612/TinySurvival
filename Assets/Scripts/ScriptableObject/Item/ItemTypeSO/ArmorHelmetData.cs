using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/NewItem/NewArmor")]
    public class ArmorHelmetData : Item_SO
    {
        public int amountArmor;
        public int amountTakeAttackdurability;

        public override int GetArmor() => amountArmor;
    }

}
