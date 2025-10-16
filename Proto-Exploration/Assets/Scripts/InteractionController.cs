using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] float interactDistance = 5f;
    [SerializeField] Transform playerOrientationTransform;

    InteractableEntity currentInteractable;

    private bool stillOnLastInteraction;
    
    private float currentIncrementTimer;
    private float maxIncrementTimer;
    private float stressIncrement;

    private void Update()
    {
        UpdateCurrentInteractable();
        UpdateInteractText();

        HandleInteractionInput();
    }

    void OnDrawGizmos()
    {
        if (playerOrientationTransform == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerOrientationTransform.position,
            playerOrientationTransform.position + playerOrientationTransform.forward * interactDistance);
    }

    void UpdateCurrentInteractable()
    {
        Ray ray = new Ray(playerOrientationTransform.position, playerOrientationTransform.forward);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance);

        if (!hitAnObject)
        {
            currentInteractable = null;
            return;
        }

        InteractableEntity interactableCandidate = hitInfo.collider.GetComponent<InteractableEntity>();

        if (!interactableCandidate)
        {
            currentInteractable = null;
            return;
        }

        if (interactableCandidate != currentInteractable)
        {
            currentInteractable = interactableCandidate;
            stressIncrement = currentInteractable.totalStressValue / currentInteractable.incrementCount;
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

    void HandleInteractionInput()
    {
        if (currentInteractable == null)
        {
            return;
        }

        if (Keyboard.current.eKey.isPressed && !stillOnLastInteraction)
        {
            currentIncrementTimer += Time.deltaTime;
        }

        if (currentIncrementTimer >= currentInteractable.interactDuration)
        {
            currentInteractable.Interact(currentInteractable.incrementCount);
            currentIncrementTimer = 0;
            stillOnLastInteraction = true;
            return;
        }
        
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {

            if (stillOnLastInteraction)
            {
                stillOnLastInteraction = false;
                return;
            }

            float incrementDuration = maxIncrementTimer / currentInteractable.incrementCount;
            int reachedIncrement = (int)Mathf.Floor(currentIncrementTimer / incrementDuration);

            if (reachedIncrement <= 0)
            {
                Debug.Log($"Failed to interact, nothing happens");
                currentIncrementTimer = 0;
                stillOnLastInteraction = false;
                return;
            }

            int stressToAdd = (int)Mathf.Floor(stressIncrement * reachedIncrement);
            Debug.Log($"Partial interaction, reached increment {reachedIncrement} out of {currentInteractable.incrementCount}");
            currentIncrementTimer = 0;
            stillOnLastInteraction = false;
            currentInteractable.Interact(reachedIncrement);
            return;
        }
    }
}