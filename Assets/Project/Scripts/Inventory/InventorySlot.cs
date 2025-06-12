using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public InventoryItem item;
    public int quantity;

    public bool IsEmpty => item == null;

    public void Clear()
    {
        item = null;
        quantity = 0;
    }

    public void ReduceAmount()
    {
        if (quantity == 1)
        {
            Clear();
        }
        else if (quantity > 1)
        {
            quantity -= 1;
        }
        else
        {
            Clear();
        }
    }
}
