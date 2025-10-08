using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [SerializeField] Transform playerTransform;

    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] float interactDistance = 5f;

    IInteractable currentInteractable;

    public void Update()
    {
        UpdateCurrentInteractable();

        UpdateInteractText();

        CheckForInteractionInput();
    }

    void OnDrawGizmos()
    {
        if (playerTransform == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerTransform.position, playerTransform.position + playerTransform.forward * interactDistance);
    }

    void UpdateCurrentInteractable()
    {
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance);

        currentInteractable = hitInfo.collider?.GetComponent<IInteractable>();
        Debug.Log(currentInteractable);
    }

    void UpdateInteractText()
    {
        if (currentInteractable == null)
        {
            interactText.text = string.Empty;
            return;
        }

        interactText.text = currentInteractable.interactMessage;
    }

    void CheckForInteractionInput()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
