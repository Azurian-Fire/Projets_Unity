using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [SerializeField] Transform playerTransform;


    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] float interactDistance = 5f;

    InteractableEntity currentInteractable;

    private float currentIncrementTimer;
    private float maxIncrementTimer;
    private float stressIncrement;

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
        Gizmos.DrawLine(playerTransform.position,
            playerTransform.position + playerTransform.forward * interactDistance);
    }

    void UpdateCurrentInteractable()
    {
        //TODO seulement on change plutot que par frame
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance);
        if (hitInfo.collider == null)
        {
            currentInteractable = null;
            return;
        }

        // if (!hitInfo.collider.GetComponent<InteractableEntity>())
        // {
        //     return;
        // }

        if (hitInfo.collider.GetComponent<InteractableEntity>() != currentInteractable)
        {
            currentInteractable = hitInfo.collider?.GetComponent<InteractableEntity>();
            Debug.Log($"currentInteractable {currentInteractable}");
            Debug.Log($"totalStressValue {currentInteractable.totalStressValue}");
            Debug.Log($"incrementCount {currentInteractable.incrementCount}");
            stressIncrement = currentInteractable.totalStressValue / currentInteractable.incrementCount;
            Debug.Log($"stressIncrement {stressIncrement}");
        }
    }

    void UpdateInteractText()
    {
        if (currentInteractable == null)
        {
            interactText.text = string.Empty;
            return;
        }

        interactText.text = currentInteractable.GetInteractMessage();
    }

    void CheckForInteractionInput()
    {
        if (currentInteractable == null)
        {
            return;
        }

        if (Keyboard.current.eKey.isPressed)
        {
            currentIncrementTimer += Time.deltaTime;
        }

        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            float incrementDuration = maxIncrementTimer / currentInteractable.incrementCount;
            int reachedIncrement = (int)Mathf.Floor(currentIncrementTimer / incrementDuration);
            int stressToAdd = (int)Mathf.Floor(stressIncrement * reachedIncrement);
            Debug.Log($"Stress to add: {stressToAdd}");
            currentInteractable.Interact(stressToAdd);
            return;
        }

        if (currentIncrementTimer >= currentInteractable.interactDuration)
        {
            currentInteractable.Interact(currentInteractable.totalStressValue);
            currentIncrementTimer = 0;
            return;
        }
    }
}