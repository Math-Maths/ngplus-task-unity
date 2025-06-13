using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject slotOption;
    [SerializeField] private TMP_Text equipUse;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject useEquipeButton;

    private Button button;
    private int index;
    private Image slotImage;

    public void Init(int i)
    {
        index = i;
        slotImage = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
        UpdateUI();
    }

    public void UpdateUI()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (slot.IsEmpty)
        {
            slotImage.color = new Color(.84f, .84f, .84f, .75f);
            iconImage.enabled = false;
            quantityText.text = "";
        }
        else
        {
            slotImage.color = Color.white;
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;
            quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
        }
    }

    private void OnClick()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (!slot.IsEmpty && !slotOption.activeSelf)
        {
            if (slot.item.itemType == ItemType.Generic)
            {
                useEquipeButton.SetActive(false);
                slotOption.SetActive(true);
            }
            else
            {
                useEquipeButton.SetActive(true);
                equipUse.text = slot.item.itemType == ItemType.Consumable ? "use" : "equip";
                slotOption.SetActive(true);
            }
        }
        else if (slotOption.activeSelf)
        {
            slotOption.SetActive(false);
        }
    }

    public void UseEquipe()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (!slot.IsEmpty)
        {
            slot.item.Use(playerInventory);
            slot.ReduceAmount();
        }

        slotOption.SetActive(false);
    }

    public void DropItem()
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        if (slot.IsEmpty || slot.item.prefab == null)
        {
            Debug.Log("Item vazio ou sem prefab para drop.");
            return;
        }

        Vector3 dropPosition = new Vector3(playerInventory.transform.position.x, slot.item.prefab.transform.position.y, playerInventory.transform.position.z) + playerInventory.transform.forward * 2f;
        Instantiate(slot.item.prefab, dropPosition, slot.item.prefab.transform.rotation);

        slot.ReduceAmount();
        slotOption.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySlot slot = InventorySystem.Instance.GetSlot(index);

        slotOption.SetActive(false);

        if (!slot.IsEmpty)
        {
            InventoryDragHandler.Instance.StartDrag(slot.item.icon, index);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // nada aqui; movimentação é feita pelo InventoryDragHandler
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryDragHandler.Instance.EndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {

        slotOption.SetActive(false);
        if (!InventoryDragHandler.Instance.IsDragging)
            return;

        int fromIndex = InventoryDragHandler.Instance.DraggedFromIndex;
        if (fromIndex == index)
            return;

        InventorySystem.Instance.MoveItem(fromIndex, index);
    }
}
