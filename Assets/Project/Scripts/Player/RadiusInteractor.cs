using UnityEngine;

public class RadiusInteractor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactable.CanInteract())
            {
                interactable.HighLight(true);
                InventorySystem.Instance.ShowInteractText(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactable.CanInteract())
            {
                interactable.HighLight(false);
                InventorySystem.Instance.ShowInteractText(false);
            }
        }
    }
}