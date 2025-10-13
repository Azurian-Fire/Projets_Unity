using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FruitEffect : MonoBehaviour, IInteractable
{
    public static event Action<int> OnFruitEaten;
    public string interactMessage => message;
    [SerializeField] string message = "Press E to eat fruit";

    public int stressEffect;

    public void Interact()
    {
        EatFruit();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EatFruit()
    {
        Debug.Log($"You ate the fruit! Stress effect: {stressEffect}");
        OnFruitEaten?.Invoke(stressEffect);
        Destroy(gameObject);
    }
}
