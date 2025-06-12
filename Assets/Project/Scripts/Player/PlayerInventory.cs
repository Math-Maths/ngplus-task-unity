using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform rightHandSlot;

    private GameObject equippedItemInstance;

    public void Equip(EquipableItem item)
    {
        //Destroy the previous item
        if (equippedItemInstance != null)
            Destroy(equippedItemInstance);

        //Instantiate a new item
        if (item.prefab != null)
        {
            equippedItemInstance = Instantiate(item.handPrefab, rightHandSlot);
            equippedItemInstance.transform.localPosition = Vector3.zero;
            equippedItemInstance.transform.localRotation = Quaternion.identity;

            Debug.Log($"Equipado: {item.itemName}");
        }
        else
        {
            Debug.LogWarning($"Item {item.itemName} não tem prefab atribuído.");
        }
    }

    public void Consume(ConsumableItem item)
    {
        Debug.Log($"Consumido: {item.itemName}");
    }
}
