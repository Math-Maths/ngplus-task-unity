using UnityEngine;

public interface IInteractable
{
    InteractableType GetInteractableType();

    public GameObject Interact(GameObject interactor);

    public void HighLight(bool state);
}

public enum InteractableType
{
    Item,
    NPC,
    Collectable
}
