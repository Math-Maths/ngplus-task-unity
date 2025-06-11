using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;

    [Header("References")]
    [SerializeField] private AnimatorBehabiour animator;

    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private CharacterController controller;
    
    private bool isSprinting;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
    }

    #region Input Subscription

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Sprint.performed += ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;

        inputActions.Player.Sprint.performed -= ctx => isSprinting = true;
        inputActions.Player.Sprint.canceled -= ctx => isSprinting = false;

        inputActions.Player.Disable();
    }

    #endregion

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed;
        controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
        else
        {
            speed = 0f;
        }
        
        animator.SetMovment(speed, isSprinting);
            
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
