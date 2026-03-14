using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/NewItem/NewConsumable")]
    public class ConsumableData : Item_SO
    {
        public int healAmount;
        public int hungerAmount;
        public bool isEat;
    }
}
