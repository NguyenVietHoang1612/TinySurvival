using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameRPG
{
    public class IVCraftSlot : MonoBehaviour
    {
        private Recipe_SO currentRecipe;
        [SerializeField] Image itemSprite;
        [SerializeField] GameObject lockIcon;
        private CanvasGroup canvasGroup;

        public static event Action<Recipe_SO> OnAnySlotClicked;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void InitializedIVCraft(Recipe_SO newRecipe_SO)
        {
            currentRecipe = newRecipe_SO;
            itemSprite.sprite = newRecipe_SO.resultItem.itemIcon;
            itemSprite.gameObject.SetActive(false);
            itemSprite.gameObject.SetActive(true);
            UpdateSlotUI();
        }

        public void UpdateSlotUI()
        {
            CraftingState state = CraftingAvailabilityChecker.Instance.GetRecipeState(currentRecipe);

            switch (state)
            {
                case CraftingState.Available:
                    canvasGroup.alpha = 1.0f;
                    lockIcon.SetActive(false);
                    break;

                case CraftingState.LockedByStation:
                    canvasGroup.alpha = 0.5f;
                    lockIcon.SetActive(true);
                    canvasGroup.interactable = false;
                    break;

                case CraftingState.MissingIngredients:
                    canvasGroup.alpha = 0.5f;
                    lockIcon.SetActive(false);
                    break;
            }
        }


        public void OnPointerUpdate()
        {
            OnAnySlotClicked?.Invoke(currentRecipe);
        }
    }
}
