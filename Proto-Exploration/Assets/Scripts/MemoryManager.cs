using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager memoryManager { get; private set; }
    [SerializeField] private List<Transform> transformList;
    private static Dictionary<string, InteractionType> knownInteractables;

    private void OnEnable()
    {
        InteractFruit.OnFruitEaten += HandleFruitMemory;
    }

    private void OnDisable()
    {
        InteractFruit.OnFruitEaten -= HandleFruitMemory;
    }
    private void Awake()
    {
        if (memoryManager != null && memoryManager != this)
        {
            Destroy(this);
        }
        else
        {
            memoryManager = this;
        }

        knownInteractables = new Dictionary<string, InteractionType>();
    }

    private void HandleFruitMemory(int stressChange, InteractableColorKey interactableColorKey)
    {
        ProcessInteraction(interactableColorKey);
        // Debug.Log(knownInteractables.Keys.ToList().Count);
        // Debug.Log($"{knownInteractables.Keys.ToList()[0].interactableName} / {knownInteractables.Keys.ToList()[0].color} / {knownInteractables.Values.ToList()[0]}");
        // Debug.Log($"{knownInteractables.Keys.ToList()[knownInteractables.Keys.ToList().Count-1].interactableName} / {knownInteractables.Keys.ToList()[knownInteractables.Values.ToList().Count-1].color} / {knownInteractables.Keys.ToList()[knownInteractables.Values.ToList().Count-1]}");
    }
    
    public void RememberInteraction(InteractableColorKey interactableColorKey, InteractionType interaction)
    {
        Debug.Log($"{interactableColorKey.GetInteractableName()} will now be known as {interaction}");
        knownInteractables[interactableColorKey.GetInteractableName()] = interaction;
        Debug.Log(interactableColorKey.GetInteractableName());
        Debug.Log(knownInteractables[interactableColorKey.GetInteractableName()]);
    }

    public static InteractionType GetMemory(string interactableColorKeyName)
    {
        if (knownInteractables.TryGetValue(interactableColorKeyName, out InteractionType interaction))
        {
            return interaction;
        }

        return InteractionType.Neutral;
    }

    private void ProcessInteraction(InteractableColorKey interactableColorKey)
    {
        InteractionType interactionType;
        int interactionValue = interactableColorKey.interactableEntity.totalStressValue;

        if (interactionValue > 0)
        {
            interactionType = InteractionType.Positive;
        }
        else if (interactionValue < 0)
        {
            interactionType = InteractionType.Negative;
        }
        else
        {
            interactionType = InteractionType.Neutral;
        }

        RememberInteraction(interactableColorKey, interactionType);
    }
}