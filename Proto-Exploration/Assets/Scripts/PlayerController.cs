using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController characterController;

    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    private float rotationY;
    private float rotationX;

    public void Move(Vector2 moveInput)
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        move = move * moveSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    public void Rotate(Vector2 lookInput)
    {
        rotationY += lookInput.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}
