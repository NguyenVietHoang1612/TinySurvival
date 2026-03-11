using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

namespace GameRPG
{
    public static class GameEvents
    {
        public static event Action<Item_SO> OnItemEquipRequested;

        public static void RequestEquip(Item_SO item)
        {
            OnItemEquipRequested?.Invoke(item);
        }
    }
}
