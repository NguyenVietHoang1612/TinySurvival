using UnityEngine;

namespace GameRPG
{
    public class RockManager : ResourceManager
    {
        public RockStatManager rockStatManager;
        protected override void Awake()
        {
            base.Awake();
            rockStatManager = GetComponent<RockStatManager>();
        }
    }
}
