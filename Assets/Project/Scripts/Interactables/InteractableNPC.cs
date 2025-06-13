using System.Collections;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InteractableType type;
    [SerializeField] private DialogueData[] dialogue;
    [SerializeField] private string characterName;
    [SerializeField] private int requiredCoins = 5;
    [SerializeField] private DialogueData finalDialogue;

    private Outline highlight;
    private int dialoguesCount = 1;
    private bool hasReceivedCoins = false;
    private bool canInteract = true;

    private void Start()
    {
        highlight = GetComponent<Outline>();
        highlight.enabled = false;
    }

    public GameObject Interact(GameObject interactor)
    {
        InventorySystem.Instance.ToggleInventory(false);

        if (dialoguesCount < dialogue.Length)
        {
            DialogueUI.Instance.StartDialogue(dialogue[dialoguesCount -1], characterName);
            dialoguesCount++;
        }
        else
        {
            DialogueUI.Instance.OnDialougueEnd += DoneTalking;
            DialogueUI.Instance.StartDialogue(dialogue[dialogue.Length - 1], characterName);
            dialoguesCount = dialogue.Length;
        }

        transform.LookAt(interactor.transform, transform.up);

        highlight.enabled = false;

        return gameObject;
    }

    private void DoneTalking()
    {
        if (!hasReceivedCoins && HasEnoughCoins())
        {
            RemoveCoins(requiredCoins);
            hasReceivedCoins = true;
            Debug.Log("Moedas entregues ao NPC!");
            StartCoroutine(ShowFinalDialougue());
        }
    }

    IEnumerator ShowFinalDialougue()
    {
        yield return new WaitForSeconds(.1f);
        DialogueUI.Instance.StartDialogue(finalDialogue, characterName);
        canInteract = false;
    }

     private bool HasEnoughCoins()
    {
        int coinCount = 0;
        foreach (var slot in InventorySystem.Instance.slots)
        {
            if (!slot.IsEmpty && slot.item.itemType == ItemType.Generic)
                coinCount += slot.quantity;

            if (coinCount >= requiredCoins)
                return true;
        }
        return false;
    }

    private void RemoveCoins(int amount)
    {
        for (int i = 0; i < InventorySystem.Instance.slots.Length; i++)
        {
            var slot = InventorySystem.Instance.slots[i];
            if (!slot.IsEmpty && slot.item.itemType == ItemType.Generic)
            {
                int toRemove = Mathf.Min(amount, slot.quantity);
                slot.quantity -= toRemove;
                amount -= toRemove;

                if (slot.quantity <= 0)
                    slot.Clear();

                if (amount <= 0)
                    break;
            }
        }

        InventorySystem.Instance.UpdateInventory();
    }

    public InteractableType GetInteractableType()
    {
        return type;
    }

    public bool CanInteract() => canInteract;

    public void HighLight(bool state)
    {
        highlight.enabled = state;
    }
}