using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/EquipableItem")]
public class EquipableItem : InventoryItem
{
    public GameObject handPrefab;

    public override void Use(PlayerInventory playerInventory)
    {
        playerInventory.Equip(this);
    }
}
