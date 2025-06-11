using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public InteractableType type;

    public GameObject Interact(GameObject interactor)
    {
        //Debug.Log($"{gameObject.name} foi pego por {interactor.name}");
        GetComponent<FloatingItem>()?.PauseRotationAndFloat();

        return gameObject;
    }

    public void DropItem()
    {
        GetComponent<FloatingItem>()?.ResumeRotationAndFloat();
    }

    public InteractableType GetInteractableType()
    {
        return type;
    }
}
