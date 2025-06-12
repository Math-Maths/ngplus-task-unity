using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public event Action OnInventoryChanged;

    public InventorySlot[] slots;

    [SerializeField] private int slotCount = 20;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;

        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new InventorySlot();
    }

    public bool AddItem(InventoryItem item, int quantity = 1)
    {
        //Verify if the item is stackable
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].item == item && item.isStackable)
            {
                slots[i].quantity += quantity;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        //Add the item to a empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].item = item;
                slots[i].quantity = quantity;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        Debug.Log("Inventory is full");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= slots.Length) return;

        slots[index].Clear();
        OnInventoryChanged?.Invoke();
    }

    public void SwapItems(int indexA, int indexB)
    {
        (slots[indexA], slots[indexB]) = (slots[indexB], slots[indexA]);
        OnInventoryChanged?.Invoke();
    }

    public InventorySlot GetSlot(int index)
    {
        return slots[index];
    }
}
