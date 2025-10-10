using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraTransform;
    [SerializeField] Vector2 mouseSensitivity;

    [Header("References")]
    Rigidbody rb;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    [SerializeField] Transform fakeBody;



    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        if (playerInput.actions["Jump"].triggered)
        {
            TryJump();
        }
    }

    private void LateUpdate()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        fakeBody.Rotate(Vector3.right * -lookInput.y * mouseSensitivity.y);
    }

    private void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 fullMovement = transform.forward * direction.y + transform.right * direction.x;
        transform.position += fullMovement * moveSpeed * Time.deltaTime;
    }

    private void RotatePlayer()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        if (lookInput == Vector2.zero) return;
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity.x);
    }

    private void TryJump()
    {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.Impulse);
    }
}
