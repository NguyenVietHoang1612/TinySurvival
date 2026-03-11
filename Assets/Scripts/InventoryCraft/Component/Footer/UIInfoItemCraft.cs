using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameRPG
{
    public class UIInfoItemCraft : MonoBehaviour
    {
        [SerializeField] private CraftSystemManager craftSystemManager;
        [SerializeField] private UI_CraftingManager ui_CraftingManager;    

        [Header("UI Components")]
        [SerializeField] private TMP_Text nameItem;
        [SerializeField] private TMP_Text decriptionItem;
        [SerializeField] private Image itemSprite;
        [SerializeField] private Button craftButton;

        [Header("Ingredients")]
        [SerializeField] private Transform RecipeItemSlotContainer;
        [SerializeField] private List<RecipeItemSlot> ingredientUIItems = new List<RecipeItemSlot>();

        private Recipe_SO currentRecipe;

        private void Start()
        {
            foreach (Transform child in RecipeItemSlotContainer)
            {
                if (child.TryGetComponent<RecipeItemSlot>(out var slot))
                {
                    ingredientUIItems.Add(slot);
                }
            }
        }

        private void OnEnable()
        {
            IVCraftSlot.OnAnySlotClicked += InitializedInfo;
        }

        private void OnDisable()
        {
            IVCraftSlot.OnAnySlotClicked -= InitializedInfo;
        }

        public void InitializedInfo(Recipe_SO recipe)
        {
            currentRecipe = recipe;

            nameItem.text = recipe.resultItem.name;
            decriptionItem.text = recipe.resultItem.itemdescription;
            itemSprite.sprite = recipe.resultItem.itemIcon;
            ShowIngredients(recipe);

            UpdateCraftButton();
        }

        private void ShowIngredients(Recipe_SO recipe)
        {
            for (int i = 0; i < ingredientUIItems.Count; i++)
            {
                if (i < recipe.ingredients.Count)
                {
                    ingredientUIItems[i].gameObject.SetActive(true);
                    ingredientUIItems[i].Setup(recipe.ingredients[i]);
                }
                else
                {
                    ingredientUIItems[i].gameObject.SetActive(false);
                }
            }
        }

        public void UpdateCraftButton()
        {
            if (currentRecipe == null) return;

            CraftingState state = CraftingAvailabilityChecker.Instance.GetRecipeState(currentRecipe);
            craftButton.interactable = (state == CraftingState.Available);
        }

        public void OnCraftButtonClicked()
        {
            if (currentRecipe != null)
            {
                craftSystemManager.Craft(currentRecipe);

                InitializedInfo(currentRecipe);

                ui_CraftingManager.RefreshUI();
            }

        }
    }
}
