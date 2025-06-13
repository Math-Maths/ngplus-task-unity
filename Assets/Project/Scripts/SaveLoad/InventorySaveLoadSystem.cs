using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class InventorySaveLoadSystem : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/inventory.json";

    public void SaveInventory()
    {
        InventorySaveData data = new InventorySaveData();

        foreach (var slot in InventorySystem.Instance.slots)
        {
            data.slots.Add(new InventorySlotData
            {
                itemID = slot.IsEmpty ? "" : slot.item.id,
                quantity = slot.IsEmpty ? 0 : slot.quantity
            });
        }

        INPCSavable[] npcSlots = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<INPCSavable>().ToArray();
        foreach (var npc in npcSlots)
        {
            data.npcData.Add(new NPCData
            {
                npcId = npc.NPCId,
                coinsDelivered = npc.GetCurrentAmount()
            });
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Inventory saved.");
    }

    public void LoadInventory()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        for (int i = 0; i < InventorySystem.Instance.slots.Length; i++)
        {
            var slotData = data.slots[i];
            if (string.IsNullOrEmpty(slotData.itemID))
                InventorySystem.Instance.slots[i].Clear();
            else
            {
                var item = ItemDatabase.Instance.GetItemByID(slotData.itemID);
                InventorySystem.Instance.slots[i].item = item;
                InventorySystem.Instance.slots[i].quantity = slotData.quantity;
            }
        }

        InventorySystem.Instance.UpdateInventory();

        INPCSavable[] npcSlots = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<INPCSavable>().ToArray();
        foreach (var npc in npcSlots)
        {
            var found = data.npcData.Find(d => d.npcId == npc.NPCId);
            if (found != null)
                npc.SetCurrentAmount(found.coinsDelivered);
        }

        Debug.Log("Inventory and NPCs Loaded.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) SaveInventory();
        if (Input.GetKeyDown(KeyCode.F9)) LoadInventory();
    }
}

[System.Serializable]
public class InventorySlotData
{
    public string itemID;
    public int quantity;
}

[System.Serializable]
public class NPCData
{
    public string npcId;
    public int coinsDelivered;
}

[System.Serializable]
public class InventorySaveData
{
    public List<InventorySlotData> slots = new List<InventorySlotData>();
    public List<NPCData> npcData = new List<NPCData>();
}


