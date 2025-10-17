using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField]
    float moveSpeed = 5f;

    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    float rotationY;

    [Header("Movement Settings")] [SerializeField]
    Transform groundCheck;

    [SerializeField] float groundDetectionDistance;
    [SerializeField] LayerMask groundMask;
    private bool isGrounded;
    [Header("Camera")] Transform cameraTransform;
    [SerializeField] Vector2 mouseSensitivity;

    [Header("References")] Rigidbody rb;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    [SerializeField] Transform orientation;

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDetectionDistance);
    }

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
        GroundCheck();
        if (playerInput.actions["Jump"].triggered)
        {
            TryJump();
        }
    }

    private void LateUpdate()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        if (lookInput == Vector2.zero) return;
        // orientation.Rotate(Vector3.right * (-lookInput.y * mouseSensitivity.y));

        // Vector3 camForwardProj =  Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        orientation.forward = cameraTransform.forward;
    }

    private void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 fullMovement = transform.forward * direction.y + transform.right * direction.x;
        transform.position += fullMovement * (moveSpeed * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        Vector3 camForwardProj = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        transform.forward = camForwardProj;
    }

    private void TryJump()
    {
        if (!isGrounded) return;
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDetectionDistance, groundMask);
    }
}