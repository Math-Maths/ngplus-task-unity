using UnityEngine;

public class RadiusInteractor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.HighLight(true);
            InventorySystem.Instance.ShowInteractText(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.HighLight(false);
            InventorySystem.Instance.ShowInteractText(false);
        }
    }
}