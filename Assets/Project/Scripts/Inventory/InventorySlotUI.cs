using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject slotOption;
    [SerializeField] private TMP_Text equipUse;

    private Button button;
    private int index;
    private Image slotImage;

    public void Init(int i)
    {
        slotImage = GetComponent<Image>();
        button = GetComponent<Button>();

        index = i;
        button.onClick.AddListener(OnClick);
        UpdateUI();
    }

    public void UpdateUI()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (slot.IsEmpty)
        {
            DesactivateSlot();
        }
        else
        {
            ActivateSlot(slot);
        }
    }

    private void DesactivateSlot()
    {
        slotImage.color = new Color(0f, 0f, 75f, 75f);
        iconImage.enabled = false;
        quantityText.text = "";
    }

    private void ActivateSlot(InventorySlot slot)
    {
        slotImage.color = Color.white;
        iconImage.enabled = true;
        iconImage.sprite = slot.item.icon;
        quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
    }

    private void OnClick()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);
        if (!slot.IsEmpty)
        {
            equipUse.text = slot.item.itemType == ItemType.Consumable ? "use" : "equip";

            slotOption.SetActive(true);
        }
    }
}
