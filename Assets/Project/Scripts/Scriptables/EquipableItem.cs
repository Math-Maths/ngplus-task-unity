using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/EquipableItem")]
public class EquipableItem : InventoryItem
{
    public string equipSlot;
    public GameObject prefab;

    public override void Use(PlayerInventory playerInventory)
    {
        playerInventory.Equip(this);
        Debug.Log($"{itemName} equipado no slot {equipSlot}.");
    }
}
