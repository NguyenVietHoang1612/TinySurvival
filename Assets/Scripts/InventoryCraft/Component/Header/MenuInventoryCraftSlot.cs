using UnityEngine;

namespace GameRPG
{
    public class MenuInventoryCraftSlot : MonoBehaviour
    {
        [SerializeField] IVCraftSlot[] ivListCraftSlot;
        [SerializeField] ItemType itemType;

       
        //public void UpdateListMenuSLot()
        //{

        //    foreach (IVCraftSlot slot in ivListCraftSlot)
        //    {
        //        if (slot.Recipe.resultItem.itemType == itemType)
        //        {
        //            slot.gameObject.SetActive(true);
        //        }
        //        else
        //        {
        //            slot.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}
