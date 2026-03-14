using UnityEngine;

namespace GameRPG
{
    [CreateAssetMenu(fileName = "New DataStatsCharacter ", menuName = "Character/NewStatsCharacter/NewStats")]
    public class BaseStatsCharacter : ScriptableObject
    {
        public int baseHealth;
        public int baseHunger;
        public float baseHungerDecreaseRate;
        public int baseDamage;
        public int baseArmor;
        public int baseMagicDamage;
        public int baseSpeedInteract;
        public float baseMovementSpeed;
    }
}
