using UnityEngine;

public enum ItemType
{
    Generic,
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
    public GameObject prefab;
    public string id;

    public abstract void Use(PlayerInventory playerInventory);
}
