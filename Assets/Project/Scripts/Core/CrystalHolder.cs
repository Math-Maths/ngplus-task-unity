using UnityEngine;

public class CyrstalHolder : MonoBehaviour
{

    [SerializeField] private DoorController door;

    private bool crystalInserted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (crystalInserted) return;

        if (other.TryGetComponent<InteractableItem>(out var interactable))
        {
            if (interactable.GetInteractableType() == InteractableType.Item)
            {
                interactable.ChangeInteraction(false);

                crystalInserted = true;
                other.transform.parent = gameObject.transform;
                other.transform.position = transform.position + transform.up * 2;
                Debug.Log("Got the Crystal");

                door.OpenDoor();
                
                interactable.DropItem();
            }
        }
    }

}