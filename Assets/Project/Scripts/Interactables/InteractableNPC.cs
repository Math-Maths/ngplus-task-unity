using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InteractableType type;
    [SerializeField] private DialogueData dialogue;
    [SerializeField] string characterName;

    public GameObject Interact(GameObject interactor)
    {
        DialogueUI.Instance.StartDialogue(dialogue, characterName);

        return gameObject;
    }

    public InteractableType GetInteractableType()
    {
        return type;
    }
}