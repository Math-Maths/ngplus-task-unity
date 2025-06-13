using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryItem scriptableItem;
    [SerializeField] int quantity;

    private Outline highlight;

    public InteractableType type;

    private void Start()
    {
        highlight = GetComponent<Outline>();
        highlight.enabled = false;
    }

    public GameObject Interact(GameObject interactor)
    {

        if (type == InteractableType.Collectable)
        {
            bool added = InventorySystem.Instance.AddItem(scriptableItem);

            if (added)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventário cheio ou item não pôde ser adicionado.");
                return null;
            }
        }
        //Debug.Log($"{gameObject.name} foi pego por {interactor.name}");
        GetComponent<FloatingItem>()?.PauseRotationAndFloat();
        highlight.enabled = false;

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

    public void HighLight(bool state)
    {
        highlight.enabled = state;
    }
}
