using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameRPG
{
    public class ProjectileMotion : MonoBehaviour
    {
        [SerializeField] private float forceFire = 45;
        [SerializeField] private float arcHeight = 0.5f;

        [SerializeField] private GameObject bulletPrefab;

        private void Start()
        {
            ObjectPoolManager.Instance.CreatePool(bulletPrefab, 10);
        }

        public void Fire(Transform firePoint, Transform target)
        {
            if (bulletPrefab == null) return;

            GameObject obj = ObjectPoolManager.Instance.GetPool(bulletPrefab);
            obj.transform.position = firePoint.position;
            obj.transform.rotation = Quaternion.identity;

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            if (rb == null) return;

            Vector2 dir = target.position - firePoint.position;

            rb.AddForce(new Vector2(dir.x, dir.y + arcHeight) * forceFire, ForceMode2D.Impulse);
        }
    }
}
