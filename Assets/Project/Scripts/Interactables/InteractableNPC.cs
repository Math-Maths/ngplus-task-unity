using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InteractableType type;
    [SerializeField] private DialogueData dialogue;
    [SerializeField] string characterName;

    private Outline highlight;

    private void Start()
    {
        highlight = GetComponent<Outline>();
        highlight.enabled = false;
    }

    public GameObject Interact(GameObject interactor)
    {
        DialogueUI.Instance.StartDialogue(dialogue, characterName);

        transform.LookAt(interactor.transform, transform.up);

        highlight.enabled = false;

        return gameObject;
    }

    public InteractableType GetInteractableType()
    {
        return type;
    }

    public void HighLight(bool state)
    {
        highlight.enabled = state;
    }
}