using UnityEngine;

namespace GameRPG
{
    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        public float spawnInterval = 2f;
        public Transform[] spawnPoints;

        private float timer;
        [SerializeField] int amountSpawn = 2;

        private void Start()
        {
            ObjectPoolManager.Instance.CreatePool(prefabToSpawn, 10);
            SpawnObject();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnObject();
            }
        }

        private void SpawnObject()
        {
            if (spawnPoints.Length <= 0) return;

            int randomAmout = Random.Range(1, amountSpawn);

            for (int i = 1; i <= randomAmout; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPosition = spawnPoints[index].position;

                GameObject obj = ObjectPoolManager.Instance.GetPool(prefabToSpawn);
                obj.transform.SetParent(transform);
                obj.transform.position = spawnPosition;
                spawnInterval += 10f;
            }

        }
    }
}
