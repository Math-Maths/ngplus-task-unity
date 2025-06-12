using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ConsumableItem")]
public class ConsumableItem : InventoryItem
{
    public int healAmount;

    public override void Use(PlayerInventory playerInventory)
    {
        playerInventory.Consume(this);
    }
}
