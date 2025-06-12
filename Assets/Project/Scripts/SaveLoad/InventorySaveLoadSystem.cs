using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class InventorySaveLoadSystem : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/inventory.json";

    public void SaveInventory()
    {
        InventorySaveData data = new InventorySaveData();

        foreach (var slot in InventorySystem.Instance.slots)
        {
            if (!slot.IsEmpty)
            {
                data.slots.Add(new InventorySlotData
                {
                    itemID = slot.item.id,
                    quantity = slot.quantity
                });
            }
            else
            {
                data.slots.Add(new InventorySlotData
                {
                    itemID = "",
                    quantity = 0
                });
            }
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Inventory path " + savePath);
    }

    public void LoadInventory()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        for (int i = 0; i < InventorySystem.Instance.slots.Length; i++)
        {
            InventorySlotData slotData = data.slots[i];

            if (string.IsNullOrEmpty(slotData.itemID))
            {
                InventorySystem.Instance.slots[i].Clear();
            }
            else
            {
                InventoryItem item = ItemDatabase.Instance.GetItemByID(slotData.itemID);
                InventorySystem.Instance.slots[i].item = item;
                InventorySystem.Instance.slots[i].quantity = slotData.quantity;
            }
        }

        InventorySystem.Instance.UpdateInventory();
        Debug.Log("Inventory Loaded.");
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.F5))
        SaveInventory();

    if (Input.GetKeyDown(KeyCode.F9))
        LoadInventory();
}
}

[System.Serializable]
public class InventorySlotData
{
    public string itemID;
    public int quantity;
}

[System.Serializable]
public class InventorySaveData
{
    public List<InventorySlotData> slots = new List<InventorySlotData>();
}


