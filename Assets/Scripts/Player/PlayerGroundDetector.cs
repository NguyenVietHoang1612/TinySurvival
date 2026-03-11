using UnityEngine;

namespace GameRPG
{
    public class PlayerGroundDetector : MonoBehaviour
    {
        private FootstepManager footstepManager;

        void Start()
        {
            footstepManager = GetComponent<FootstepManager>();
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            GroundSurface surface = collision.collider.GetComponent<GroundSurface>();
            if (surface != null)
            {
                footstepManager.SetGround(surface.groundType);
            }
        }
    }

}
