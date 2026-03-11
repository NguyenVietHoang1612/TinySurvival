using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        private Dictionary<GameObject, ObjectPool> objectPool = new();
        public void CreatePool(GameObject prefab, int initialSize)
        {
            if (objectPool.ContainsKey(prefab)) return;

            var pool = new ObjectPool(prefab, transform, initialSize);
            objectPool.Add(prefab, pool);
        }

        public GameObject GetPool(GameObject prefab)
        {
            if (!objectPool.ContainsKey(prefab))
            {
                CreatePool(prefab, 5);
            }

            return objectPool[prefab].Get();
        }

        public void Return(GameObject prefab, GameObject obj)
        {
            if (!objectPool.ContainsKey(prefab))
            {
                Debug.Log("Không có key pool");
                return;
            }
            objectPool[prefab].Return(obj);
        }
    }
}
