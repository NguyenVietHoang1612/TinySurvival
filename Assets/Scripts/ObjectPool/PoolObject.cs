using UnityEngine;

namespace GameRPG
{
    public class PoolObject : MonoBehaviour
    {
        public ObjectPool pool;

        public void ReturnToPool()
        {
            pool.Return(gameObject);
        }
    }
}
