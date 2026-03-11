using UnityEngine;

namespace GameRPG
{
    public class PlayerProximitySensor : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private LayerMask stationLayer;

        [field: SerializeField] public StationTag CurrentStationTag { get; private set; } = StationTag.None;

        private void Update()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, stationLayer);
            if (hitColliders.Length > 0)
            {
                var station = hitColliders[0].GetComponent<CraftingStation>();
                if (station != null)
                {
                    Debug.Log($"Player is near station: {station.StationTag}");
                    CurrentStationTag = station.StationTag;
                    return;
                }
            }

            CurrentStationTag = StationTag.None;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
