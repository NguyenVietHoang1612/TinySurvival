using UnityEngine;

namespace GameRPG
{
    public class CraftingStation : MonoBehaviour
    {
        [SerializeField] private StationTag stationTag;
        public StationTag StationTag => stationTag;
    }
}
