using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/NewItem/NewBuilding")]
    public class BuildingData : Item_SO
    {
        public Vector2 size;

        public GameObject prefab;
    }
}
