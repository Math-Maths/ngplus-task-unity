using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySlotUI[] slotUIs;

    private void Start()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].Init(i);
        }

        InventorySystem.Instance.OnInventoryChanged += UpdateAll;
    }

    private void OnDestroy()
    {
        InventorySystem.Instance.OnInventoryChanged -= UpdateAll;
    }

    private void UpdateAll()
    {
        foreach (var slot in slotUIs)
        {
            slot.UpdateUI();
        }
    }
}
