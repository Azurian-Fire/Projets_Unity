using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    public float health;
    public float maxHealth;
    public float width;
    public float height;

    public void SetMaxHealth(float maxH)
    {
        maxHealth = maxH;
    }

    public void SetHealthUI(float h)
    {
        health = h;
        float newWidth = (health / maxHealth) * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
}
