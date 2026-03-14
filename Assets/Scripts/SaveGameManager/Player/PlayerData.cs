using System;
using System.Collections.Generic;

namespace GameRPG
{
    [Serializable]
    public class PlayerData
    {
        public int WorldSceneIndex;
        public int level;
        public int currentHealth;
        public int maxHealth;
        public int currentExp;

        public InventorySlotData[] slots = new InventorySlotData[12];
        public List<ItemDroppedData> itemsData;

        public float[] positionPlayer;
    }

    [Serializable]
    public class InventorySlotData
    {
        public int iD;
        public Item_SO item;
        public int quantity;
    }

    [Serializable]
    public class ItemDroppedData
    {
        public Item_SO item;
        public int quantity;
        public float[] position;
    }

}
