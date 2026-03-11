using UnityEngine;

namespace GameRPG
{
    public class ResourceManager : MonoBehaviour
    {
        public Animator Anim { get; private set; }
        public Collider2D SetCollider { get; private set; }

        public ResourceStatsManager StatsManager { get; private set; }

        public SpriteRenderer SpriteRenderer { get; private set; }

        protected virtual void Awake()
        {
            Anim = GetComponentInChildren<Animator>();
            SetCollider = GetComponentInChildren<Collider2D>();
            StatsManager = GetComponent<ResourceStatsManager>();
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        protected virtual void Start()
        {
            

        }

    }
}

