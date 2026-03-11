using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Item/NewItem/NewItemDatabase")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public Item_SO[] items;
        public Dictionary<Item_SO, int> GetID;
        public Dictionary<int, Item_SO> GetItem;



        public Item_SO GetItemByID(int id)
        {
            if (GetItem.ContainsKey(id))
            {
                return GetItem[id];
            }
            return null;
        }
        public void OnAfterDeserialize()
        {
            GetID = new Dictionary<Item_SO, int>();
            GetItem = new Dictionary<int, Item_SO>();
            for (int i = 0; i < items.Length; i++)
            {
                GetID.Add(items[i], i);
                GetItem.Add(i, items[i]);
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }
}
