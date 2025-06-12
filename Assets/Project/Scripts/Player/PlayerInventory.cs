using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform rightHandSlot;

    private GameObject equippedItemInstance;

    public void Equip(EquipableItem item)
    {
        // Destroi item anterior, se houver
        if (equippedItemInstance != null)
            Destroy(equippedItemInstance);

        // Instancia o novo objeto na mão
        if (item.prefab != null)
        {
            equippedItemInstance = Instantiate(item.prefab, rightHandSlot);
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
