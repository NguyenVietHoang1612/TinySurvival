using System.Collections.Generic;
using UnityEngine;
namespace GameRPG
{ 
    public class FootstepManager : MonoBehaviour
    {
        [System.Serializable]
        public class FootstepClips
        {
            public GroundType groundType;
            public AudioClip[] clips;
        }

        public List<FootstepClips> database;
        private GroundType currentGround = GroundType.Dirt;

        public void SetGround(GroundType ground)
        {
            currentGround = ground;
        }

        public void PlayFootstep()
        {
            FootstepClips data = database.Find(x => x.groundType == currentGround);
            if (data != null && data.clips.Length > 0)
            {
                int index = Random.Range(0, data.clips.Length);
                AudioManager.Instance.PlaySFX(data.clips[index]); 
            }
        }
    }
}
