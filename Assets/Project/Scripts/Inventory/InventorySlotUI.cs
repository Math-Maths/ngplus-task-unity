using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text quantityText;
    public Button button;

    private int index;

    public void Init(int i)
    {
        index = i;
        button.onClick.AddListener(OnClick);
        UpdateUI();
    }

    public void UpdateUI()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (slot.IsEmpty)
        {
            iconImage.enabled = false;
            quantityText.text = "";
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;
            quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
        }
    }

    private void OnClick()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);
        if (!slot.IsEmpty)
        {
            Debug.Log("Usando item: " + slot.item.itemName);

            // Aqui podemos aplicar o efeito (ex: curar)
            if (slot.item.itemType == ItemType.Consumable)
            {
                InventorySystem.Instance.RemoveItem(index);
            }
        }
    }
}
