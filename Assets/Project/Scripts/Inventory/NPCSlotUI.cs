using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCSlotUI : MonoBehaviour, IDropHandler, INPCSavable
{
    [SerializeField] private InventoryItem acceptedItem;
    [SerializeField] private int requiredAmount = 10;
    [SerializeField] private int currentAmount = 0;
    [SerializeField] private string npcId;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;

    private int currentCoins;
    private Image slotImage;

    public string NPCId => npcId;
    private InventorySlot npcSlot = new InventorySlot(); // Apenas 1 npcSlot
    public InventorySlot Slot => npcSlot;

    public int GetCurrentAmount() => currentCoins;

    public void SetCurrentAmount(int amount)
    {
        currentCoins = amount;
        UpdateUI();
    }


    private void OnEnable()
    {
        Debug.Log("Aconteceu");
        slotImage = GetComponent<Image>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (npcSlot.IsEmpty)
        {
            slotImage.color = new Color(.84f, .84f, .84f, .75f);
            iconImage.enabled = false;
            quantityText.text = "";
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = npcSlot.item.icon;
            quantityText.text = npcSlot.item.isStackable ? npcSlot.quantity.ToString() : "";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!InventoryDragHandler.Instance.IsDragging) return;

        int fromIndex = InventoryDragHandler.Instance.DraggedFromIndex;
        var sourceSlot = InventorySystem.Instance.GetSlot(fromIndex);

        if (sourceSlot.IsEmpty) return;

        if (npcSlot.IsEmpty || npcSlot.item.itemType != ItemType.Generic)
        {
            Debug.Log("Apenas moedas são aceitas aqui!");
            return;
        }

        if (!npcSlot.IsEmpty && npcSlot.item == sourceSlot.item && npcSlot.item.isStackable)
        {
            npcSlot.quantity += sourceSlot.quantity;
            sourceSlot.Clear();
        }
        else if (npcSlot.IsEmpty)
        {
            npcSlot.item = sourceSlot.item;
            npcSlot.quantity = sourceSlot.quantity;
            sourceSlot.Clear();
        }

        InventorySystem.Instance.UpdateInventory();
        UpdateUI();
    }
}
