[System.Serializable]
public class InventorySlot
{
    public InventoryItem item;
    public int quantity;
    public string id;

    public bool IsEmpty => item == null;

    public void Clear()
    {
        item = null;
        quantity = 0;
    }

    public void ReduceAmount(int amount = 1)
    {
        quantity -= amount;
        if (quantity <= 0)
        {
            Clear();
        }

        InventorySystem.Instance.UpdateInventory();
    }

    public void Set(InventoryItem newItem, int newQuantity)
    {
        item = newItem;
        quantity = newQuantity;

        InventorySystem.Instance.UpdateInventory();
    }
}
