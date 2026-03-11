using System;
using UnityEngine;

namespace GameRPG
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private GameObject currentPrefabPlacement;
        [SerializeField] private GameObject placementPreviewPrefab;
        [SerializeField] private LayerMask placementLayer;
        [SerializeField] private Camera mainCamera;
        [SerializeField] Grid grid;

        private GridData gridData;
        private BuildingData currentBuildingData;
        private bool isPlacing = false;

        private SpriteRenderer previewSpriteRenderer;
        private IInventory inventory;

        [SerializeField] private LayerMask ObstacleLayerMask;

        private void Start()
        {
            inventory = FindFirstObjectByType<InventoryManager>();
            gridData = new GridData();
            if (inventory == null)
            {
                Debug.LogError("InventoryManager in PlacementSystem not found in the scene.");
            }
        }

        private void OnEnable()
        {
            InventorySlots.OnBuildingItemClicked += StartPlacement;
        }

        private void OnDisable()
        {
            InventorySlots.OnBuildingItemClicked -= StartPlacement;
        }

        private void Update()
        {

            if (isPlacing && placementPreviewPrefab != null)
            {

                Vector3 mousePos = GetMouseWorldPosition();
                Vector3Int mouseGridPos = grid.WorldToCell(mousePos);
                placementPreviewPrefab.transform.position = grid.GetCellCenterWorld(mouseGridPos);

                bool canPlace = CheckPlacementValidate(mouseGridPos, currentBuildingData);

                if (canPlace == false)
                {
                    Color redColor = Color.red;
                    redColor.a = 0.7f;
                    previewSpriteRenderer.color = redColor;
                }
                else
                {
                    Color previewColor = Color.white;
                    previewColor.a = 0.7f;
                    previewSpriteRenderer.color = previewColor;

                }

                if (PlayerInputManager.Instance.LeftMouse_Input)
                {
                    PlayerInputManager.Instance.UseMouseLeftInput();
                    PlaceObject();
                }

                if (PlayerInputManager.Instance.RightMouse_Input)
                {
                    PlayerInputManager.Instance.UseMouseRightInput();
                    StopPlacement();
                }

                
            }
        }

        public void StartPlacement(BuildingData buildingData)
        {
            if (placementPreviewPrefab != null)
            {
                Destroy(placementPreviewPrefab);
                placementPreviewPrefab = null;
            }

            currentBuildingData = buildingData;
            currentPrefabPlacement = buildingData.prefab;

            if (currentPrefabPlacement != null)
            {
                Vector3 mousePos = GetMouseWorldPosition();
                Vector3Int mouseGridPos = grid.WorldToCell(mousePos);

                placementPreviewPrefab = Instantiate(currentPrefabPlacement);
                placementPreviewPrefab.GetComponent<Collider2D>().enabled = false;
                previewSpriteRenderer = placementPreviewPrefab.GetComponentInChildren<SpriteRenderer>();
                isPlacing = true;
                placementPreviewPrefab.transform.position = grid.GetCellCenterWorld(mouseGridPos);
            }
        }

        public Vector3 GetMouseWorldPosition()
        {
            Vector2 mouseVec2 = mainCamera.ScreenToWorldPoint(PlayerInputManager.Instance.mousePositionAction);

            RaycastHit2D hit = Physics2D.Raycast(mouseVec2, Vector2.zero, 0f, placementLayer);

            if (hit.collider != null)
            {
                return hit.point;
            }

            return mouseVec2;
        }

        private bool CheckPlacementValidate(Vector3Int gridPos, BuildingData buildingData)
        {
            bool canPlace = gridData.CanPlaceObject(new Vector2Int(gridPos.x, gridPos.y), buildingData.size);

            Collider2D collider = Physics2D.OverlapCircle(grid.GetCellCenterWorld(gridPos), 1f, ObstacleLayerMask);

            return collider == null && canPlace;
        }

        private void PlaceObject()
        {
            Vector3 mousePos = GetMouseWorldPosition();
            Vector3Int mouseGridPos = grid.WorldToCell(mousePos);
            bool canPlace = CheckPlacementValidate(mouseGridPos, currentBuildingData);

            if (!canPlace)
            {
                Debug.Log("Khong the dat object o vi tri nay.");
                return;
            }

            Instantiate(currentPrefabPlacement, placementPreviewPrefab.transform.position, Quaternion.identity);
            
            gridData.AddObjectToGrid(new Vector2Int(mouseGridPos.x, mouseGridPos.y), currentBuildingData.size, currentBuildingData.id);

            if (inventory != null)
            {
                inventory.RemoveItem(currentBuildingData, 1);

                if (!inventory.HasItem(currentBuildingData,1))
                {
                    StopPlacement();
                    return;
                }
            }
        }

        public void StopPlacement()
        {
            isPlacing = false;
            currentBuildingData = null;

            if (placementPreviewPrefab != null)
            {
                previewSpriteRenderer = null;
                Destroy(placementPreviewPrefab);
            }
        }
    }
}
