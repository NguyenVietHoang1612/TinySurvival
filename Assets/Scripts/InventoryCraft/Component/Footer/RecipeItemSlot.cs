using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameRPG.Recipe_SO;

namespace GameRPG
{
    public class RecipeItemSlot : MonoBehaviour
    {
        [Header("Recipe Item")]
        private Ingredient currentIngredient;
        [SerializeField] TMP_Text quantityRecipeText;
        [SerializeField] Image recipeItemImage;

        [field: SerializeField] private IInventory inventory;

        private void Start()
        {
            inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<IInventory>();

            if (inventory == null)
            {
                Debug.LogError("Không tìm thấy implementation của IInventory trong RecipeItemSlot!");
            }
        }

        public void Setup(Ingredient ingredient)
        {
            currentIngredient = ingredient;
            recipeItemImage.sprite = currentIngredient.item.itemIcon;

            int totalInInventory = inventory.CountItemInInventory(currentIngredient.item);
            quantityRecipeText.text = $"{totalInInventory}/{currentIngredient.amount}";
            quantityRecipeText.color = totalInInventory >= currentIngredient.amount ? Color.white : Color.red;
        }
    }
}
