namespace GameRPG
{
    public interface IInventory
    {
        public bool HasItem(Item_SO itemSO, int quantity);
        public int CountItemInInventory(Item_SO item);
        public void AddItem(Item_SO itemSO, int quantity);
        public void RemoveItem(Item_SO itemSO, int quantity);
    }
}
