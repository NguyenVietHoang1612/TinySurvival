
using System;
using UnityEngine;

namespace GameRPG
{
    [Serializable]
    public class ItemInWorldData
    {
        public Item_SO item;
        public int quantity;
        public float[] position;

        public ItemInWorldData(Item_SO item, Vector3 pos)
        {
            position = new float[3];
            position[0] = pos.x;
            position[1] = pos.y;
            position[2] = pos.z;

            this.item = item;
            quantity = 1;
        }

    }

    [Serializable]
    public class ItemInWorld
    {
        public ItemInWorldData[] itemInWorldDatas;
    }

}
