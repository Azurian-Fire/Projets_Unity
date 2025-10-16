using System;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.Rendering.DebugUI;

public class InteractFruit : InteractableEntity
{
    public static event Action<int, InteractableEntity> OnFruitEaten;
    public string interactMessage => message;
    [SerializeField] string message = "Press E to eat fruit";

    public override string GetInteractMessage()
    {
        string message = base.GetInteractMessage();
        return message;
    }

    public override void Interact(int succesfulfIncrementCount, int stressChange)
    {
        if (stressChange == 0)
        {
            stressChange = totalStressValue;
        }
        Debug.Log($"You ate the fruit! Stress effect: {stressChange}");
        OnFruitEaten?.Invoke(totalStressValue, this);
        Destroy(gameObject);
    }

}
