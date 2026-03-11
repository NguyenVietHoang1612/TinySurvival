using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRPG
{
    public class SaveGameManager : MonoBehaviour
    {
        public static SaveGameManager Instance;

        private int worldSceneIndex = 1;
        private string saveFileName = "sv002.js";

        public PlayerData currentPlayerData;

        private Player activePlayerInstance;

        [SerializeField] private ItemDatabaseObject itemData;

        [SerializeField] private bool saveGame = false;
        [SerializeField] private bool loadGame = false;

        public bool continueButtonActive = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (saveGame)
            {
                SaveGame();
                saveGame = false;
            }

            if (loadGame)
            {
                LoadGame();
                loadGame = false;
            }
        }

        public void NewGame()
        {

            currentPlayerData = new PlayerData();
            currentPlayerData.level = 0;
            currentPlayerData.currentHealth = 15;
            currentPlayerData.maxHealth = 15;
            currentPlayerData.currentExp = 0;
            currentPlayerData.positionPlayer = new float[3] { 0f, 0f, 0f };
            currentPlayerData.slots = new InventorySlot[12];

            SaveGame();
            StartCoroutine(LoadWorldSceneAndApplyData());
            Debug.Log("NewGame");
        }

        public void ContinueGame()
        {

            StartCoroutine(LoadWorldSceneAndApplyData());

            LoadGame();
        }

        // Coroutine để tải Scene và sau đó áp dụng dữ liệu
        public IEnumerator LoadWorldSceneAndApplyData()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f)
            {

                yield return null;
            }

            loadOperation.allowSceneActivation = true;

            yield return null;
        }

        public void SaveGame()
        {
            if (activePlayerInstance != null)
            {
                activePlayerInstance.SaveDataPlayer(ref currentPlayerData);
            }

            SaveSystem.SaveJson(currentPlayerData, saveFileName);

            Debug.Log("Game saved.");
        }

        public void LoadGame()
        {
            if (SaveSystem.SaveExistsJson(saveFileName))
            {
                currentPlayerData = SaveSystem.LoadJson<PlayerData>(saveFileName);

                for (int i = 0; i < currentPlayerData.slots.Length; i++)
                {
                    if (currentPlayerData.slots[i].item == null) continue;

                    Debug.Log($"Loaded item ID: {currentPlayerData.slots[i].item.name}, Quantity: {currentPlayerData.slots[i].quantity}");
                }
                if (currentPlayerData == null)
                {
                    Debug.LogWarning("Failed to load player data, initializing new data.");
                    NewGame();
                }

                Debug.Log("Game loaded.");
                if (activePlayerInstance != null)
                {
                    activePlayerInstance.LoadPlayerData(currentPlayerData);
                }
            }
            else
            {
                Debug.LogWarning("No save file found. Initializing new game data (this happens on first launch or after deleting save).");
                NewGame();
            }
        }

        public void RegisterPlayer(Player player)
        {
            if (player == null)
            {
                Debug.LogError("Attempted to register a null Player instance.");
                return;
            }

            if (activePlayerInstance != null && activePlayerInstance != player)
            {
                Debug.LogWarning("Another Player instance registered. Overwriting old reference.");
            }

            activePlayerInstance = player;
            Debug.Log("Player registered. Applying loaded/new game data.");

            ApplyPlayerDataToPlayer(activePlayerInstance);
        }

        private void ApplyPlayerDataToPlayer(Player targetPlayer)
        {
            if (targetPlayer == null || currentPlayerData == null)
            {
                Debug.LogError("Cannot apply player data: targetPlayer or currentPlayerData is null.");
                return;
            }

            targetPlayer.LoadPlayerData(currentPlayerData);
        }


        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}