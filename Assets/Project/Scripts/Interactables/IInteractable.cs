using UnityEngine;

public interface IInteractable
{
    InteractableType GetInteractableType();

    public GameObject Interact(GameObject interactor);
}

public enum InteractableType
{
    Item,
    NPC,
    Door
}
