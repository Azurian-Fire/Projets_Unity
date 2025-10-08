using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] HealthBarUI healthBarUI;

    void Start()
    {
        healthBarUI.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SetHealth(-10f);
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SetHealth(10f);
        }
    }

    public void SetHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBarUI.SetHealth(health);
    }
}
