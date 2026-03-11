using System.Collections.Generic;
using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Item/Crafting/Recipe")]
    public class Recipe_SO : ScriptableObject
    {
        public string recipeName;
        public Item_SO resultItem;
        public int amountItemResult;
        public StationTag stationTag;

        public List<Ingredient> ingredients = new List<Ingredient>();



        [System.Serializable]
        public class Ingredient
        {
            public Item_SO item;
            public int amount;
        } 
    }

    public enum StationTag
    {
        None,
        Anvil,
        Furnace,
        Workbench
    }
}
