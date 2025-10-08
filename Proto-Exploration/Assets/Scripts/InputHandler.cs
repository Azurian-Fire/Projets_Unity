using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController characterController;
    private InputAction moveAction;
    private InputAction lookAction;

    private Vector2 moveInput;
    private Vector2 lookInput;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        characterController.Move(moveInput);

        lookInput = lookAction.ReadValue<Vector2>();
        characterController.Rotate(lookInput);

    }
}
