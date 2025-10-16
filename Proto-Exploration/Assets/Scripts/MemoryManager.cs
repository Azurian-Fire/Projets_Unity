using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager memoryManager { get; private set; }
    [SerializeField] private List<Transform> transformList;
    private Dictionary<InteractableColorKey, InteractionType> knownInteractables;

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

        knownInteractables = new Dictionary<InteractableColorKey, InteractionType>();
    }

    private void HandleFruitMemory(int stressChange, InteractableEntity entity)
    {
        ProcessInteraction(entity);
        // Debug.Log(knownInteractables.Keys.ToList().Count);
        // Debug.Log($"{knownInteractables.Keys.ToList()[0].interactableName} / {knownInteractables.Keys.ToList()[0].color} / {knownInteractables.Values.ToList()[0]}");
        // Debug.Log($"{knownInteractables.Keys.ToList()[knownInteractables.Keys.ToList().Count-1].interactableName} / {knownInteractables.Keys.ToList()[knownInteractables.Values.ToList().Count-1].color} / {knownInteractables.Keys.ToList()[knownInteractables.Values.ToList().Count-1]}");
    }
    
    public void RememberInteraction(string interactableName, string color, InteractionType interaction)
    {
        InteractableColorKey key = new InteractableColorKey(interactableName, color);
        knownInteractables[key] = interaction;
    }

    public InteractionType GetMemory(string interactableName, string color)
    {
        InteractableColorKey key = new InteractableColorKey(interactableName, color);
        if (knownInteractables.TryGetValue(key, out InteractionType interaction))
        {
            return interaction;
        }

        return InteractionType.Neutral;
    }

    private void ProcessInteraction(InteractableEntity interactableEntity)
    {
        InteractionType interactionType;
        string interactableName = Enum.GetName(typeof(InteractableType), interactableEntity.interactableType);
        int interactionValue = interactableEntity.totalStressValue;

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

        RememberInteraction(interactableName, interactableEntity.colorName, interactionType);
    }
}