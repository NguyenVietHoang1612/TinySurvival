using UnityEngine;

namespace GameRPG
{
    public class GameInitializer : MonoBehaviour
    {
        void Start()
        {
            Player player = GameObject.Find("PlayerRoot").GetComponent<Player>();

            InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

            if (SaveGameManager.Instance != null && SaveGameManager.Instance.continueButtonActive)
            {
                PlayerData data = SaveGameManager.Instance.currentPlayerData;

                player.LoadPlayerData(data);
            }
        }


    }
}
