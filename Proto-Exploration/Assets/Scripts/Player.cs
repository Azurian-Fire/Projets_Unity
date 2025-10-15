using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] TMP_Text currentHealthText;
    [SerializeField] TMP_Text totalHealthText;
    [SerializeField] HealthBarUI healthBarUI;

    private void OnEnable()
    {
        InteractFruit.OnFruitEaten += HandleFruitEaten;
    }

    private void OnDisable()
    {
        InteractFruit.OnFruitEaten -= HandleFruitEaten;
    }

    private void HandleFruitEaten(int fruitEffect)
    {
        ChangeStress(fruitEffect);
    }

    void Start()
    {
        healthBarUI.SetMaxHealth(0f);
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SetHealth(-10f);
            ChangeStress(-10);
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SetHealth(10f);
            ChangeStress(10);
        }
    }

    public void SetHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBarUI.SetHealth(health);
    }

    public void ChangeStress(int stressDelta)
    {
        health = Mathf.Clamp(health + stressDelta, 0, maxHealth);
        currentHealthText.text = health.ToString();
    }
}
