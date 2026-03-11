using System;
using System.Collections.Generic;

using UnityEngine;

namespace GameRPG
{
    public class DroppedItem : MonoBehaviour
    {
        [SerializeField] GameObject itemPrefab;
        public List<LootItem> droppedItems = new List<LootItem>();


        [Header("Random Spawn Settings")]
        [SerializeField] private bool spawnOnStart = true;
        [SerializeField] private int totalItemsToSpawn = 50;
        [SerializeField] private Vector2 spawnAreaMin;
        [SerializeField] private Vector2 spawnAreaMax;
        [SerializeField] private LootChance[] randomPoolChance;


        public static event Action OnSpawnItem;

        [Header("Debug")]
        [SerializeField] private LayerMask obstacle;

        private void OnEnable()
        {
            GoblinStatsManage.DropItemOnMonsterDefeated += DropArrayItemLoot;
            InventoryManager.DropLootInventory += DropLoot;
            ResourceStatsManager.OnDropItemOnResourceDefeated += DropArrayItemLoot;
            PlayerInteract.LootDataItem += RemoveFromDroppedList;
        }

        private void OnDisable()
        {
            GoblinStatsManage.DropItemOnMonsterDefeated -= DropArrayItemLoot;
            InventoryManager.DropLootInventory -= DropLoot;
            ResourceStatsManager.OnDropItemOnResourceDefeated -= DropArrayItemLoot;
            PlayerInteract.LootDataItem -= RemoveFromDroppedList;
        }

        private void Start()
        {
            ObjectPoolManager.Instance.CreatePool(itemPrefab, 50);

            if (SaveGameManager.Instance != null && SaveGameManager.Instance.continueButtonActive)
            {
                LoadItemDropped();
            }
            else if (spawnOnStart)
            {
                Debug.Log("SpawnItem");
                SpawnRandomItemsWorld();
            }

            GetItemInWorld();
        }


        public Item_SO GetRandomItem()
        {
            if (randomPoolChance == null || randomPoolChance.Length <= 0) return null;

            int totalChance = 0;

            foreach (var lootChance in randomPoolChance)
            {
                totalChance += lootChance.dropWeight;
            }

            var randomValue = UnityEngine.Random.Range(0, totalChance);
            int currentChance = 0;

            foreach (var lootChance in randomPoolChance)
            {
                currentChance += lootChance.dropWeight;
                if (currentChance >= randomValue)
                {
                    return lootChance.itemSO;
                }
            }
            return null;

        }

        public void LoadItemDropped()
        {
            if (SaveGameManager.Instance != null && SaveGameManager.Instance.continueButtonActive)
            {

                PlayerData playerData = SaveGameManager.Instance.currentPlayerData;
                droppedItems.Clear();
                for (int i = 0; i < playerData.itemsData.Count; i++)
                {
                    if (playerData.itemsData[i].item != null)
                    {
                        Vector3 pos = new Vector3(
                            playerData.itemsData[i].position[0],
                            playerData.itemsData[i].position[1],
                            playerData.itemsData[i].position[2]);
                        Item_SO itemSO = playerData.itemsData[i].item;

                        GameObject lootItemGO = ObjectPoolManager.Instance.GetPool(itemPrefab);
                        lootItemGO.transform.position = pos;
                        lootItemGO.transform.SetParent(transform);
                        lootItemGO.transform.rotation = Quaternion.identity;

                        LootItem lootItem = lootItemGO.GetComponent<LootItem>();

                        if (lootItem != null)
                        {
                            lootItem.SetItem(itemSO);
                            lootItem.SetQuantity(playerData.itemsData[i].quantity);
                            lootItem.Initialize(itemSO, playerData.itemsData[i].quantity);
                        }
                        else
                        {
                            Debug.LogWarning("LootItem component not found on the instantiated item prefab.");
                        }
                    }

                }
            }
        }

        public void SpawnRandomItemsWorld()
        {
            if (randomPoolChance == null || randomPoolChance.Length == 0) return;

            int itemsSpawned = 0;
            int maxAttempts = totalItemsToSpawn; 
            int attempts = 0;

            while (itemsSpawned < totalItemsToSpawn && attempts < maxAttempts)
            {
                attempts++;
                float randomX = UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float randomY = UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                Vector2 randomPos = new Vector2(randomX, randomY);

                Collider2D hit = Physics2D.OverlapCircle(randomPos, 0.2f, obstacle);

                if (hit == null)
                {
                    Item_SO randomSO = GetRandomItem();
                    DropLoot(randomSO, 1, randomPos);
                    itemsSpawned++;
                }
            }

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Không tìm đủ vị trí đất để spawn item. Đã dừng sau " + maxAttempts + " lần thử.");
            }
        }

        public void SetItemDroppedInWorld()
        {
            GetItemInWorld();
        }

        public void GetItemInWorld()
        {
            droppedItems.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                var lootItem = transform.GetChild(i).GetComponent<LootItem>();
                if (lootItem.Quantity <= 0 || lootItem == null) continue;

                droppedItems.Add(lootItem);
            }
        }


        public void DropLoot(Item_SO itemSO, int quantity, Vector2 positonItemDrop)
        {
            if (quantity <= 0) return;

            var itemGO = ObjectPoolManager.Instance.GetPool(itemPrefab);

            LootItem loot = itemGO.GetComponent<LootItem>();
            Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();

            loot.transform.SetParent(transform);
            loot.transform.position = positonItemDrop;
            loot.transform.rotation = Quaternion.identity;

            loot.SetItem(itemSO);
            loot.SetOriginPrefab(itemPrefab);

            rb.simulated = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            Vector2 force = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(2f, 4f);
            rb.AddForce(force, ForceMode2D.Impulse);

            OnSpawnItem?.Invoke();

            loot.Initialize(itemSO, quantity);
        }


        public void DropArrayItemLoot(Item_SO[] itemSOs, int quantity, Vector2 positonItemDrop)
        {
            foreach (var item in itemSOs)
            {
                for (int i = 0; i < quantity; i++)
                {
                    Item_SO randomItem = itemSOs[UnityEngine.Random.Range(0, itemSOs.Length)];
                    Vector2 randomPos = positonItemDrop + UnityEngine.Random.insideUnitCircle * 0.3f;

                    DropLoot(randomItem, 1, randomPos);
                }
            }
        }

        public void SaveItemDropped(ref PlayerData playerData)
        {
            SetItemDroppedInWorld();
            for (int i = 0; i < droppedItems.Count; i++)
            {
                playerData.itemsData.Add(new ItemDroppedData()
                {
                    item = droppedItems[i].Item,
                    quantity = droppedItems[i].Quantity,
                    position = new float[3]
                    {
                        droppedItems[i].transform.position.x,
                        droppedItems[i].transform.position.y,
                        droppedItems[i].transform.position.z
                    }
                });
            }
        }

        private void RemoveFromDroppedList(Item_SO so, int qty)
        {
            droppedItems.RemoveAll(item => item.Quantity <= 0 || !item.gameObject.activeInHierarchy);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, (spawnAreaMin.y + spawnAreaMax.y) / 2, 0);
            Vector3 size = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 1);
            Gizmos.DrawWireCube(center, size);
        }
    }

    [Serializable]
    public class LootChance
    {
        public Item_SO itemSO;
        public int dropWeight;
    }
}
