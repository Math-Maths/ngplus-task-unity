using UnityEngine;

public enum ItemType
{
    Generic,
    QuestItem,
    Consumable,
    Equipment
}

public abstract class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public bool isStackable;
    public int maxStack = 1;

    public abstract void Use(PlayerInventory playerInventory);
}
