using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    public List<InventoryItem> allItems;

    private Dictionary<string, InventoryItem> itemDict = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;

        foreach (var item in allItems)
        {
            itemDict[item.id] = item;
        }
    }

    public InventoryItem GetItemByID(string id)
    {
        return itemDict.TryGetValue(id, out var item) ? item : null;
    }
}
