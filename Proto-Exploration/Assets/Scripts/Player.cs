using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField] HealthBarUI healthBarUI;
    
    private void OnEnable()
    {
        InteractFruit.OnFruitEaten += HandleFruitEaten;
    }

    private void OnDisable()
    {
        InteractFruit.OnFruitEaten -= HandleFruitEaten;
    }

    private void HandleFruitEaten(int fruitEffect, InteractableColorKey interactableColorKey)
    {
        ChangeHealth(fruitEffect);
    }

    void Start()
    {
        ChangeHealth(0);
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            ChangeHealth(-10);
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeHealth(10);
        }
    }

    public void ChangeHealth(float healthChange)
    {
        health = Mathf.Clamp(health + healthChange, 0, maxHealth);
        healthBarUI.SetHealthUI(health, maxHealth);
    }
}