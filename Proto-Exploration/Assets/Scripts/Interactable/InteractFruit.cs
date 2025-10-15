using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InteractFruit : InteractableEntity
{
    public static event Action<int> OnFruitEaten;
    public string interactMessage => message;
    [SerializeField] string message = "Press E to eat fruit";

    public int stressEffect;

    public override string GetInteractMessage()
    {
        string message = base.GetInteractMessage();
        return message;
    }

    public override void Interact(int stressChange)
    {
        Debug.Log($"You ate the fruit! Stress effect: {stressChange}");
        OnFruitEaten?.Invoke(stressEffect);
        Destroy(gameObject);
    }

}
