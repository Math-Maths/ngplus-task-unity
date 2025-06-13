using UnityEngine;
using UnityEngine.EventSystems;

public class NPCSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryItem acceptedItem;
    [SerializeField] private int requiredAmount = 10;
    [SerializeField] private int currentAmount = 0;

    public void OnDrop(PointerEventData eventData)
    {
        if (!InventoryDragHandler.Instance.IsDragging) return;

        int fromIndex = InventoryDragHandler.Instance.DraggedFromIndex;
        var slot = InventorySystem.Instance.GetSlot(fromIndex);

        if (slot.IsEmpty || slot.item.itemType != ItemType.Generic)
        {
            Debug.Log("Apenas moedas são aceitas aqui!");
            return;
        }

        // Transferência: retirar do inventário do jogador
        currentAmount += slot.quantity;
        slot.Clear();
        InventorySystem.Instance.UpdateInventory();

        Debug.Log($"Moedas entregues: {currentAmount}/{requiredAmount}");

        if (currentAmount >= requiredAmount)
        {
            Debug.Log("Recompensa disponível!");
            // Acionar evento, dar item, etc.
        }
    }

    // Salvar valores relevantes no save
    public int GetCurrentAmount() => currentAmount;
    public void SetCurrentAmount(int value) => currentAmount = value;
}
