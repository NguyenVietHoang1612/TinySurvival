using UnityEngine;

namespace GameRPG
{
    public class YSort : MonoBehaviour
    {
        private int _baseSortingOrder;
        [SerializeField] private SortableSprite[] _sortableSprites;
        [SerializeField] private Transform _sortOffsetMarker;

        private void OnEnable()
        {
            RefreshSort();
            DroppedItem.OnSpawnItem += RefreshSort;

        }

        private void OnDisable()
        {
            DroppedItem.OnSpawnItem -= RefreshSort;
        }

        public void RefreshSort()
        {
            _baseSortingOrder = _sortOffsetMarker.GetSortingOrder();
            foreach (var sortableSprite in _sortableSprites)
            {
                sortableSprite.SpriteRenderer.sortingOrder = _baseSortingOrder + sortableSprite.SortingOrder;
            }
        }
    }
}
