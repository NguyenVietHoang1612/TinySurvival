using System;
using UnityEngine;

namespace GameRPG
{
    public class DynamicYSort : MonoBehaviour
    {
        private int _baseSortingOrder;
        [SerializeField] private SortableSprite[] _sortableSprites;
        [SerializeField] private Transform _sortOffsetMarker;

        private void Update()
        {
            _baseSortingOrder = _sortOffsetMarker.GetSortingOrder();

            foreach (var sortableSprite in _sortableSprites)
            {
                sortableSprite.SpriteRenderer.sortingOrder =
                    _baseSortingOrder + sortableSprite.SortingOrder;
            }
        }
    }

    [Serializable]
    public struct SortableSprite
    {
        public SpriteRenderer SpriteRenderer;
        public int SortingOrder;
    }
}
