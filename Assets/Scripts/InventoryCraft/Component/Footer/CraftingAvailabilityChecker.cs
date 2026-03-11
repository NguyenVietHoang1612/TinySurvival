using UnityEngine;

namespace GameRPG
{
    public class CraftingAvailabilityChecker : Singleton<CraftingAvailabilityChecker>
    {
        [SerializeField] private PlayerProximitySensor proximitySensor;


        private CraftSystemManager craftSystemManager;

        protected override void Awake()
        {
            base.Awake();
            if (craftSystemManager == null)
                craftSystemManager = FindFirstObjectByType<CraftSystemManager>();
        }

        public CraftingState GetRecipeState(Recipe_SO recipe)
        {
            if (recipe.stationTag != StationTag.None && proximitySensor.CurrentStationTag != recipe.stationTag)
            {
                return CraftingState.LockedByStation;
            }

            bool hasEnoughIngredients = craftSystemManager.CanCraft(recipe);

            if (!hasEnoughIngredients)
            {
                return CraftingState.MissingIngredients;
            }

            return CraftingState.Available;
        }
    }

    public enum CraftingState
    {
        Available,         
        LockedByStation,   
        MissingIngredients 
    }
}
