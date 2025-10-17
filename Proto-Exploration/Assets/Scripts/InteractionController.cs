using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] float interactDistance = 5f;
    [SerializeField] Transform playerOrientationTransform;
    [SerializeField] InteractBarUI interactBarUI;

    InteractableEntity currentInteractable;

    private bool stillOnLastInteraction;

    private float currentIncrementTimer;
    private float maxIncrementTimer;
    private float stressIncrement;
    private float incrementDuration;

    private float interactBarCompletion;
    private int reachedIncrement;

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
        Gizmos.DrawLine(Camera.main.transform.position,
            Camera.main.transform.position + Camera.main.transform.forward * 3 * interactDistance);
        Gizmos.color = Color.red;
        Vector3 fixedCameraVector = Camera.main.transform.position + Camera.main.transform.forward * 3 * interactDistance;
        Gizmos.DrawLine(playerOrientationTransform.position, fixedCameraVector);
    }

    void UpdateCurrentInteractable()
    {
        Vector3 fixedCameraVector = Camera.main.transform.position + Camera.main.transform.forward * 3 * interactDistance;
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
            maxIncrementTimer = currentInteractable.interactDuration;
            incrementDuration = maxIncrementTimer / currentInteractable.incrementCount;
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

    void UpdateInteractBar(float completionTime)
    {
        float newCompletion = completionTime / incrementDuration;
        // newCompletion = Mathf.Clamp(newCompletion, 0, 1);
        // Debug.Log(newCompletion);
        interactBarUI.SetInteractUI(newCompletion);
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
            if (currentIncrementTimer >= incrementDuration)
            {
                currentIncrementTimer = currentIncrementTimer % incrementDuration;
                reachedIncrement++;
            }

            // reachedIncrement = (int)Mathf.Floor(currentIncrementTimer / incrementDuration);
            UpdateInteractBar(currentIncrementTimer);
        }

        if (reachedIncrement == currentInteractable.incrementCount && !stillOnLastInteraction)
        {
            currentInteractable.Interact(reachedIncrement);
            currentIncrementTimer = 0;
            stillOnLastInteraction = true;
            return;
        }

        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            UpdateInteractBar(0);
            if (stillOnLastInteraction)
            {
                stillOnLastInteraction = false;
                reachedIncrement = 0;
                return;
            }


            if (reachedIncrement <= 0)
            {
                Debug.Log($"Failed to interact, nothing happens");
                currentIncrementTimer = 0;
                stillOnLastInteraction = false;
                return;
            }

            int stressToAdd = (int)Mathf.Floor(stressIncrement * reachedIncrement);
            Debug.Log(
                $"Partial interaction, reached increment {reachedIncrement} out of {currentInteractable.incrementCount}");
            currentIncrementTimer = 0;
            stillOnLastInteraction = false;
            currentInteractable.Interact(reachedIncrement);
            reachedIncrement = 0;
        }
    }
}