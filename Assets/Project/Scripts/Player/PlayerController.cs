using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;

    [Header("Interaction Settings")]
    [SerializeField] private float interactRadius = 2f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("References")]
    [SerializeField] private AnimatorBehabiour animator;
    [SerializeField] private Transform carryPlaceHolder;
    [SerializeField] private GameObject runParticles;

    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private CharacterController controller;
    private InteractableItem carryingObj;
    
    private bool isSprinting;
    private bool occupied = false;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
    }

    #region Input/Events Subscription

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Sprint.performed += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;

        inputActions.Player.Interact.performed += OnInteract;

        inputActions.Player.Inventory.performed += OpenInventory;

        DialogueEvents.OnDialogueStarted += DisableMovement;
        DialogueEvents.OnDialogueEnded += EnableMovement;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;

        inputActions.Player.Sprint.performed -= ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled -= ctx => isSprinting = false;

        inputActions.Player.Interact.performed -= OnInteract;

        inputActions.Player.Inventory.performed -= OpenInventory;

        inputActions.Player.Disable();

        DialogueEvents.OnDialogueStarted -= DisableMovement;
        DialogueEvents.OnDialogueEnded -= EnableMovement;
    }

    #endregion

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (occupied) return;

        float speed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed;
        controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = move;
            runParticles.SetActive(isSprinting);
        }
        else
        {
            runParticles.SetActive(false);
            speed = 0f;
        }

        animator.SetMovment(speed, isSprinting);

    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (occupied) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius, interactableLayer);

        if (carryingObj == null)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IInteractable>(out var interactable))
                {
                    HandleInteraction(interactable.GetInteractableType(), interactable.Interact(gameObject));
                    break; //Interact with the first found
                }
            }
        }
        else
        {
            carryingObj.DropItem();
            carryingObj.transform.parent = null;
            carryingObj = null;
            animator.HandleObject(false);
        }
    }

    private void HandleInteraction(InteractableType type, GameObject obj)
    {

        InventorySystem.Instance.ShowInteractText(false);

        switch (type)
        {
            case InteractableType.Item:
                Debug.Log("You got an item");
                carryingObj = obj.GetComponent<InteractableItem>();
                carryingObj.transform.parent = carryPlaceHolder;
                carryingObj.transform.position = carryPlaceHolder.position;
                carryingObj.transform.rotation = carryPlaceHolder.rotation;
                animator.HandleObject(true);
                break;
            case InteractableType.NPC:
                animator.SetMovment(0, false);
                transform.LookAt(obj.transform, transform.up);
                //Debug.Log("Let's Talk");
                break;
            case InteractableType.Collectable:
                Debug.Log("Take it");
                break;
        }
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        InventorySystem.Instance.ToggleInventory();
        Debug.Log("click");
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void DisableMovement()
    {
        occupied = true;
    }

    private void EnableMovement()
    {
        occupied = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
