using UnityEngine;

namespace GameRPG
{
    public class Item_SO : ScriptableObject
    {
        public int id;
        public string name;
        public Sprite itemIcon;
        public ItemType itemType;
        [TextArea] public string itemdescription;

        public int stackSize = 25;
        public bool isStackable;

        public virtual int GetPhysicDamage() => 0;
        public virtual int GetMagicDamage() => 0;
        public virtual int GetSpeedInteract() => 0;
        public virtual int GetArmor() => 0;
        public virtual int GetMovementSpeed() => 0;

    }

    public enum ItemType
    {
        Weapon,
        Tool,
        ConsumableCraftItem,
        Building,
        Armor,
        Helmet
    }


}
