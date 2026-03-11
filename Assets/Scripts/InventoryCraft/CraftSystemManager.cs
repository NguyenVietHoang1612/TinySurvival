using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameRPG
{
    public class CraftSystemManager : MonoBehaviour
    {
        [SerializeField] private List<Recipe_SO> allRecipeSO;

        public IEnumerable<Recipe_SO> AllRecipeSO => allRecipeSO;

        private IInventory inventory;
        
        private void Start()
        {
            inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<IInventory>();

            if (inventory == null)
            {
                Debug.LogError("Không tìm thấy implementation của IInventory!");
            }
        }

        public bool CanCraft(Recipe_SO recipe)
        {
            if (inventory == null) return false;

            foreach (var ing in recipe.ingredients)
            {
                if (!inventory.HasItem(ing.item, ing.amount))
                {
                    return false;
                }

            }
            return true;
        }

        public void Craft(Recipe_SO recipe)
        {
            if (!CanCraft(recipe)) return;

            foreach (var ing in recipe.ingredients)
            {
                inventory.RemoveItem(ing.item, ing.amount);
            }

            inventory.AddItem(recipe.resultItem, recipe.amountItemResult);

            Debug.Log($"Che tao thanh cong: {recipe.resultItem.name} - {recipe.amountItemResult}");;
        }
    }


}
