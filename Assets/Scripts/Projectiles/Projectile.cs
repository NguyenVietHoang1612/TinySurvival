using UnityEngine;

namespace GameRPG
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D RB { get; protected set; }
        public Animator Animator { get; protected set; }

        [SerializeField] protected int damagePlayer = 15;


        [SerializeField] protected LayerMask layerPlayer;
        protected virtual void OnEnable()
        {
            RB = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
    }
}
