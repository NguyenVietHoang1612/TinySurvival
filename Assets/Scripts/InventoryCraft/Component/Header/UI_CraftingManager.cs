using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameRPG
{
    public class UI_CraftingManager : MonoBehaviour
    {
        [SerializeField] private CraftSystemManager craftSystemManager; 


        [SerializeField] private GameObject recipeSlotPrefab; 
        [SerializeField] private Transform contentParent;      

        private List<IVCraftSlot> spawnedSlots = new List<IVCraftSlot>();

        private void Start()
        {
            GenerateCraftingList();
        }

        public void GenerateCraftingList()
        {
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }
            spawnedSlots.Clear();

            foreach (Recipe_SO recipe in craftSystemManager.AllRecipeSO)
            {
                GameObject newSlotGO = Instantiate(recipeSlotPrefab, contentParent);

                IVCraftSlot slotScript = newSlotGO.GetComponent<IVCraftSlot>();
                slotScript.InitializedIVCraft(recipe);

                spawnedSlots.Add(slotScript);
            }
        }

        public void RefreshUI()
        {
            foreach (var slot in spawnedSlots)
            {
                slot.UpdateSlotUI();
            }
        }
    }
}
