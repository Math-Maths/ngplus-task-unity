using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public event Action OnInventoryChanged;

    public InventorySlot[] slots;

    [SerializeField] private int slotCount = 20;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject interactText;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new InventorySlot();

        interactText.SetActive(false);
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

    public void UpdateInventory()
    {
        OnInventoryChanged?.Invoke();
    }


    public void ToggleInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            OnInventoryChanged?.Invoke();
        }
    }

    public void MoveItem(int from, int to)
    {
        if (from < 0 || to < 0 || from >= slots.Length || to >= slots.Length)
            return;

        InventorySlot source = slots[from];
        InventorySlot target = slots[to];

        if (source.IsEmpty)
            return;

        // Mesma stackável → somar
        if (!target.IsEmpty && target.item == source.item && source.item.isStackable)
        {
            target.quantity += source.quantity;
            source.Clear();
        }
        else // trocar
        {
            var tempItem = target.item;
            var tempQty = target.quantity;

            target.Set(source.item, source.quantity);
            source.Set(tempItem, tempQty);
        }

        OnInventoryChanged?.Invoke();
    }

    public void ShowInteractText(bool state)
    {
        interactText.SetActive(state);
    }

}
