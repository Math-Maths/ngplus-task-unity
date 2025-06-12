using UnityEngine;

public enum ItemType
{
    Generic,
    QuestItem,
    Consumable,
    Equipment
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public abstract class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public bool isStackable;
    public int maxStack = 1;
}
