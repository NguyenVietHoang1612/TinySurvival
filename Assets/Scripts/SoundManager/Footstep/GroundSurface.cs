using UnityEngine;

namespace GameRPG
{
    public enum GroundType
    {
        Dirt,
        Wood,
        Stone,
        Water
    }

    public class GroundSurface : MonoBehaviour
    {
        

        public GroundType groundType;
    }
}
